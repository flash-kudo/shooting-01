using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class CharacterControll : MonoBehaviour
    {
        [SerializeField]
        public Vector2 Position;

        [SerializeField]
        public float Direction = 0;
        [SerializeField]
        public float MoveSpeed = 0;

        [SerializeField]
        public float RotateSpeed = 0;
        [SerializeField]
        public float Accelerate = 0;


        [SerializeField]
        public float ColliderScale = 0.1f;
        [SerializeField]
        public Vector2 ColliderPosition;


        [SerializeField]
        public CharacterTyep CharType;

        private BarrageCollider BarrageCollider;

        private Vector3 BaseAngle;


        private List<CharacterControll> HitTarget;

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        void Start()
        {
            InitMove();
            InitHitCheck();
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        public void Update()
        {
            if (HitCheck() == false)
            {
                ProcMove();
                EntryHitCheck();
            }
        }

        /// *******************************************************
        /// <summary>Gizmo描画</summary>
        /// *******************************************************
        void OnDrawGizmos()
        {
            Color prev_color = Gizmos.color;

            EntryHitCheck();
            Color gizcol = (CharType == CharacterTyep.NONE) ? Color.green : (CharType == CharacterTyep.ENEMY) ? Color.red : Color.blue;
            BarrageCollider.DrawGizmoLine(gizcol);

            Gizmos.color = prev_color;
        }

        /// *******************************************************
        /// <summary>移動初期処理</summary>
        /// *******************************************************
        protected virtual void InitMove()
        {
            BaseAngle = transform.eulerAngles;
        }

        /// *******************************************************
        /// <summary>移動処理</summary>
        /// *******************************************************
        protected virtual void ProcMove()
        {
            MoveSpeed += Accelerate;
            Direction = ((Direction + RotateSpeed + 180f) % 360f) - 180f;

            Position.x = Position.x + Mathf.Sin(Direction * Mathf.Deg2Rad) * MoveSpeed;
            Position.y = Position.y + Mathf.Cos(Direction * Mathf.Deg2Rad) * MoveSpeed;

            transform.localPosition = Position;
            transform.localEulerAngles = BaseAngle + new Vector3(0, 0, -Direction);

            if (Position.x < -4) RemoveField();
            if (Position.x > 4) RemoveField();
            if (Position.y < -6) RemoveField();
            if (Position.y > 6) RemoveField();
        }

        /// *******************************************************
        /// <summary>破棄処理</summary>
        /// *******************************************************
        private void RemoveField()
        {
            Destroy(gameObject);
        }

        /// *******************************************************
        /// <summary>中央位置</summary>
        /// *******************************************************
        public Vector3 WorldPosition 
        {
            get { return transform.position; }
        }

        // ########################################################

        /// <summary>境界最小値X座標</summary>
        public float BoundsMinX { get { return BarrageCollider.BoundsMinX; } }
        /// <summary>境界最小値Y座標</summary>
        public float BoundsMinY { get { return BarrageCollider.BoundsMinY; } }
        /// <summary>境界最大値X座標</summary>
        public float BoundsMaxX { get { return BarrageCollider.BoundsMaxX; } }
        /// <summary>境界最大値Y座標</summary>
        public float BoundsMaxY { get { return BarrageCollider.BoundsMaxY; } }

        /// *******************************************************
        /// <summary>当たり判定初期処理</summary>
        /// *******************************************************
        protected virtual void InitHitCheck()
        {
            BarrageCollider = new BarrageCollider();
            HitTarget = new List<CharacterControll>();
        }

        /// *******************************************************
        /// <summary>当たり判定準備</summary>
        /// *******************************************************
        protected virtual void EntryHitCheck()
        {
            if (BarrageCollider == null) BarrageCollider = new BarrageCollider();
            BarrageCollider.ColliderScale = ColliderScale;
            BarrageCollider.ColliderPosition = ColliderPosition;
            BarrageCollider.UpdatePosition(transform);

            if (CollisionManager.Instance == null) return;

            CollisionManager.Instance.EntryHitCheck(this);
        }

        /// *******************************************************
        /// <summary>当たり判定テスト</summary>
        /// <param name="target">判定対象</param>
        /// <returns>true:当たった/false:当たってない</returns>
        /// *******************************************************
        public bool HitTest(CharacterControll target)
        {
            return BarrageCollider.HitTest(target.BarrageCollider);
        }

        /// *******************************************************
        /// <summary>当たり対象追加</summary>
        /// <param name="target">当たり対象</param>
        /// *******************************************************
        public virtual void AddHitTarget(CharacterControll target)
        {
            HitTarget.Add(target);
        }

        /// *******************************************************
        /// <summary>当たり対象があるか</summary>
        /// <returns>true:ある/false:無い</returns>
        /// *******************************************************
        protected virtual bool HitCheck()
        {
            if (HitTarget == null) return false;
            if (HitTarget.Count == 0) return false;
            RemoveField();
            return true;
        }

    }
}
