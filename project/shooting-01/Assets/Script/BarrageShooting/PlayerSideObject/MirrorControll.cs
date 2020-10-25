using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class MirrorControll : MonoBehaviour
    {
        public GameObject MirrorEdge1;
        public GameObject MirrorEdge2;

        public float ColliderScale = 0.5f;

        protected Vector2 Position
        {
            get
            {
                return transform.localPosition;
            }
            set
            {
                transform.localPosition = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (PlayerControll.Instance != null)
            {
                PlayerControll.Instance.MirrirList.Add(this);
                PlayerControll.Instance.MirrirEdgeList.Add(MirrorEdge1);
                PlayerControll.Instance.MirrirEdgeList.Add(MirrorEdge2);
            }
        }

        private void OnDisable()
        {
            if (PlayerControll.Instance != null)
            {
                PlayerControll.Instance.MirrirList.Remove(this);
                PlayerControll.Instance.MirrirEdgeList.Remove(MirrorEdge1);
                PlayerControll.Instance.MirrirEdgeList.Remove(MirrorEdge2);
            }
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        protected void Update()
        {
            //if (StageManager.Instance.IsMovableMirror == true)
            //{
                MOUSE_STATE state = GetMouseState();
                Vector2 position;

                switch (state)
                {
                    case MOUSE_STATE.DOWN:
                        if (Draggable == null)
                        {
                            position = GetMousePosition();
                            if ((ColliderScale - Vector2.Distance(position, Position)) > 0f)
                            {
                                Draggable = this;
                                Position = position;
                            }
                        }
                        break;
                    case MOUSE_STATE.UP:
                        if (Draggable == this)
                        {
                            Position = GetMousePosition();
                            Draggable = null;
                        }
                        break;
                    case MOUSE_STATE.DRAG:
                        if (Draggable == this)
                        {
                            Position = GetMousePosition();
                        }
                        break;
                }
            //}
            //else
            //{
            //    LastMouseDown = false;
            //    Draggable = null;
            //}

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

        private Vector2 GetMousePosition()
        {
            Vector3 campos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(campos.x, campos.y);
        }

        public void MirrorShot()
        {
            GameObject reflecteff = StageManager.Instance.InstantiateObject("Reflect".ToLower());
            ShotControll self_ctrl = reflecteff.GetComponent<ShotControll>();
            self_ctrl.Position = this.transform.position;
            self_ctrl.Direction = -this.transform.eulerAngles.z;
            self_ctrl.Player = PlayerControll.Instance;
            self_ctrl.ShotSpeed = PlayerControll.Instance.ShotSpeed;
            reflecteff.SetActive(true);

        }

    }
}
