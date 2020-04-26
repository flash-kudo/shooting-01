using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class HugeMeteorControll : EnemyControll
    {
        private bool IsUpperHit;

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        protected override void OnStart()
        {
            base.OnStart();
            IsUpperHit = false;
        }

        /// *******************************************************
        /// <summary>当たり対象があるか</summary>
        /// <returns>true:ある/false:無い</returns>
        /// *******************************************************
        protected override bool HitCheck()
        {
            if (HitTarget == null) return false;
            if (HitTarget.Count == 0) return false;

            HitTarget.ForEach(trg => {
                if (trg.Position.y >= Position.y) IsUpperHit = true;
                ToughPoint -= trg.AttackPoint;
            });
            if (IsUpperHit == true)
            {
                RemoveField();
                return true;
            }
            if (ToughPoint < 0)
            {
                RemoveField();
                return true;
            }
            HitTarget.Clear();
            return false;
        }

        /// *******************************************************
        /// <summary>破棄処理</summary>
        /// *******************************************************
        protected override void RemoveField()
        {
            if(IsUpperHit == false) ScriptMain.MovePlayer.PlayNextLine(this);

            if (StageManager.Instance != null) StageManager.Instance.RemoveEnemyList(this);
            base.RemoveField();
        }

    }
}

