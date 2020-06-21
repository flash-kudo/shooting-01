using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class BuildScreenManager : MonoBehaviour
    {
        public Canvas MainScreen;

        // Start is called before the first frame update
        void Start()
        {
            CloseScreen();
        }

        public void OpenScreen()
        {
            StageManager.Instance.IsBuildScreen = true;
            MainScreen.enabled = true;
        }

        public void CloseScreen()
        {
            MainScreen.enabled = false;
            StageManager.Instance.IsBuildScreen = false;
        }
    }
}
