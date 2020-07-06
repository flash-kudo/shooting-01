using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class MirrorControll : MonoBehaviour
    {
        public GameObject MirrorEdge1;
        public GameObject MirrorEdge2;

        // Start is called before the first frame update
        void Start()
        {
            if (PlayerControll.Instance != null)
            {
                PlayerControll.Instance.MirrirEdgeList.Add(MirrorEdge1);
                PlayerControll.Instance.MirrirEdgeList.Add(MirrorEdge2);
            }
        }

        private void OnDisable()
        {
            if (PlayerControll.Instance != null)
            {
                PlayerControll.Instance.MirrirEdgeList.Remove(MirrorEdge1);
                PlayerControll.Instance.MirrirEdgeList.Remove(MirrorEdge2);
            }
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        protected void Update()
        {
            if (StageManager.Instance.IsMovableMirror == true)
            {
                MOUSE_STATE state = GetMouseState();

                switch (state)
                {
                    case MOUSE_STATE.DOWN:
                        if (Draggable == null)
                        {
                            float angle = GetMouseAngle();
                            float dst = angle - SelfAngle;

                            if ((dst > -22.5f) && (dst < 22.5f))
                            {
                                Draggable = this;
                                SelfAngle = angle;
                            }
                        }
                        break;
                    case MOUSE_STATE.UP:
                        if (Draggable == this)
                        {
                            SelfAngle = GetMouseAngle();
                            Draggable = null;
                        }
                        break;
                    case MOUSE_STATE.DRAG:
                        if (Draggable == this)
                        {
                            SelfAngle = GetMouseAngle();
                        }
                        break;
                }
            }
            else
            {
                LastMouseDown = false;
                Draggable = null;
            }
        }

        private enum MOUSE_STATE
        {
            NONE,
            DOWN,
            DRAG,
            UP,
        }
        private bool LastMouseDown = false;

        private static MirrorControll Draggable = null;

        private MOUSE_STATE GetMouseState()
        {
            bool CurrentMouseDown = Input.GetMouseButton(0);
            MOUSE_STATE state = MOUSE_STATE.NONE;
            if (CurrentMouseDown == true)
            {
                if (LastMouseDown == true) state = MOUSE_STATE.DRAG;
                else state = MOUSE_STATE.DOWN;
            }
            else
            {
                if (LastMouseDown == true) state = MOUSE_STATE.UP;
                else state = MOUSE_STATE.NONE;
            }
            LastMouseDown = CurrentMouseDown;

            return state;
        }

        private float GetMouseAngle()
        {
            Vector3 campos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(transform.position.x - campos.x, campos.y - transform.position.y);

            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 180;
            if (angle < 0) angle -= 360;

            return angle;
        }
        private float SelfAngle
        {
            get
            {
                float angle = transform.rotation.eulerAngles.z;
                if (angle < 0) angle -= 360;
                return angle;
            }
            set
            {
                transform.rotation = Quaternion.Euler(0, 0, value);
            }
        }

    }
}
