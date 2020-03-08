using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class PlayerControll : MonoBehaviour
    {

        public GameObject ShotPrefab;
        [Range(0.017f, 1f)]
        public float ShotInterval = 0.1f;
        [Range(1, 10)]
        public int ShotCount = 1;
        [Range(0f, 180f)]
        public float ShotWidth = 0f;

        private float ShotPast = 0;

        public GameObject GranadePrefab;
        [Range(0.017f, 2f)]
        public float GranadeInterval = 0.5f;

        private float GranadePast = 0;


        public bool UseShot;
        public bool UseGranade;


        private Vector3 Target = new Vector3();
        private float Distance = 0;
        private float Direction = 0;

        private Vector3 Position { get { return transform.position; } }

        // Update is called once per frame
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
        }

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

        private void CreateShot(float direction)
        {
            GameObject self_go = Instantiate(ShotPrefab);
            CharacterControll self_ctrl = self_go.GetComponent<CharacterControll>();
            self_ctrl.Position = Position;
            self_ctrl.Direction = direction;
            self_go.SetActive(true);
        }

        private void ProcGranade()
        {
            GranadePast += CharacterControll.FRAME_TIME;
            if (GranadePast > GranadeInterval)
            {
                CreateGranade();
                GranadePast -= GranadeInterval;
            }
        }

        private void CreateGranade()
        {
            GameObject self_go = Instantiate(GranadePrefab);
            GranadeControll self_ctrl = self_go.GetComponent<GranadeControll>();
            self_ctrl.Position = Position;
            self_ctrl.Direction = Direction;
            self_ctrl.TargetDistance = Distance;
            self_go.SetActive(true);
        }
    }
}
