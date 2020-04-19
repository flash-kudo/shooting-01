using BarrageShooting.EnemyScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class FortressControll : CharacterControll
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


        public StaticDirection DirectionSetter;
        public float FortressDirection = 0;
        public float FortressScale = 1.7f;
        public float TunnelScale = 0.28f;

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        protected override void OnStart()
        {
            CharType = CharacterTyep.ENEMY;

            Position.x = 0;

            base.OnStart();
        }

        private void OnEnable()
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.FortressHitCheck = FortressHisCheck;
            }
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.FortressHitCheck = null;
            }
        }

        public bool FortressHisCheck(Vector2 position, float scale)
        {
            float dist = Vector2.Distance(position, Position);
            if (dist > (FortressScale + scale)) return false;

            Vector2[] rect = TunnelLine();
            int cnt = 0;
            for (int i = 0; i < 4; ++i)
            {
                float x1 = rect[(i + 1) % 4].x - rect[i].x;
                float y1 = rect[(i + 1) % 4].y - rect[i].y;
                float x2 = position.x - rect[i].x;
                float y2 = position.y - rect[i].y;
                if (x1 * y2 - x2 * y1 < 0)
                {
                    ++cnt;
                }
                else
                {
                    --cnt;
                }
            }
            return (cnt != 4) && (cnt != -4);
        }

        private Vector2[] TunnelLine()
        {
            Vector2[] pos = new Vector2[4];

            float scale = FortressScale + 0.1f;

            float rad = Mathf.Deg2Rad * (180 - FortressDirection);
            Vector2 width = new Vector2(-TunnelScale * Mathf.Cos(rad), TunnelScale * Mathf.Sin(rad));
            Vector2 line = new Vector2(scale * Mathf.Sin(rad), scale * Mathf.Cos(rad));

            pos[0] = Position + width;
            pos[1] = Position - width;
            pos[2] = Position - width + line;
            pos[3] = Position + width + line;

            return pos;
        }

#if UNITY_EDITOR
        /// *******************************************************
        /// <summary>Gizmo描画</summary>
        /// *******************************************************
        protected override void OnGizmo()
        {
            base.OnGizmo();

            Color prev_color = Gizmos.color;

            Gizmos.color = Color.red;
            for (int i = 0; i < 72; i++)
            {
                Vector3 pos1 = new Vector3(
                    Mathf.Sin(Mathf.Deg2Rad * (i + 0) * 5) * FortressScale + Position.x,
                    Mathf.Cos(Mathf.Deg2Rad * (i + 0) * 5) * FortressScale + Position.y,
                    0);
                Vector3 pos2 = new Vector3(
                    Mathf.Sin(Mathf.Deg2Rad * (i + 1) * 5) * FortressScale + Position.x,
                    Mathf.Cos(Mathf.Deg2Rad * (i + 1) * 5) * FortressScale + Position.y,
                    0);
                Gizmos.DrawLine(pos1, pos2);
            }

            Vector2[] tunnel = TunnelLine();

            Gizmos.DrawLine(tunnel[0], tunnel[3]);
            Gizmos.DrawLine(tunnel[1], tunnel[2]);

            Gizmos.color = prev_color;
        }
#endif

        /// *******************************************************
        /// <summary>位置処理</summary>
        /// *******************************************************
        protected override void UpodatePosition(Vector2 move)
        {
            Position = Position + move;

            DirectionSetter.Rotation = FortressDirection;

            UpdatePosition();

            // if (Position.x < -4) RemoveField();
            // if (Position.x > 4) RemoveField();
            // if (Position.y < -6) RemoveField();
            // if (Position.y > 6) RemoveField();
        }


    }

}
