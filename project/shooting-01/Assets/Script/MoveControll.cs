using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class MoveControll : MonoBehaviour
    {
        public Vector2 Position;

        public float Direction = 0;
        public float MoveSpeed = 0;

        public float RotateSpeed = 0;
        public float Accelerate = 0;

        private Vector3 BaseAngle;

        // Start is called before the first frame update
        void Start()
        {
            BaseAngle = transform.eulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
            MoveSpeed += Accelerate;
            Direction = ((Direction + RotateSpeed + 180f) % 360f) - 180f;

            Position.x = Position.x + Mathf.Sin(Direction * Mathf.Deg2Rad) * MoveSpeed;
            Position.y = Position.y + Mathf.Cos(Direction * Mathf.Deg2Rad) * MoveSpeed;

            transform.localPosition = Position;
            transform.localEulerAngles = BaseAngle + new Vector3(0, 0, -Direction);
        }
    }
}
