using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class ShotControll : CharacterControll
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


        public static int NohitCount = 0;

        public float ShotSpeed = 15f;
        public float MirrorPower = 2.0f;

        public float ReflectPower = 2.0f;
        public float ReflectScorePower = 1.0f;

        public PlayerControll Player;

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        protected override void OnStart()
        {
            CharType = CharacterTyep.SELF;

            MoveSpeed = ShotSpeed / 100f;

            base.OnStart();
        }

        /// *******************************************************
        /// <summary>移動処理</summary>
        /// *******************************************************
        protected override void ProcMove()
        {
            Vector2 move = CalcurateMove();
            move = WallCheck(move);

            if((GameManager.Instance != null) &&  (GameManager.Instance.IsHitFortress(Position + move, ColliderScale) == true)){
                RemoveField();
            }

            UpodatePosition(move);
        }


        /// *******************************************************
        /// <summary>破棄処理</summary>
        /// *******************************************************
        protected override void RemoveField()
        {
            if (StageManager.Instance != null)
            {
                GameObject expload = StageManager.Instance.InstantiateObject("HitMark".ToLower());
                expload.transform.position = this.transform.position;
                expload.transform.rotation = this.transform.rotation;
                expload.SetActive(true);
            }

            if(IsAttacked == false)
            {
                NohitCount++;
                if(NohitCount >= 10)
                {
                    GameManager.Instance.AddExp(ExperienceData.ExpType.MISS_SHOT);
                    NohitCount = 0;
                }
            }
            else
            {
                NohitCount = 0;
            }

            Destroy(gameObject);
        }



        /// *******************************************************
        /// <summary>ミラー当たり判定処理</summary>
        /// *******************************************************
        private Vector2 WallCheck(Vector2 move)
        {
            for (int i = 0; i < Player.MirrirEdgeList.Count; i += 2)
            {
                Vector2 edge1 = Player.MirrirEdgeList[i + 0].transform.position;
                Vector2 edge2 = Player.MirrirEdgeList[i + 1].transform.position;

                Vector2 intersection;
                bool is_hit = IsHitWall(move, edge1, edge2, out intersection);
                if (is_hit == true)
                {
                    Direction = HitDirection(Mathf.Atan2(edge1.x - edge2.x, edge1.y - edge2.y) * Mathf.Rad2Deg);
                    move = Vector2.zero;
                    Position = intersection;
                    AttackPoint *= MirrorPower;
                    ReflectScorePower *= ReflectPower;
                    break;
                }
            }
            return move;
        }


        /// *******************************************************
        /// <summary>ミラー接触チェック</summary>
        /// *******************************************************
        private bool IsHitWall(Vector2 move, Vector2 edge1, Vector2 edge2, out Vector2 intersection)
        {
            intersection = Vector2.zero;
            Vector2 next = Position + move;

            var d = (next.x - Position.x) * (edge2.y - edge1.y) - (next.y - Position.y) * (edge2.x - edge1.x);

            if (d == 0.0f)
            {
                return false;
            }

            var u = ((edge1.x - Position.x) * (edge2.y - edge1.y) - (edge1.y - Position.y) * (edge2.x - edge1.x)) / d;
            var v = ((edge1.x - Position.x) * (next.y - Position.y) - (edge1.y - Position.y) * (next.x - Position.x)) / d;

            if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
            {
                return false;
            }

            intersection.x = Position.x + u * (next.x - Position.x);
            intersection.y = Position.y + u * (next.y - Position.y);

            if (intersection == Position) return false;

            return true;
        }

        /// *******************************************************
        /// <summary>ミラーヒット時方向転換</summary>
        /// *******************************************************
        private float HitDirection(float wall_dir)
        {
            float distance = (wall_dir - Direction + (720 + 180)) % 360 - 180;

            return wall_dir + distance;
        }

    }
}