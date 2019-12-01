using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class BarrageCollider
    {
        [SerializeField]
        public float ColliderScale = 0.1f;
        [SerializeField]
        public Vector2 ColliderPosition;

        private Vector2 _ColliderCenter;
        /// <summary>コリジョン中央位置</summary>
        public Vector2 ColliderCenter { get { return _ColliderCenter; } set { _ColliderCenter = value; } }

        /// <summary>コリジョン半径</summary>
        public float ColliderRadius { get { return Mathf.Abs(ColliderScale); } }

        /// <summary>境界最小値X座標</summary>
        public float BoundsMinX { get { return ColliderCenter.x - ColliderRadius; } }
        /// <summary>境界最小値Y座標</summary>
        public float BoundsMinY { get { return ColliderCenter.y - ColliderRadius; } }
        /// <summary>境界最大値X座標</summary>
        public float BoundsMaxX { get { return ColliderCenter.x + ColliderRadius; } }
        /// <summary>境界最大値Y座標</summary>
        public float BoundsMaxY { get { return ColliderCenter.y + ColliderRadius; } }

        /// *******************************************************
        /// <summary>コリジョン位置の更新</summary>
        /// <param name="trns">キャラTransform</param>
        /// *******************************************************
        public void UpdatePosition(Transform trns)
        {
            float pos_x =
                ColliderPosition.x * Mathf.Cos(Mathf.Deg2Rad * trns.eulerAngles.z) -
                ColliderPosition.y * Mathf.Sin(Mathf.Deg2Rad * trns.eulerAngles.z) +
                trns.position.x;
            float pos_y =
                ColliderPosition.x * Mathf.Sin(Mathf.Deg2Rad * trns.eulerAngles.z) +
                ColliderPosition.y * Mathf.Cos(Mathf.Deg2Rad * trns.eulerAngles.z) +
                trns.position.y;

            if (_ColliderCenter == null) _ColliderCenter = new Vector2();
            _ColliderCenter.x = pos_x;
            _ColliderCenter.y = pos_y;
        }

        /// *******************************************************
        /// <summary>当たり判定テスト</summary>
        /// <param name="target">判定対象</param>
        /// <returns>true:当たった/false:当たってない</returns>
        /// *******************************************************
        public bool HitTest(BarrageCollider target)
        {
            float dist = Vector2.Distance(target.ColliderCenter, ColliderCenter);
            return (dist < (target.ColliderRadius + ColliderRadius));
        }

        /// *******************************************************
        /// <summary>Gizmo用範囲描画</summary>
        /// <param name="gizmo_color">描画色</param>
        /// *******************************************************
        public void DrawGizmoLine(Color gizmo_color)
        {
            Gizmos.color = gizmo_color;
            for (int i = 0; i < 72; i++)
            {
                Vector3 pos1 = new Vector3(
                    Mathf.Sin(Mathf.Deg2Rad * (i + 0) * 5) * ColliderScale + ColliderCenter.x,
                    Mathf.Cos(Mathf.Deg2Rad * (i + 0) * 5) * ColliderScale + ColliderCenter.y,
                    0);
                Vector3 pos2 = new Vector3(
                    Mathf.Sin(Mathf.Deg2Rad * (i + 1) * 5) * ColliderScale + ColliderCenter.x,
                    Mathf.Cos(Mathf.Deg2Rad * (i + 1) * 5) * ColliderScale + ColliderCenter.y,
                    0);
                Gizmos.DrawLine(pos1, pos2);
            }
        }
    }
}
