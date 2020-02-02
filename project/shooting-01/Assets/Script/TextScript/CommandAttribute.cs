using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextScript
{
    public class CommandAttribute
    {
        public string Name;
        public List<ScriptSentence> DataSrc;

        /// *******************************************************
        /// <summary>コンストラクタ（解析）</summary>
        /// *******************************************************
        public CommandAttribute(string name)
        {
            Name = name;
            DataSrc = new List<ScriptSentence>();
        }
        /// *******************************************************
        /// <summary>文字列出力</summary>
        /// *******************************************************
        public void AddData(ScriptSentence data)
        {
            DataSrc.Add(data);
        }

        /// *******************************************************
        /// <summary>float表現</summary>
        /// *******************************************************
        public float FloatValue
        {
            get
            {
                if (DataSrc.Count == 0) return 0;
                if (DataSrc.Count == 1) return ParseFloat(0);
                else
                {
                    float min = ParseFloat(0);
                    float max = ParseFloat(1);
                    if (GameManager.Instance == null) return 0;
                    return Mathf.Lerp(min, max, GameManager.Instance.PlayerLevelRate());
                }
            }
        }

        /// *******************************************************
        /// <summary>int表現</summary>
        /// *******************************************************
        public int IntValue
        {
            get
            {
                return Mathf.FloorToInt(FloatValue);
            }
        }

        /// *******************************************************
        /// <summary>string表現</summary>
        /// *******************************************************
        public string StringValue
        {
            get
            {
                if (DataSrc.Count == 0) return null;
                return DataSrc[0].SourceStr;
            }
        }

        /// *******************************************************
        /// <summary>Float解析</summary>
        /// *******************************************************
        private float ParseFloat(int index)
        {
            float ans = 0;
            if (float.TryParse(DataSrc[0].SourceStr, out ans) == true) return ans;
            return 0;
        }

        /// *******************************************************
        /// <summary>文字列出力</summary>
        /// *******************************************************
        public override string ToString()
        {
            string log = "";
            log += Name + ":( ";
            DataSrc.ForEach(_ => log += _.ToString());
            log += ")";
            return log;
        }
    }
}