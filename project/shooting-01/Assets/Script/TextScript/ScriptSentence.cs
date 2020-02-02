using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextScript
{
    public enum CHAR_TYPE
    {
        CALC_PLUS,
        CALC_MINUS,
        CALC_STAR,
        CALC_SLASH,
        CALC_EQUAL,
        DOUBLE_QUOTATION,
        PARENTHESIS_OPEN,
        PARENTHESIS_CLOSE,
        BRACE_OPEN,
        BRACE_CLOSE,
        BRACKET_OPEN,
        BRACKET_CLOSE,

        STRING = 10000,
    }

    public class ScriptSentence
    {
        public const string CALCURATE_SIGN = "+-*/=\"(){}[]";

        public string SourceStr;
        public CHAR_TYPE Type;

        /// *******************************************************
        /// <summary>センテンス解析・分類</summary>
        /// *******************************************************
        public ScriptSentence(string str)
        {
            SourceStr = str.ToLower();
            Type = CHAR_TYPE.STRING;

            if (SourceStr.Length == 1)
            {
                int type = CALCURATE_SIGN.IndexOf(SourceStr);
                if (type >= 0) Type = (CHAR_TYPE)type;
            }
        }

        /// *******************************************************
        /// <summary>文字列出力</summary>
        /// *******************************************************
        public override string ToString()
        {
            return SourceStr + " ";
        }
    }
}