using System.Collections;
using System.Collections.Generic;
using TextScript;
using UnityEngine;

namespace BarrageShooting.EnemyScript
{
    public class MoveScript
    {
        private EnemyScriptMain Manager;
        private ScriptGroup Scripts;

        private int LineIndex;
        private int PastTime;
        private int WaitTime;

        public MoveScript(EnemyScriptMain mng, ScriptGroup group)
        {
            Manager = mng;
            Scripts = group;

            PastTime = 0;
            WaitTime = -1;
            LineIndex = -1;
        }

        public void OnUpdate(EnemyControll character)
        {
            if (Scripts == null) return;
            if (Scripts.ScriptLine.Count == 0) return;
            PastTime++;
            if(PastTime > WaitTime)
            {
                LineIndex++;
                if (LineIndex >= Scripts.ScriptLine.Count) LineIndex = 0;
                PlayLine(character, Scripts.ScriptLine[LineIndex]);
                PastTime = 0;
            }
        }

        private void PlayLine(EnemyControll character, ScriptLine line)
        {
            switch (line.CommandName)
            {
                case "position": character.TargetDirection = TargetType.DIRECTION_POSITION; break;
                case "target": character.TargetDirection = TargetType.DIRECTION_TARGET; break;
                case "angle": character.TargetDirection = TargetType.DIRECTION_ANGLE; break;
                case "bottom": character.TargetDirection = TargetType.DIRECTION_BOTTOM; break;
                case "top": character.TargetDirection = TargetType.DIRECTION_TOP; break;
                case "left": character.TargetDirection = TargetType.DIRECTION_LEFT; break;
                case "right": character.TargetDirection = TargetType.DIRECTION_RIGHT; break;
            }

            line.Attributes.ForEach(atr => { 
                if(RunAttribute(atr) == false)
                {
                    Manager.OverrideParam(atr);
                } 
            });
        }

        public bool RunAttribute(CommandAttribute attribute)
        {
            switch (attribute.Name)
            {
                case "time": WaitTime = attribute.IntValue; return true;
            }
            return false;
        }

    }
}
