using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextScript;

namespace BarrageShooting.StageScript
{
    public class StageScriptMain : TextScriptCore
    {
        private const string LIST_SCRIPT = "wave_list";
        private const int START_INTERVAL = 120;
        private const int WAVE_INTERVAL = 120;

        private bool IsProcStage;
        private int StepIndex;

        private int PastTime;
        private int WaitTime;

        private List<string> WaveList;
        private Dictionary<string, WaveScript> WaveWarehouse;
        private WaveScript CurrentWave;

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
            group.ScriptLine.ForEach(line => {
                line.Attributes.ForEach(atr => { 
                    if(atr.Name.CompareTo("target") == 0)
                    {
                        string trg = atr.StringValue;

                        if(string.IsNullOrEmpty(trg) == false)
                        {
                            WaveScript wave;
                            if (WaveWarehouse.TryGetValue(trg, out wave) == false)
                            {
                                wave = new WaveScript(this, Group.Find(grp => grp.GroupName == trg));
                                if (wave != null) WaveWarehouse.Add(trg, wave);
                            }

                            if (wave != null) WaveList.Add(trg);
                        }
                    }
                });
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

            if ((StepIndex & 0x01) == 0)
            {
                PastTime++;
                if(PastTime > WaitTime)
                {
                    CurrentWave = WaveWarehouse[WaveList[StepIndex / 2]];
                    CurrentWave.StartWave();

                    StepIndex++;
                    WaitTime = WAVE_INTERVAL;
                    PastTime = 0;
                }
            }
            else {
                if (CurrentWave.OnUpdate() == false) StepIndex++;
                if ((StepIndex / 2) >= WaveList.Count) OnFInishStage();
            }
        }

        /// *******************************************************
        /// <summary>ステージ終了</summary>
        /// *******************************************************
        public void OnFInishStage()
        {
            IsProcStage = false;
            Debug.Log("EndStage");
        }
    }
}
