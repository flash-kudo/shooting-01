using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class BuildScreenManager : MonoBehaviour
    {
        private const bool IS_DEBUG = true;

        public GameObject MainScreen1;
        public GameObject MainScreen2;
        public GameObject MainScreen3;
        public GameObject MainScreenDebug;

        // Start is called before the first frame update
        void Start()
        {
            CloseScreen();
        }

        public void OnBuildHissatsu()
        {
            Debug.Log("必殺");
            CloseScreen();
        }
        public void OnBuildHansha()
        {
            Debug.Log("反射");
            CloseScreen();
        }
        public void OnBuildKabe()
        {
            Debug.Log("壁");
            CloseScreen();
        }
        public void OnTimeup()
        {
            Debug.Log("Timeup");
            CloseScreen();
        }


        public void OpenScreen()
        {
            StageManager.Instance.IsBuildScreen = true;

            if (IS_DEBUG)
            {
                MainScreenDebug.SetActive(true);
            }
            else
            {
                float rnd = Random.value;
                if (rnd < 1f / 3f) MainScreen1.SetActive(true);
                else if (rnd < 2f / 3f) MainScreen2.SetActive(true);
                else MainScreen3.SetActive(true);
            }

        }

        public void CloseScreen()
        {
            MainScreen1.SetActive(false);
            MainScreen2.SetActive(false);
            MainScreen3.SetActive(false);
            MainScreenDebug.SetActive(false);

            StageManager.Instance.IsBuildScreen = false;
        }
    }
}
