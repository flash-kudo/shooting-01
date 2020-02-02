using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextScript;

namespace BarrageShooting.EnemyScript
{
    public class EnemyScriptMain : TextScriptCore
    {
        private const string MOVE_SCRIPT = "move";

        public EnemyControll Character;
        public MoveScript MovePlayer;

        public EnemyScriptMain(EnemyControll character)
        {
            Character = character;
        }

        public void ReadScriptFile(string path)
        {
            ReadFile(path);
            ParseScript();
        }
        public void ReadScriptText(string path)
        {
            ReadText(path);
            ParseScript();
        }
        private void ParseScript()
        {
            ScriptGroup move = Group.Find(grp => grp.GroupName == MOVE_SCRIPT);
            if(move != null) MovePlayer = new MoveScript(this, move);
        }

        public void OnUpdate()
        {
            if (MovePlayer != null) MovePlayer.OnUpdate(Character);
        }

        public void OverrideParam(CommandAttribute attribute)
        {
            switch (attribute.Name)
            {
                case "pos_x": Character.Position.x = attribute.FloatValue; break;
                case "pos_y": Character.Position.y = attribute.FloatValue; break;
                case "dir": Character.Direction = attribute.FloatValue; break;
                case "spd": Character.MoveSpeed = attribute.FloatValue; break;
                case "max_spd": Character.MaxSpeed = attribute.FloatValue; break;
                case "rot_spd": Character.RotateSpeed = attribute.FloatValue; break;
                case "max_rot": Character.MaxRotateSpeed = attribute.FloatValue; break;
                case "acc": Character.Accelerate = attribute.FloatValue; break;
                case "shift_spd": Character.ShiftSpeed = attribute.FloatValue; break;
                case "shift_ang": Character.ShiftAngle = attribute.FloatValue; break;
                case "shift_dmp": Character.ShiftDamp = attribute.FloatValue; break;
                case "trg_ang": Character.LeftTargetAngle = attribute.FloatValue; break;
                case "trg_pos_x": Character.TargetPosition.x = attribute.FloatValue; break;
                case "trg_pos_y": Character.TargetPosition.x = attribute.FloatValue; break;
            }
        }

    }
}
