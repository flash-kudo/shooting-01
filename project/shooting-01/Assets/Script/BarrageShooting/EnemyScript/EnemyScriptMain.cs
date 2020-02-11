using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextScript;

namespace BarrageShooting.EnemyScript
{
    public class EnemyScriptMain : TextScriptCore
    {
        private const string SPAWN_SCRIPT = "spawn";
        private const string MOVE_SCRIPT = "move";

        public EnemyControll Character;
        public MoveScript MovePlayer;

        public string SpawnKey;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public EnemyScriptMain(EnemyControll character, string key)
        {
            Character = character;
            SpawnKey = key;
        }

        /// *******************************************************
        /// <summary>ファイル読み込み</summary>
        /// *******************************************************
        public void ReadScriptFile(string path)
        {
            ReadFile(path);
            ParseScript();
        }
        /// *******************************************************
        /// <summary>テキスト読み込み</summary>
        /// *******************************************************
        public void ReadScriptText(string path)
        {
            ReadText(path);
            ParseScript();
        }
        /// *******************************************************
        /// <summary>スクリプト解析</summary>
        /// *******************************************************
        private void ParseScript()
        {
            ScriptGroup spawn = Group.Find(grp => grp.GroupName == SPAWN_SCRIPT);
            ScriptGroup move = Group.Find(grp => grp.GroupName == MOVE_SCRIPT);
            if (spawn != null) new SpawnScript(this, spawn, SpawnKey);
            if (move != null) MovePlayer = new MoveScript(this, move);
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        public void OnUpdate()
        {
            if (MovePlayer != null) MovePlayer.OnUpdate(Character);
        }

        /// *******************************************************
        /// <summary>共通パラメータ更新</summary>
        /// *******************************************************
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
                case "trg_pos_x": Character.TargetPosition.x = 
                        (Character.SpawnSide == SpawnPosition.LEFT)? 
                        attribute.FloatValue : -attribute.FloatValue; break;
                case "trg_pos_y": Character.TargetPosition.y = attribute.FloatValue; break;
            }
        }

    }
}
