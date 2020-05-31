using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class GranadeControll : CharacterControll
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

        private int Step = 0;
        private float StepPast = 0;

        [SerializeField]
        public float TargetDistance = 0;

        [Range(10f, 100f)]
        public float MoveDulation = 50f;
        [Range(0.01f, 2)]
        public float BlastDuration = 1f;

        public float ColliderSpeed = 0.01f;
        public float ColliderMax = 0.5f;

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        protected override void OnStart()
        {
            CharType = CharacterTyep.SELF;

            MoveSpeed = TargetDistance / ((MoveDulation / 100f) * FRAME_RATE);

            Step = 0;
            base.OnStart();
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        protected override void OnUpdate()
        {
            HitCheck();

            StepPast += FRAME_TIME;

            switch (Step)
            {
                case 0:
                    ProcMove();
                    if (StepPast > (MoveDulation / 100f))
                    {
                        StepPast = 0;
                        Step++;
                    }
                    break;
                case 1:
                    ColliderScale = Mathf.Min(ColliderScale + ColliderSpeed, ColliderMax);
                    EntryHitCheck();
                    if (StepPast > BlastDuration)
                    {
                        StepPast = 0;
                        Step++;
                    }
                    break;
                case 2:
                    RemoveField();
                    break;
            }

        }

        /// *******************************************************
        /// <summary>当たり対象があるか</summary>
        /// <returns>true:ある/false:無い</returns>
        /// *******************************************************
        protected override bool HitCheck()
        {
            if (HitTarget == null) return false;
            HitTarget.Clear();
            return false;
        }
    }
}
