using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class MirrorDirectionImage : MonoBehaviour
    {
        public GameObject Arrow0;
        public GameObject Arrow1;
        public GameObject Arrow2;
        public GameObject Arrow3;
        public GameObject Arrow4;
        public GameObject Arrow5;
        public GameObject Arrow6;
        public GameObject Arrow7;

        public void SetRotation(int rot)
        {
            Arrow0.SetActive(rot == 0);
            Arrow1.SetActive(rot == 1);
            Arrow2.SetActive(rot == 2);
            Arrow3.SetActive(rot == 3);
            Arrow4.SetActive(rot == 4);
            Arrow5.SetActive(rot == 5);
            Arrow6.SetActive(rot == 6);
            Arrow7.SetActive(rot == 7);
        }
    }
}
