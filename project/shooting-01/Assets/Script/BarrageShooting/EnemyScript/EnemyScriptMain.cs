using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextScript;

namespace BarrageShooting.EnemyScript
{

    public class EnemyScriptMain : TextScriptCore
    {
        public const float SPEED_RATE = 0.001f;

        private const string SPAWN_SCRIPT = "spawn";
        private const string MOVE_SCRIPT = "move";

        public EnemyControll Character;
        public MoveScript MovePlayer;

        [HideInInspector]
        public InitialData Initial;

        public string SpawnKey;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public EnemyScriptMain(EnemyControll character, InitialData initial, string key)
        {
            Character = character;
            Initial = initial;
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
        public void ReadScriptText(string source)
        {
            ReadText(source);
            ParseScript();
        }
        /// *******************************************************
        /// <summary>スクリプト解析</summary>
        /// *******************************************************
        private void ParseScript()
        {
            ScriptGroup spawn = Group.Find(grp => grp.GroupName == SPAWN_SCRIPT);
            ScriptGroup move = Group.Find(grp => grp.GroupName == MOVE_SCRIPT);

            float wait = -1;

            if (spawn != null)
            {
                SpawnScript spawn_script = new SpawnScript(this, spawn, SpawnKey);
                wait = spawn_script.WaitTime;
            }
            if ((Initial != null) && (Initial.Override_WateTime == true)) wait = Initial.WateTime;
            if (move != null) MovePlayer = new MoveScript(this, move, SpawnKey, wait);
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
        public void OverrideParam(CommandAttribute attribute, bool inckude_key)
        {
            switch (attribute.Name)
            {
                case "pos_x": Character.Position.x = attribute.FloatValue; break;
                case "pos_y": Character.Position.y = attribute.FloatValue; break;
                case "dir": Character.Direction = attribute.FloatValue; break;
                case "spd": Character.MoveSpeed = attribute.FloatValue * SPEED_RATE; break;
                case "max_spd": Character.MaxSpeed = attribute.FloatValue * SPEED_RATE; break;
                case "rot_spd": Character.RotateSpeed = attribute.FloatValue; break;
                case "max_rot": Character.MaxRotateSpeed = attribute.FloatValue; break;
                case "acc": Character.Accelerate = attribute.FloatValue; break;
                case "trg_ang": Character.ScriptTargetAngle =
                    ((inckude_key == true) || (Character.SpawnSide == SpawnPosition.LEFT)) ?
                    attribute.FloatValue : (360 - attribute.FloatValue); break;
                case "trg_pos_x": Character.TargetPosition.x = 
                        ((inckude_key == true) || (Character.SpawnSide == SpawnPosition.LEFT))? 
                        attribute.FloatValue : -attribute.FloatValue; break;
                case "trg_pos_y": Character.TargetPosition.y = attribute.FloatValue; break;
            }
        }

    }
}
