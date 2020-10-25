using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextScript;

namespace BarrageShooting.StageScript
{
    public class StageScriptMain : TextScriptCore
    {
        private StageManager Manager;

        private const string LIST_SCRIPT = "wave_list";
        private const int START_INTERVAL = 120;
        private const int WAVE_INTERVAL = 30;
        private const int MESSAGE_INTERVAL = 60;
        private const int STEP_LNG = 5;

        public bool IsProcStage;
        private int StepIndex;

        private int PastTime;
        private int WaitTime;

        private List<string> WaveList;
        private List<string> WaveMessage;
        private Dictionary<string, WaveScript> WaveWarehouse;
        private WaveScript CurrentWave;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public StageScriptMain(StageManager mng)
        {
            Manager = mng;
        }

        /// *******************************************************
        /// <summary>ファイル読み込み</summary>
        /// *******************************************************
        public void ReadScriptFile(string path)
        {
            ReadFile(path);
            ParseScript();
        }
        /// *******************************************************
        /// <summary>テキスト読み込み</summary>
        /// *******************************************************
        public void ReadScriptText(string source)
        {
            ReadText(source);
            ParseScript();
        }

        /// *******************************************************
        /// <summary>スクリプト解析</summary>
        /// *******************************************************
        private void ParseScript()
        {
            WaveWarehouse = new Dictionary<string, WaveScript>();
            ScriptGroup group = Group.Find(grp => grp.GroupName == LIST_SCRIPT);

            WaveList = new List<string>();
            WaveMessage = new List<string>();
            group.ScriptLine.ForEach(line => {
                if(line.CommandName.CompareTo("wave") == 0)
                {
                    line.Attributes.ForEach(atr => {
                        if (atr.Name.CompareTo("target") == 0)
                        {
                            string trg = atr.StringValue;

                            if (string.IsNullOrEmpty(trg) == false)
                            {
                                WaveScript wave;
                                if (WaveWarehouse.TryGetValue(trg, out wave) == false)
                                {
                                    wave = new WaveScript(this, null, Group.Find(grp => grp.GroupName == trg));
                                    if (wave != null) WaveWarehouse.Add(trg, wave);
                                }

                                if (wave != null) WaveList.Add(trg);
                            }
                        }
                    });
                }
                else if (line.CommandName.CompareTo("build") == 0)
                {
                    WaveList.Add("build");
                }
                string msg = "";
                line.Attributes.ForEach(atr => {
                    if (atr.Name.CompareTo("msg") == 0) msg = atr.StringValue;
                });
                WaveMessage.Add(msg);
            });
        }

        /// *******************************************************
        /// <summary>ステージ開始</summary>
        /// *******************************************************
        public void StartStage()
        {
            IsProcStage = true;
            StepIndex = 0;
            WaitTime = START_INTERVAL;
            PastTime = 0;
        }

        /// *******************************************************
        /// <summary>ステージ進行 / 更新処理</summary>
        /// *******************************************************
        public void OnUpdate()
        {
            if (IsProcStage == false) return;
            if (Manager.OnBombRunning == true) return;

            Manager.SetWaveNumber(StepIndex / STEP_LNG);

            if (Manager.GetWaveNumber() >= WaveList.Count)
            {
                OnFInishStage();
                return;
            }
            if ((StepIndex % STEP_LNG) == 0)
            {
                string wave_msg = WaveMessage[Manager.GetWaveNumber()];
                if (StageManager.Instance != null) StageManager.Instance.ShowMessage(wave_msg);
                StepIndex++;
            }
            else if ((StepIndex % STEP_LNG) == 1)
            {
                PastTime++;
                if (PastTime > MESSAGE_INTERVAL)
                {
                    StepIndex++;
                    PastTime = 0;
                }
            }
            else if ((StepIndex % STEP_LNG) == 2)
            {
                PastTime++;
                if (PastTime > WaitTime)
                {
                    if (StageManager.Instance != null) StageManager.Instance.HideMessage();

                    string wave_name = WaveList[Manager.GetWaveNumber()];

                    if (wave_name.CompareTo("build") == 0)
                    {
                        if (StageManager.Instance != null) StageManager.Instance.OpenBuildScreen();
                        StepIndex++;
                    }
                    else
                    {
                        CurrentWave = WaveWarehouse[wave_name];
                        CurrentWave.StartWave();
                    }

                    StepIndex++;
                    WaitTime = WAVE_INTERVAL;
                    PastTime = 0;
                }
            }
            else if ((StepIndex % STEP_LNG) == 3)
            {
                if ((CurrentWave != null) && (CurrentWave.OnUpdate() == false)) StepIndex++;
            }
            else if ((StepIndex % STEP_LNG) == 4)
            {
                if (StageManager.Instance == null) return;
                if (StageManager.Instance.IsBuildScreen == true) return;
                if (StageManager.Instance.EnemyCount != 0) return;
                if (FortressControll.Instance != null) return;
                StepIndex++;
            }

        }

        /// *******************************************************
        /// <summary>ステージ終了</summary>
        /// *******************************************************
        public void OnFInishStage()
        {
            Manager.OnEndGame();
        }
    }
}
