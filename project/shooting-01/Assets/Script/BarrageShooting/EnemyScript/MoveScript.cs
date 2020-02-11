using System.Collections;
using System.Collections.Generic;
using TextScript;
using UnityEngine;

namespace BarrageShooting.EnemyScript
{
    public enum LIMIT_TYPE
    {
        TIME,
        NEAR,
        UNLIMITED,
    }

    public class MoveScript
    {
        private EnemyScriptMain Manager;
        private ScriptGroup Scripts;

        private int LineIndex;

        private LIMIT_TYPE Limit;

        private int PastTime;
        private int WaitTime;

        private List<ShotScript> Shots;
        private List<ShotScript> RemoveShots;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public MoveScript(EnemyScriptMain mng, ScriptGroup group)
        {
            Manager = mng;
            Scripts = group;

            Limit = LIMIT_TYPE.TIME;
            PastTime = 0;
            WaitTime = -1;
            LineIndex = -1;

            Shots = new List<ShotScript>();
            RemoveShots = new List<ShotScript>();
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        public void OnUpdate(EnemyControll character)
        {
            if (Scripts == null) return;
            if (Scripts.ScriptLine.Count == 0) return;

            if(Limit == LIMIT_TYPE.TIME)
            {
                PastTime++;
                if (PastTime > WaitTime) PlayNextLine(character);
            }
            if (Limit == LIMIT_TYPE.NEAR)
            {
                var dst = Vector2.Distance(character.TargetPosition, character.Position);
                if(dst < 0.5) PlayNextLine(character);
            }
            if (Shots != null)
            {
                Shots.ForEach(shot => shot.OnUpdate(character));
                RemoveShots.ForEach(shot => Shots.Remove(shot));
            }
        }

        /// *******************************************************
        /// <summary>次コマンド実行</summary>
        /// *******************************************************
        private void PlayNextLine(EnemyControll character)
        {
            LineIndex++;
            if (LineIndex >= Scripts.ScriptLine.Count) LineIndex = 0;
            PlayLine(character, Scripts.ScriptLine[LineIndex]);
            PastTime = 0;
        }

        /// *******************************************************
        /// <summary>コマンド実行</summary>
        /// *******************************************************
        private void PlayLine(EnemyControll character, ScriptLine line)
        {
            var last_limit = Limit;

            Limit = LIMIT_TYPE.NEAR;
            switch (line.CommandName)
            {
                case "position": character.TargetDirection = TargetType.DIRECTION_POSITION; break;
                case "target": character.TargetDirection = TargetType.DIRECTION_TARGET; break;
                case "angle": character.TargetDirection = TargetType.DIRECTION_ANGLE; break;
                case "bottom": character.TargetDirection = TargetType.DIRECTION_BOTTOM; break;
                case "top": character.TargetDirection = TargetType.DIRECTION_TOP; break;
                case "left": character.TargetDirection = TargetType.DIRECTION_LEFT; break;
                case "right": character.TargetDirection = TargetType.DIRECTION_RIGHT; break;
                case "shot": ShotCommand(character, line); return;
            }

            line.Attributes.ForEach(atr => { 
                if(RunAttribute(atr) == false)
                {
                    Manager.OverrideParam(atr);
                } 
            });
        }

        /// *******************************************************
        /// <summary>個別アトリビュート解析</summary>
        /// *******************************************************
        public bool RunAttribute(CommandAttribute attribute)
        {
            switch (attribute.Name)
            {
                case "time": WaitTime = attribute.IntValue; Limit = LIMIT_TYPE.TIME;  return true;
                case "unlimited": Limit = LIMIT_TYPE.UNLIMITED; return true;
            }
            return false;
        }

        /// *******************************************************
        /// <summary>射撃アトリビュート解析</summary>
        /// *******************************************************
        private void ShotCommand(EnemyControll character, ScriptLine line)
        {
            //string path = "";

            line.Attributes.ForEach(atr => {
                switch (atr.Name)
                {
                    case "time": WaitTime = atr.IntValue; Limit = LIMIT_TYPE.TIME; break;
                    //case "path": path = atr.StringValue; break;
                }
            });

            //GameObject prefab = (GameObject)Resources.Load(path);
            //GameObject blt_go = Object.Instantiate(prefab);
            //EnemyControll blt_ctrl = blt_go.GetComponent<EnemyControll>();
            //blt_ctrl.Position = character.Position;
            //blt_ctrl.Direction = character.Direction;
            //blt_go.SetActive(true);

            Shots.Add(new ShotScript(this, line));
        }

        public void RemoveShot(ShotScript shot)
        {
            RemoveShots.Add(shot);
        }

    }
}
