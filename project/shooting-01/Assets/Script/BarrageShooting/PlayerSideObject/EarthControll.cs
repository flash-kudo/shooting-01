using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class EarthControll : CharacterControll
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

        private int HIT_SETTIME = 3;
        public int HitmarkTime = 0;
        public SpriteRenderer EarthRenderer;

        // Start is called before the first frame update
        void Start()
        {
            AttackPoint = float.PositiveInfinity;
            base.OnStart();
        }

        // Update is called once per frame
        protected override void OnUpdate()
        {
            if(HitmarkTime > 0)
            {
                EarthRenderer.color = Color.red;
                HitmarkTime--;
            }
            else
            {
                EarthRenderer.color = Color.white;
            }
            base.OnUpdate();
        }

        /// *******************************************************
        /// <summary>当たり対象があるか</summary>
        /// <returns>true:ある/false:無い</returns>
        /// *******************************************************
        protected override bool HitCheck()
        {
            if (HitTarget == null) return false;
            if (HitTarget.Count == 0) return false;

            HitmarkTime = HIT_SETTIME;

            HitTarget.ForEach(trg => {
                ToughPoint -= trg.AttackPoint;
            });
            if (ToughPoint < 0)
            {
                // gameover
                return true;
            }
            HitTarget.Clear();
            return false;
        }

    }
}
