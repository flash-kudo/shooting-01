using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GodTouches;

namespace BarrageShooting
{
    public class IngameScreenManager : MonoBehaviour
    {
        private PlayerControll PlayerCtrl;

        public GameObject ShotCenter;
        public GameObject ShotDirection;
        public float ButtonWidth = 0.5f;
        public float MoveWidth = 0.5f;

        public Button ShotButton;
        public Button GranadeButton;

        public Button BombButton5;
        public Button BombButton4;
        public Button BombButton3;
        public Button BombButton2;
        public Button BombButton1;
        public GameObject BombControll;

        private Vector3 StartPos;
        private bool EnableButton;

        private Vector3 Center { get { return ShotCenter.transform.position; } }
        private Vector3 Position 
        {
            get { return ShotDirection.transform.position; }
            set { ShotDirection.transform.position = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            PlayerCtrl = PlayerControll.Instance;

            UpdateShotButton();
            HideBombButton();
            UpdateBombButton();
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        void Update()
        {
            
            GodPhase phase = GodTouch.GetPhase();
            Vector3 pos = Camera.main.ScreenToWorldPoint(GodTouch.GetPosition());
            pos.z = 0;

            switch (phase)
            {
                case GodPhase.Began:
                    EnableButton = false;
                    Vector3 press = pos - Center;
                    float distance = (press.x * press.x) + (press.y * press.y);
                    if (distance < (ButtonWidth * ButtonWidth))
                    {
                        StartPos = pos;
                        Position = Center;
                        EnableButton = true;
                    }
                    break;
                case GodPhase.Moved:
                    if (EnableButton)
                    {
                        Vector3 move = pos - StartPos;
                        float movepos = (move.x * move.x) + (move.y * move.y);
                        if (movepos > (MoveWidth * MoveWidth))
                        {
                            move = Vector3.Normalize(move) * MoveWidth;
                        }
                        Position = Center + move;

                        float dst = move.magnitude / MoveWidth;
                        float rad = Mathf.Atan2(move.x, move.y);

                        PlayerControll.Instance.OnShot(rad, dst);
                    }
                    break;
                case GodPhase.Ended:
                    EnableButton = false;
                    Position = Center;
                    break;
            }
        }

        public void UpdateIngameScreen()
        {
            UpdateBombButton();
        }

        public void SwitchShot()
        {
            PlayerCtrl.SwitchShot();
            UpdateShotButton();
        }


        private void UpdateShotButton()
        {
            ShotButton.gameObject.SetActive(PlayerCtrl.UseShot == true);
            GranadeButton.gameObject.SetActive(PlayerCtrl.UseShot == false);
        }

        private void UpdateBombButton()
        {
            if (IsActiveBombButton() == false)
            {
                BombButton5.gameObject.SetActive(true);
            }
        }

        private void HideBombButton()
        {
            BombButton5.gameObject.SetActive(false);
            BombButton4.gameObject.SetActive(false);
            BombButton3.gameObject.SetActive(false);
            BombButton2.gameObject.SetActive(false);
            BombButton1.gameObject.SetActive(false);
        }
        private bool IsActiveBombButton()
        {
            if (BombButton5.gameObject.activeSelf == true) return true;
            if (BombButton4.gameObject.activeSelf == true) return true;
            if (BombButton3.gameObject.activeSelf == true) return true;
            if (BombButton2.gameObject.activeSelf == true) return true;
            if (BombButton1.gameObject.activeSelf == true) return true;
            return false;
        }

        public void OnBombButton5()
        {
            if (StageManager.Instance.BombCount > 0)
            {
                HideBombButton();
                BombButton4.gameObject.SetActive(true);
            }
        }
        public void OnBombButton4()
        {
            HideBombButton();
            BombButton3.gameObject.SetActive(true);
        }
        public void OnBombButton3()
        {
            HideBombButton();
            BombButton2.gameObject.SetActive(true);
        }
        public void OnBombButton2()
        {
            HideBombButton();
            BombButton1.gameObject.SetActive(true);
        }
        public void OnBombButton1()
        {
            HideBombButton();
            BombControll.SetActive(true);
        }

        public void OnBombButton0()
        {
            StageManager.Instance.UseBomb();
        }
    }
}
