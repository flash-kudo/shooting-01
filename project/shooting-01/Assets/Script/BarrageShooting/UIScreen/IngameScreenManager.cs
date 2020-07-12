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


        // Start is called before the first frame update
        void Start()
        {
            PlayerCtrl = PlayerControll.Instance;

            UpdateShotButton();
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

    }
}
