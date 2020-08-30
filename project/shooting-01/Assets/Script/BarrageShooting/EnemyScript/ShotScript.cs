using System.Collections;
using System.Collections.Generic;
using TextScript;
using UnityEngine;

namespace BarrageShooting.EnemyScript
{
    public enum AngleType
    {
        RELATIVE,
        ABSOLUTE,
    }

    public class ShotScript
    {
        public MoveScript Manager;

        private string Source;

        private int ShotTime;
        private float Interval;
        private int StartCount;
        private int EndCount;
        private AngleType AngleBase;
        private float StartWidth;
        private float EndWidth;
        private float StartShiftAngle;
        private float EndShiftAngle;
        private int Delay;
        private float ShiftSpeed;
        private float ShiftDamp;

        private int CurrentShotCounts;
        private float PastInterval;

        /// *******************************************************
        /// <summary>コンストラクタ</summary>
        /// *******************************************************
        public ShotScript(MoveScript mng, ScriptLine line)
        {
            Manager = mng;

            CurrentShotCounts = 0;

            Source = null;
            ShotTime = 0;
            Interval = 1;
            StartCount = 1;
            EndCount = 1;
            AngleBase = AngleType.RELATIVE;
            StartWidth = 0;
            EndWidth = 0;
            StartShiftAngle = 0;
            EndShiftAngle = 0;
            Delay = -1;
            ShiftSpeed = 0;
            ShiftDamp = 0;

            line.Attributes.ForEach(atr => {
                switch (atr.Name)
                {
                    case "path": Source = atr.StringValue; break;
                    case "shot_time": ShotTime = atr.IntValue - 1; break;
                    case "interval": Interval = atr.FloatValue; break;
                    case "st_count": StartCount = atr.IntValue; break;
                    case "ed_count": EndCount = atr.IntValue; break;
                    case "angle_base": if(atr.StringValue == "absolute") AngleBase = AngleType.ABSOLUTE; break;
                    case "st_width": StartWidth = atr.FloatValue; break;
                    case "ed_width": EndWidth = atr.FloatValue; break;
                    case "st_sftang": StartShiftAngle = atr.FloatValue; break;
                    case "ed_sftang": EndShiftAngle = atr.FloatValue; break;
                    case "delay": Delay = atr.IntValue; break;
                    case "speed": ShiftSpeed = atr.FloatValue * EnemyScriptMain.SPEED_RATE; break;
                    case "damp": ShiftDamp = atr.FloatValue * EnemyScriptMain.SPEED_RATE; break;
                }
            });

            PastInterval = Interval;
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        public void OnUpdate(EnemyControll character)
        {
            PastInterval++;
            if (PastInterval >= Interval)
            {
                ShotSame(character);
                CurrentShotCounts++;
                PastInterval = 0;
                if(CurrentShotCounts > ShotTime)
                {
                    Manager.RemoveShot(this);
                }
            }

        }

        /// *******************************************************
        /// <summary>同時発射処理</summary>
        /// *******************************************************
        internal void ShotSame(EnemyControll character)
        {
            float rate = CurrentRate();
            int count = CurrentCount(rate) - 1;
            float width = CurrentWidth(rate);
            float half_width = -width / 2f;
            float sftwidth = CurrentShiftAngle(rate);
            float half_sftwidth = -sftwidth / 2f;


            if (count == 0)
            {
                ShotOne(character, 0, 0);
            }
            else
            {
                float tics = width / count;
                float sfttics = sftwidth / count;
                for (int i = 0; i <= count; i++)
                {
                    ShotOne(character, half_sftwidth + sfttics * i, half_width + tics * i);
                }
            }
        }

        /// *******************************************************
        /// <summary>単発発射処理</summary>
        /// *******************************************************
        private void ShotOne(EnemyControll character, float shift_direction, float direction)
        {
            GameObject blt_go = StageManager.Instance.InstantiateObject(Source);
            EnemyControll blt_ctrl = blt_go.GetComponent<EnemyControll>();
            if(blt_ctrl != null){
                blt_ctrl.Position = character.Position;

                InitialData initial = new InitialData();

                float angle = (AngleBase == AngleType.ABSOLUTE) ? 180f : character.Direction;

                initial.Direction = angle + direction;
                initial.ShiftAngle = angle + shift_direction;
                initial.ShiftSpeed = ShiftSpeed;
                initial.ShiftDamp = ShiftDamp;
                initial.WateTime = Delay;

                blt_ctrl.Initial = initial;
            }
            else
            {
                blt_go.transform.position = character.transform.position;
            }
            blt_go.SetActive(true);
        }

        /// *******************************************************
        /// <summary>進捗割合</summary>
        /// *******************************************************
        private float CurrentRate()
        {
            if (ShotTime <= 0) return 1;
            return (float)CurrentShotCounts / (float)ShotTime;
        }

        /// *******************************************************
        /// <summary>現発射数</summary>
        /// *******************************************************
        private int CurrentCount(float rate)
        {
            return Mathf.Max(Mathf.RoundToInt((float)StartCount + (float)(EndCount - StartCount) * rate), 1);
        }

        /// *******************************************************
        /// <summary>現弾角度</summary>
        /// *******************************************************
        private float CurrentWidth(float rate)
        {
            return StartWidth + (EndWidth - StartWidth) * rate;
        }
        /// *******************************************************
        /// <summary>現発射角度</summary>
        /// *******************************************************
        private float CurrentShiftAngle(float rate)
        {
            return StartShiftAngle + (EndShiftAngle - StartShiftAngle) * rate;
        }
    }

}
