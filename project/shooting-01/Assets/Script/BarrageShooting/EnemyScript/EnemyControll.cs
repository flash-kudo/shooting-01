using BarrageShooting.EnemyScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public enum TargetType
    {
        DIRECTION_POSITION,
        DIRECTION_TARGET,
        DIRECTION_ANGLE,
        DIRECTION_BOTTOM,
        DIRECTION_TOP,
        DIRECTION_LEFT,
        DIRECTION_RIGHT,
    }

    public enum SpawnPosition
    {
        NONE,
        LEFT,
        RIGHT,
    }


    public class EnemyControll : CharacterControll
    {
        // [SerializeField]
        // public Vector2 Position;

        // [SerializeField]
        // public float Direction = 0;
        // [SerializeField]
        // public float MoveSpeed = 0;
        // [SerializeField]
        // public float MaxSpeed = 1;

        // [SerializeField]
        // public float RotateSpeed = 0;
        // [SerializeField]
        // public float Accelerate = 0;

        // [SerializeField]
        // public float ShiftSpeed = 0;
        // [SerializeField]
        // public float ShiftAngle = 0;
        // [SerializeField]
        // public float ShiftDamp = 0;

        // [SerializeField]
        // public float ColliderScale = 0.1f;
        // [SerializeField]
        // public Vector2 ColliderPosition;

        // [SerializeField]
        // public float ToughPoint = 1;
        // [SerializeField]
        // public float AttackPoint = 10;

        [SerializeField]
        public float MaxRotateSpeed;

        [SerializeField]
        public TargetType TargetDirection;

        [SerializeField]
        public float ScriptTargetAngle;
        [SerializeField]
        public Vector2 TargetPosition;
        [SerializeField]
        public CharacterControll TargetCharacter;

        [SerializeField]
        public TextAsset Script;
        [SerializeField]
        public string SpawnKey;

        [HideInInspector]
        public InitialData Initial;
        [HideInInspector]
        public SpawnPosition SpawnSide;
        protected EnemyScriptMain ScriptMain;

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        protected override void OnStart()
        {
            CharType = CharacterTyep.ENEMY;
            if(TargetPosition == null) TargetPosition = new Vector2();
            ScriptMain = new EnemyScriptMain(this, Initial, SpawnKey);

            if (Initial != null) SetInitial(Initial);

            if(StageManager.Instance != null) StageManager.Instance.AddEnemyList(this);

            base.OnStart();

            if (Script != null) ScriptMain.ReadScriptText(Script.text);
            SpawnSide = (Position.x < 0) ? SpawnPosition.LEFT : SpawnPosition.RIGHT;
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        protected override void OnUpdate()
        {
            SetNextAngle();
            base.OnUpdate();
            if(ScriptMain != null) ScriptMain.OnUpdate();
        }

        /// *******************************************************
        /// <summary>角度計算</summary>
        /// *******************************************************
        private void SetNextAngle()
        {
            float target = TargetAngle();

            float distance = target - Direction;
            while (distance > 180) distance -= 360;
            while (distance < -180) distance += 360;

            float speed = Mathf.Min(Mathf.Abs(MaxRotateSpeed), Mathf.Abs(distance));

            if (distance < 0)
            {
                Direction -= speed;
            }
            else
            {
                Direction += speed;
            }
        }

        /// *******************************************************
        /// <summary>初期データ登録</summary>
        /// *******************************************************
        private void SetInitial(InitialData initial)
        {
            if (initial.Override_Direction) Direction = initial.Direction;
            if (initial.Override_ShiftSpeed) ShiftSpeed = initial.ShiftSpeed;
            if (initial.Override_ShiftAngle) ShiftAngle = initial.ShiftAngle;
            if (initial.Override_ShiftDamp) ShiftDamp = initial.ShiftDamp;
        }


        /// *******************************************************
        /// <summary>目標角度算出</summary>
        /// *******************************************************
        private float TargetAngle()
        {
            Vector2 dp;

            switch (TargetDirection)
            {
                case TargetType.DIRECTION_TARGET:
                    TargetPosition = TargetCharacter.Position;
                    dp = TargetPosition - Position;
                    return Mathf.Atan2(dp.x, dp.y) * Mathf.Rad2Deg;
                case TargetType.DIRECTION_POSITION:
                    dp = TargetPosition - Position;
                    return Mathf.Atan2(dp.x, dp.y) * Mathf.Rad2Deg;
                case TargetType.DIRECTION_ANGLE: return ScriptTargetAngle;
                case TargetType.DIRECTION_BOTTOM: return 180;
                case TargetType.DIRECTION_TOP: return 0;
                case TargetType.DIRECTION_LEFT: return 270;
                case TargetType.DIRECTION_RIGHT: return 90;
            }

            return 180;
        }

        /// *******************************************************
        /// <summary>破棄処理</summary>
        /// *******************************************************
        protected override void RemoveField()
        {
            if (StageManager.Instance != null) StageManager.Instance.RemoveEnemyList(this);
            base.RemoveField();
        }

    }
}