using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TextScript
{
    public class TextScriptCore
    {
        public List<ScriptGroup> Group;

        /// *******************************************************
        /// <summary>ファイル読み込み</summary>
        /// *******************************************************
        public void ReadFile(string path)
        {
            TextAsset textasset = new TextAsset();
            textasset = Resources.Load(path, typeof(TextAsset)) as TextAsset;

            if (textasset == null)
            {
                Debug.LogError("ファイルパスが不正です。:\"" + path + "\"");
                return;
            }

            string texts = textasset.text;

            ReadText(texts);
        }

        /// *******************************************************
        /// <summary>テキスト組み込み</summary>
        /// *******************************************************
        public void ReadText(string source)
        {
            source = CutoutComments(source);

            Parse(source);

        }

        /// *******************************************************
        /// <summary>解析</summary>
        /// *******************************************************
        private void Parse(string src)
        {
            Group = new List<ScriptGroup>();

            int st = 0;
            int et = 0;
            int sb = 0;

            while (sb <= src.Length)
            {
                st = src.IndexOf('{', sb);
                if (st < 0) break;
                et = src.IndexOf('}', st);
                if (et < 0) break;

                string name = src.Substring(st + 1, et - st - 1).Trim();

                sb = src.IndexOf('{', et);
                if (sb < 0) sb = src.Length;

                string body = src.Substring(et + 1, sb - et - 1).Trim();

                if ((name.Length > 0) && (body.Length > 0))
                {
                    Group.Add(new ScriptGroup(name, body));
                }
            }


            //Group.ForEach(_ => Debug.Log(_.ToString()));
        }

        /// *******************************************************
        /// <summary>コメント破棄</summary>
        /// *******************************************************
        private string CutoutComments(string src)
        {
            src = Regex.Replace(src, "\\/\\/.*$", "", RegexOptions.Multiline);
            src = Regex.Replace(src, "\\/\\*[^(*/)]*\\*\\/", "", RegexOptions.Singleline);

            return src;
        }
    }
}