using System.Collections;
using System.Collections.Generic;
using TextScript;
using UnityEngine;

namespace BarrageShooting.StageScript
{
    public class WaveScript
    {
        private StageScriptMain Manager;
        private WaveScript Parent;
        private ScriptGroup Scripts;

        private WaveScript ChildScript;

        private int LineIndex;

        private int PastTime;
        private int WaitTime;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public WaveScript(StageScriptMain mng, WaveScript prnt, ScriptGroup group)
        {
            Manager = mng;
            Parent = prnt;
            Scripts = group;

            ChildScript = null;
        }

        /// *******************************************************
        /// <summary>Wave開始</summary>
        /// *******************************************************
        public void StartWave()
        {
            WaitTime = -1;
            PastTime = 0;
            LineIndex = 0;
        }

        /// *******************************************************
        /// <summary>Wave進行 / 更新処理</summary>
        /// *******************************************************
        public bool OnUpdate()
        {
            if(ChildScript != null)
            {
                if(ChildScript.OnUpdate() == false)
                {
                    ChildScript = null;
                }
                return true;
            }


            PastTime++;
            if(PastTime > WaitTime)
            {
                bool isOpenRandom = false;
                List<ScriptLine> GroupLines = new List<ScriptLine>();

                while (true)
                {
                    if (Scripts == null) return false;
                    if (Scripts.ScriptLine == null) return false;
                    if (LineIndex >= Scripts.ScriptLine.Count) return false;

                    ScriptLine line = Scripts.ScriptLine[LineIndex];
                    LineIndex++;

                    string CommandName = line.CommandName;

                    if(isOpenRandom == true)
                    {
                        if (CommandName.CompareTo("/random") == 0)
                        {
                            isOpenRandom = false;

                            if((GroupLines != null) && (GroupLines.Count > 0))
                            {
                                float rndtotal = 0;
                                GroupLines.ForEach(grp => rndtotal += grp.GetAttribute("rate").FloatValue);
                                float rndans = Random.value * rndtotal;
                                int selectindex = 0;
                                for (int i = 0; i < GroupLines.Count; i++)
                                {
                                    rndans = rndans - GroupLines[i].GetAttribute("rate").FloatValue;
                                    if(rndans <= 0)
                                    {
                                        selectindex = i;
                                        break;
                                    }
                                }
                                string childname = GroupLines[selectindex].GetAttribute("target").StringValue;
                                ScriptGroup group = Manager.Group.Find(grp => grp.GroupName == childname);
                                ChildScript = new WaveScript(Manager, this, group);
                                ChildScript.StartWave();
                            }

                        }
                        else if (CommandName.CompareTo("group") == 0)
                        {
                            GroupLines.Add(line);
                        }
                    }
                    else
                    {
                        if (CommandName.CompareTo("interval") == 0)
                        {
                            IntervalLine(line);
                            PastTime = 0;
                            break;
                        }
                        else if (CommandName.CompareTo("random") == 0)
                        {
                            isOpenRandom = true;
                            GroupLines = new List<ScriptLine>();
                        }
                        else
                        {
                            SpawnLine(line);
                        }
                    }

                }
            }

            return true;
        }

        /// *******************************************************
        /// <summary>インターバルコマンド実行</summary>
        /// *******************************************************
        private void IntervalLine(ScriptLine line)
        {
            line.Attributes.ForEach(atr =>
            {
                switch (atr.Name)
                {
                    case "delay": WaitTime = atr.IntValue; break;
                }
            });
        }

        /// *******************************************************
        /// <summary>コマンド実行</summary>
        /// *******************************************************
        private void SpawnLine(ScriptLine line)
        {
            string source = null;
            string key = null;
            float spd = 0;
            float dir = 0;

            line.Attributes.ForEach(atr =>
            {
                switch (atr.Name)
                {
                    case "source": source = atr.StringValue; break;
                    case "key": key = atr.StringValue; break;
                    case "spd": spd = atr.FloatValue; break;
                    case "dir": dir = atr.FloatValue; break;
                }
            });
            if (string.IsNullOrEmpty(source) == false)
            {
                //Debug.Log("Spawn:" + source + " / " + key);

                GameObject spawner = StageManager.Instance.InstantiateObject(source);
                EnemyControll self_ctrl = spawner.GetComponent<EnemyControll>();
                FortressControll fortress_ctrl = spawner.GetComponent<FortressControll>();

                if (self_ctrl != null) self_ctrl.SpawnKey = key;
                if(fortress_ctrl != null)
                {
                    fortress_ctrl.MoveSpeed = spd / 100f;
                    fortress_ctrl.FortressDirection = dir;
                }

                spawner.SetActive(true);
            }
        }

    }
}
