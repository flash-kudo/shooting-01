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
    }
}
