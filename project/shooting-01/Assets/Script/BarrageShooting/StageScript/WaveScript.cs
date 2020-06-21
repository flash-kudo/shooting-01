using System.Collections;
using System.Collections.Generic;
using TextScript;
using UnityEngine;

namespace BarrageShooting.StageScript
{
    public class WaveScript
    {
        private StageScriptMain Manager;
        private ScriptGroup Scripts;

        private int LineIndex;

        private int PastTime;
        private int WaitTime;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public WaveScript(StageScriptMain mng, ScriptGroup group)
        {
            Manager = mng;
            Scripts = group;
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
            PastTime++;
            if(PastTime > WaitTime)
            {
                while (true)
                {
                    if (LineIndex >= Scripts.ScriptLine.Count) return false;

                    ScriptLine line = Scripts.ScriptLine[LineIndex];
                    LineIndex++;

                    if (line.CommandName.CompareTo("interval") == 0){
                        IntervalLine(line);
                        PastTime = 0;
                        break;
                    }
                    else
                    {
                        SpawnLine(line);
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
