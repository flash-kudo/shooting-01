using BarrageShooting;
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
                    float base_param = ParseFloat(0);
                    float add_param = ParseFloat(1);
                    if (StageManager.Instance == null) return base_param;
                    bool invert = (add_param < 0);
                    add_param = Mathf.Abs(add_param);

                    float rate = (add_param * GameManager.Instance.LevelNumber()) + 1.0f;

                    if(invert == false)
                    {
                        return base_param * rate;
                    }
                    else
                    {
                        return base_param / rate;
                    }

                    //return base_param + (add_param * StageManager.Instance.WavePlayerLevel);
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
        /// <summary>string表現が指定の文字列と同等か</summary>
        /// *******************************************************
        public bool CompareStrValue(string value)
        {
            if (DataSrc.Count == 0) return false;
            if (DataSrc[0].SourceStr == null) return false;

            return (DataSrc[0].SourceStr.CompareTo(value) == 0);
        }

        /// *******************************************************
        /// <summary>複製</summary>
        /// *******************************************************
        public CommandAttribute Duplicate()
        {
            CommandAttribute atr = new CommandAttribute(Name);
            List<ScriptSentence> stc_list = new List<ScriptSentence>();
            DataSrc.ForEach(stc => {
                stc_list.Add(stc.Duplicate());
            });
            atr.DataSrc = stc_list;
            return atr;
        }

        /// *******************************************************
        /// <summary>Float解析</summary>
        /// *******************************************************
        private float ParseFloat(int index)
        {
            float ans = 0;
            if (float.TryParse(DataSrc[index].SourceStr, out ans) == true) return ans;
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