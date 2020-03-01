using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextScript
{
    public class ScriptGroup
    {
        public string GroupName;

        public List<ScriptLine> ScriptLine;

        /// *******************************************************
        /// <summary>コンストラクタ（解析）</summary>
        /// *******************************************************
        public ScriptGroup(string name, string body)
        {
            GroupName = name.ToLower();
            Parse(body);
        }

        /// *******************************************************
        /// <summary>コンストラクタ（内部用）</summary>
        /// *******************************************************
        private ScriptGroup(string name)
        {
            GroupName = name;
        }
        /// *******************************************************
        /// <summary>解析</summary>
        /// *******************************************************
        private void Parse(string src)
        {
            ScriptLine = new List<ScriptLine>();

            int st = 0;
            int et = 0;

            while (et <= src.Length)
            {
                st = src.IndexOf('<', et);
                if (st < 0) break;
                et = src.IndexOf('>', st);
                if (et < 0) break;

                string body = src.Substring(st + 1, et - st - 1).Trim();

                if(body.Length > 0) ScriptLine.Add(new ScriptLine(body));
            }

        }

        /// *******************************************************
        /// <summary>複製</summary>
        /// *******************************************************
        public ScriptGroup Duplicate()
        {
            ScriptGroup group = new ScriptGroup(GroupName);
            List<ScriptLine> line_list = new List<ScriptLine>();
            ScriptLine.ForEach(line => { line_list.Add(line.Duplicate()); });
            group.ScriptLine = line_list;
            return group;
        }

        /// *******************************************************
        /// <summary>行精査</summary>
        /// *******************************************************
        public void ActivateLine(Func<ScriptLine,bool> use_line)
        {
            List<ScriptLine> line_list = new List<ScriptLine>();
            ScriptLine.ForEach(line => {
                bool is_use = use_line(line);
                if (is_use) line_list.Add(line);
            });
            ScriptLine = line_list;
        }

        /// *******************************************************
        /// <summary>文字列出力</summary>
        /// *******************************************************
        public override string ToString()
        {
            string log = "";
            log += "group:<" + GroupName + ">\n";
            ScriptLine.ForEach(_ => log += _.ToString() + "\n");
            return log;
        }
    }
}
