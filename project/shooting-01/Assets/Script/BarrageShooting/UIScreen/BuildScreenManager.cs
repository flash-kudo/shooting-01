using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class BuildScreenManager : MonoBehaviour
    {
        private const bool IS_DEBUG = false;

        public GameObject MainScreen1;
        public GameObject MainScreen2;
        public GameObject MainScreen3;
        public GameObject MainScreenDebug;
        public GameObject WallScreen;
        public GameObject MirrorScreen;

        public GameObject WallSource;
        public GameObject MirrorSource;

        // Start is called before the first frame update
        void Start()
        {
            CloseScreen();
        }

        public void OnBuildHissatsu()
        {
            StageManager.Instance.BombCount++;

            CloseScreen();
        }
        public void OnBuildHansha()
        {
            CloseScreen(true);

            Object.Instantiate(MirrorSource);

            StageManager.Instance.IsMovableMirror = true;
            MirrorScreen.SetActive(true);
        }
        public void OnBuildKabe()
        {
            CloseScreen(true);

            Object.Instantiate(WallSource);

            StageManager.Instance.IsMovableWall = true;
            WallScreen.SetActive(true);
        }
        public void OnTimeup()
        {
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

        public void CloseScreen(bool stay_build = false)
        {
            MainScreen1.SetActive(false);
            MainScreen2.SetActive(false);
            MainScreen3.SetActive(false);
            MainScreenDebug.SetActive(false);
            WallScreen.SetActive(false);
            MirrorScreen.SetActive(false);

            StageManager.Instance.IsBuildScreen = stay_build;
            StageManager.Instance.IsMovableWall = false;
            StageManager.Instance.IsMovableMirror = false;
        }
    }
}
