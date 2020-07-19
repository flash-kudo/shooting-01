using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BarrageShooting
{
    public class IngameScreenManager : MonoBehaviour
    {
        private PlayerControll PlayerCtrl;

        public Button ShotButton;
        public Button GranadeButton;

        public Button BombButton5;
        public Button BombButton4;
        public Button BombButton3;
        public Button BombButton2;
        public Button BombButton1;


        // Start is called before the first frame update
        void Start()
        {
            PlayerCtrl = PlayerControll.Instance;

            UpdateShotButton();
            HideBombButton();
            UpdateBombButton();
        }

        public void UpdateIngameScreen()
        {
            UpdateBombButton();
        }

        public void SwitchShot()
        {
            PlayerCtrl.UseShot = !PlayerCtrl.UseShot;
            UpdateShotButton();
        }


        private void UpdateShotButton()
        {
            ShotButton.gameObject.SetActive(PlayerCtrl.UseShot == true);
            GranadeButton.gameObject.SetActive(PlayerCtrl.UseShot == false);
        }

        private void UpdateBombButton()
        {
            if (StageManager.Instance.BombCount > 0)
            {
                if (IsActiveBombButton() == false)
                {
                    BombButton5.gameObject.SetActive(true);
                }
            }
            else
            {
                HideBombButton();
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
            HideBombButton();
            BombButton4.gameObject.SetActive(true);
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
            StageManager.Instance.UseBomb();
        }
    }
}
