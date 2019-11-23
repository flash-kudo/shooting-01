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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MoveSpeed += Accelerate;
            Direction = ((Direction + RotateSpeed + 180f) % 360f) - 180f;

            Position.x = Position.x + Mathf.Cos(Direction * Mathf.Deg2Rad) * MoveSpeed;
            Position.y = Position.y + Mathf.Sin(Direction * Mathf.Deg2Rad) * MoveSpeed;

            transform.position = Position;
        }
    }
}
