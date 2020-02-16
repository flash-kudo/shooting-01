using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting.EnemyScript
{
    public class InitialData
    {
        private float _Direction;
        private float _ShiftSpeed;
        private float _ShiftAngle;
        private float _ShiftDamp;
        private float _WateTime;

        public bool Override_Direction = false;
        public bool Override_ShiftSpeed = false;
        public bool Override_ShiftAngle = false;
        public bool Override_ShiftDamp = false;
        public bool Override_WateTime = false;

        public float Direction { get { return _Direction; } set { _Direction = value; Override_Direction = true; } }
        public float ShiftSpeed { get { return _ShiftSpeed; } set { _ShiftSpeed = value; Override_ShiftSpeed = true; } }
        public float ShiftAngle { get { return _ShiftAngle; } set { _ShiftAngle = value; Override_ShiftAngle = true; } }
        public float ShiftDamp { get { return _ShiftDamp; } set { _ShiftDamp = value; Override_ShiftDamp = true; } }
        public float WateTime { get { return _WateTime; } set { _WateTime = value; Override_WateTime = true; } }
    }
}