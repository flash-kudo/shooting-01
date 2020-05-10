using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TextScript
{
    public class ScriptLine
    {
        public string CommandName;

        public List<CommandAttribute> Attributes;

        /// *******************************************************
        /// <summary>コンストラクタ（解析）</summary>
        /// *******************************************************
        public ScriptLine(string str)
        {
            Attributes = new List<CommandAttribute>();

            List<ScriptSentence> sentences = ParseToSentence(str);

            CommandName = null;
            if (sentences.Count == 0) return;
            if (sentences[0].Type != CHAR_TYPE.STRING) return;
            CommandName = sentences[0].SourceStr;
            sentences.RemoveAt(0);

            while(sentences.Count > 0)
            {
                if(sentences[0].Type != CHAR_TYPE.STRING)
                {
                    sentences.RemoveAt(0);
                    continue;
                }

                var attdat = new List<ScriptSentence>();
                CommandAttribute att = new CommandAttribute(sentences[0].SourceStr);
                Attributes.Add(att);
                sentences.RemoveAt(0);

                if (sentences.Count == 0) break;

                if (sentences[0].Type != CHAR_TYPE.CALC_EQUAL) continue;
                sentences.RemoveAt(0);

                if (sentences[0].Type == CHAR_TYPE.STRING)
                {
                    att.AddData(sentences[0]);
                    sentences.RemoveAt(0);
                    continue;
                }
                if ((sentences.Count >= 2) &&
                    (sentences[0].Type == CHAR_TYPE.CALC_MINUS) &&
                    (sentences[1].Type == CHAR_TYPE.STRING))
                {
                    sentences[1].SourceStr = "-" + sentences[1].SourceStr;
                    att.AddData(sentences[1]);
                    sentences.RemoveRange(0, 2);
                    continue;
                }
                if ((sentences.Count >= 5) &&
                    (sentences[0].Type == CHAR_TYPE.BRACKET_OPEN) &&
                    (sentences[1].Type == CHAR_TYPE.STRING) &&
                    (sentences[2].Type == CHAR_TYPE.COMMA) &&
                    (sentences[3].Type == CHAR_TYPE.STRING) &&
                    (sentences[4].Type == CHAR_TYPE.BRACKET_CLOSE))
                {
                    att.AddData(sentences[1]);
                    att.AddData(sentences[3]);
                    sentences.RemoveRange(0, 5);
                    continue;
                }
                if ((sentences.Count >= 6) &&
                    (sentences[0].Type == CHAR_TYPE.BRACKET_OPEN) &&
                    (sentences[1].Type == CHAR_TYPE.CALC_MINUS) &&
                    (sentences[2].Type == CHAR_TYPE.STRING) &&
                    (sentences[3].Type == CHAR_TYPE.COMMA) &&
                    (sentences[4].Type == CHAR_TYPE.STRING) &&
                    (sentences[5].Type == CHAR_TYPE.BRACKET_CLOSE))
                {
                    sentences[2].SourceStr = "-" + sentences[2].SourceStr;
                    att.AddData(sentences[2]);
                    att.AddData(sentences[4]);
                    sentences.RemoveRange(0, 6);
                    continue;
                }
                if ((sentences.Count >= 6) &&
                    (sentences[0].Type == CHAR_TYPE.BRACKET_OPEN) &&
                    (sentences[1].Type == CHAR_TYPE.STRING) &&
                    (sentences[2].Type == CHAR_TYPE.COMMA) &&
                    (sentences[3].Type == CHAR_TYPE.CALC_MINUS) &&
                    (sentences[4].Type == CHAR_TYPE.STRING) &&
                    (sentences[5].Type == CHAR_TYPE.BRACKET_CLOSE))
                {
                    sentences[4].SourceStr = "-" + sentences[4].SourceStr;
                    att.AddData(sentences[1]);
                    att.AddData(sentences[4]);
                    sentences.RemoveRange(0, 6);
                    continue;
                }
                if ((sentences.Count >= 7) &&
                    (sentences[0].Type == CHAR_TYPE.BRACKET_OPEN) &&
                    (sentences[1].Type == CHAR_TYPE.CALC_MINUS) &&
                    (sentences[2].Type == CHAR_TYPE.STRING) &&
                    (sentences[3].Type == CHAR_TYPE.COMMA) &&
                    (sentences[4].Type == CHAR_TYPE.CALC_MINUS) &&
                    (sentences[5].Type == CHAR_TYPE.STRING) &&
                    (sentences[6].Type == CHAR_TYPE.BRACKET_CLOSE))
                {
                    sentences[2].SourceStr = "-" + sentences[2].SourceStr;
                    sentences[5].SourceStr = "-" + sentences[5].SourceStr;
                    att.AddData(sentences[2]);
                    att.AddData(sentences[5]);
                    sentences.RemoveRange(0, 7);
                    continue;
                }
                sentences.RemoveAt(0);
            }
        }

        /// *******************************************************
        /// <summary>コンストラクタ（内部用）</summary>
        /// *******************************************************
        private ScriptLine()
        {

        }

        /// *******************************************************
        /// <summary>アトリビュート取得</summary>
        /// *******************************************************
        public CommandAttribute GetAttribute(string key)
        {
            if (Attributes == null) return null;
            return Attributes.Find(atr => { return (atr.Name.CompareTo(key) == 0); });
        }

        /// *******************************************************
        /// <summary>複製</summary>
        /// *******************************************************
        public ScriptLine Duplicate()
        {
            ScriptLine line = new ScriptLine();
            line.CommandName = CommandName;
            List<CommandAttribute> atr_list = new List<CommandAttribute>();
            Attributes.ForEach(atr => { atr_list.Add(atr.Duplicate()); });
            line.Attributes = atr_list;
            return line;
        }

        /// *******************************************************
        /// <summary>センテンス解析</summary>
        /// *******************************************************
        private List<ScriptSentence> ParseToSentence(string str)
        {
            var sentences = new List<ScriptSentence>();

            InitRegPaturn();
            str = Regex.Replace(str, SighRegPaturn, " $0 ", RegexOptions.Singleline);
            str = Regex.Replace(str, "\\s+", " ", RegexOptions.Singleline);

            List<string> stc = new List<string>(str.Split(' '));

            stc.ForEach(_ => sentences.Add(new ScriptSentence(_)));

            return sentences;
        }

        private static string SighRegPaturn = null;

        /// *******************************************************
        /// <summary>正規表現パターンの初期化</summary>
        /// *******************************************************
        private static void InitRegPaturn()
        {
            if (SighRegPaturn != null) return;

            SighRegPaturn = "";
            List<char> chars = new List<char>(ScriptSentence.CALCURATE_SIGN.ToCharArray());

            chars.ForEach(_ => SighRegPaturn += "\\" + _);

            SighRegPaturn = "([" + SighRegPaturn + "])";
        }

        /// *******************************************************
        /// <summary>文字列出力</summary>
        /// *******************************************************
        public override string ToString()
        {
            string log = "";
            log += "<" + CommandName + ">";
            Attributes.ForEach(_ => log += _.ToString());
            return log;
        }
    }
}
