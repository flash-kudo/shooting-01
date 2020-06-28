using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class WallControll : CharacterControll
    {
        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        protected override void OnUpdate()
        {
            base.OnUpdate();

            if(StageManager.Instance.IsMovableWall == true)
            {
                MOUSE_STATE state = GetMouseState();
                Vector2 position;

                switch (state)
                {
                    case MOUSE_STATE.DOWN:
                        if(Draggable == null)
                        {
                            position = GetMousePosition();
                            if((ColliderScale - Vector2.Distance(position, Position)) > 0f)
                            {
                                Draggable = this;
                                Position = position;
                            }
                        }
                        break;
                    case MOUSE_STATE.UP:
                        if(Draggable == this)
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

        private static WallControll Draggable = null;

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

    }
}