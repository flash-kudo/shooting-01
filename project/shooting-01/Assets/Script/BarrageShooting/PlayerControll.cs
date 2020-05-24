using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class PlayerControll : MonoBehaviour
    {

        public GameObject ShotPrefab;
        public GameObject ShotMuzzle;
        [Range(0.017f, 1f)]
        public float ShotInterval = 0.1f;
        [Range(1, 10)]
        public int ShotCount = 1;
        [Range(0f, 180f)]
        public float ShotWidth = 0f;

        private float ShotPast = 0;

        public GameObject GranadePrefab;
        public GameObject GranadeMuzzle;
        [Range(0.017f, 2f)]
        public float GranadeInterval = 0.5f;

        private float GranadePast = 0;

        public GameObject MirrorEdge;


        public bool UseShot;
        public bool UseGranade;
        [Range(0,8)]
        public int MirrorCount = 0;

        private Vector3 Target = new Vector3();
        private float Distance = 0;
        private float Direction = 0;
        public List<GameObject> MirrirEdgeList;

        private Vector3 Position { get { return transform.position; } }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Target = Position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Distance = Vector3.Distance(new Vector3(0, 0, Target.z), Target);
                Direction = Mathf.Atan2(Target.x, Target.y) * Mathf.Rad2Deg + 180;
            }
            if(UseShot) ProcShot();
            if(UseGranade) ProcGranade();
            UpdateMirrorEdges();
        }

        /// *******************************************************
        /// <summary>主砲間隔処理</summary>
        /// *******************************************************
        private void ProcShot()
        {
            ShotPast += CharacterControll.FRAME_TIME;
            if (ShotPast > ShotInterval)
            {

                float dir = (ShotCount == 1) ? Direction : Direction - (ShotWidth / 2f);
                float dist = (ShotCount == 1) ? 0 : ShotWidth / (float)(ShotCount - 1);
                for (int i = 0; i < ShotCount; i++)
                {
                    CreateShot(dir);
                    dir += dist;
                }
                ShotPast -= ShotInterval;
            }
        }

        /// *******************************************************
        /// <summary>主砲発射処理</summary>
        /// *******************************************************
        private void CreateShot(float direction)
        {
            GameObject self_go = Instantiate(ShotPrefab);
            ShotControll self_ctrl = self_go.GetComponent<ShotControll>();
            self_ctrl.Position = Position;
            self_ctrl.Direction = direction;
            self_ctrl.Player = this;
            self_go.SetActive(true);

            Instantiate(ShotMuzzle, transform.position, Quaternion.Euler(0,0,-direction));
        }

        /// *******************************************************
        /// <summary>榴弾間隔処理</summary>
        /// *******************************************************
        private void ProcGranade()
        {
            GranadePast += CharacterControll.FRAME_TIME;
            if (GranadePast > GranadeInterval)
            {
                CreateGranade();
                GranadePast -= GranadeInterval;
            }
        }

        /// *******************************************************
        /// <summary>榴弾発射処理</summary>
        /// *******************************************************
        private void CreateGranade()
        {
            GameObject self_go = Instantiate(GranadePrefab);
            GranadeControll self_ctrl = self_go.GetComponent<GranadeControll>();
            self_ctrl.Position = Position;
            self_ctrl.Direction = Direction;
            self_ctrl.TargetDistance = Distance;
            self_go.SetActive(true);

            Instantiate(GranadeMuzzle, transform.position, Quaternion.Euler(0, 0, -Direction));

        }

        /// *******************************************************
        /// <summary>ミラー増減処理</summary>
        /// *******************************************************
        private void UpdateMirrorEdges()
        {
            if (MirrirEdgeList == null) MirrirEdgeList = new List<GameObject>();

            int distance = (MirrorCount * 2) - MirrirEdgeList.Count;
            if (distance > 0)
            {
                GameObject last_edge = null;
                for(int i = 0; i < distance; i++)
                {
                    GameObject mirror_go = Instantiate(MirrorEdge);
                    mirror_go.transform.parent = this.transform;
                    mirror_go.SetActive(true);
                    MirrirEdgeList.Add(mirror_go);

                    if((i % 2) != 0)
                    {
                        MirrorGraphics graph = mirror_go.GetComponent<MirrorGraphics>();
                        graph.AnotherEdge = last_edge;
                    }

                    last_edge = mirror_go;

                }
            }
            if (distance < 0)
            {
                for (int i = 0; i > distance; i--)
                {
                    int index = MirrirEdgeList.Count - 1;
                    GameObject trsh = MirrirEdgeList[index];
                    MirrirEdgeList.RemoveAt(index);
                    Destroy(trsh);
                }
            }
        }

    }
}
