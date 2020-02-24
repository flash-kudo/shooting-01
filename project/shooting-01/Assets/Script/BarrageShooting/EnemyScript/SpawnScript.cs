using System.Collections;
using System.Collections.Generic;
using TextScript;
using UnityEngine;

namespace BarrageShooting.EnemyScript
{
    public class SpawnScript
    {
        private EnemyScriptMain Manager;
        private ScriptGroup Scripts;
        public float WaitTime;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public SpawnScript(EnemyScriptMain mng, ScriptGroup group, string keyword = null)
        {
            Manager = mng;
            Scripts = group;

            FindRunScript("common");
            if (string.IsNullOrEmpty(keyword)) keyword = "default";
            FindRunScript(keyword);
        }

        /// *******************************************************
        /// <summary>タグ名検索＋実行</summary>
        /// *******************************************************
        private bool FindRunScript(string keyword)
        {
            WaitTime = -1;
            ScriptLine script_line = Scripts.ScriptLine.Find(line => line.CommandName.CompareTo(keyword) == 0);
            if (script_line != null)
            {
                PlayLine(Manager.Character, script_line);
                return true;
            }

            return false;
        }

        /// *******************************************************
        /// <summary>コマンド実行</summary>
        /// *******************************************************
        private void PlayLine(EnemyControll character, ScriptLine line)
        {

            line.Attributes.ForEach(atr => {
                if (atr.Name.CompareTo("time") == 0)
                {
                    WaitTime = atr.FloatValue;
                }
                else
                {
                    Manager.OverrideParam(atr);
                }
            });
        }

    }
}