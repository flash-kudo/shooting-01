using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
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
        public Collider2D Collider;


        [SerializeField]
        public CharacterTyep CharType;

        private Vector3 BaseAngle;


        private List<CharacterControll> HitTarget;

        // Start is called before the first frame update
        void Start()
        {
            InitMove();
            InitHitCheck();
        }

        // Update is called once per frame
        public void Update()
        {
            if (HitCheck() == false)
            {
                ProcMove();
                EntryHitCheck();
            }
        }

        protected virtual void InitMove()
        {
            BaseAngle = transform.eulerAngles;
        }

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

        private void RemoveField()
        {
            Destroy(gameObject);
        }

        public Vector3 WorldPosition 
        {
            get { return transform.position; }
        }

        protected virtual void InitHitCheck()
        {
            HitTarget = new List<CharacterControll>();
        }

        protected virtual void EntryHitCheck()
        {
            if (CollisionManager.Instance == null) return;
            CollisionManager.Instance.EntryHitCheck(this);
        }
        public virtual void AddHitTarget(CharacterControll target)
        {
            HitTarget.Add(target);
        }
        protected virtual bool HitCheck()
        {
            if (HitTarget == null) return false;
            if (HitTarget.Count == 0) return false;
            RemoveField();
            return true;
        }

    }
}
