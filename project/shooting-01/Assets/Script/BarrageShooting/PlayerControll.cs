using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace BarrageShooting
{
    public class PlayerControll : MonoBehaviour
    {
        public GameObject PlayerImage;
        public GameObject ShotTarget;

        public GameObject ShotPrefab;
        public GameObject ShotMuzzle;
        [Range(0.017f, 1f)]
        public float ShotInterval = 0.1f;
        [Range(1, 10)]
        public int ShotCount = 1;
        [Range(0f, 180f)]
        public float ShotWidth = 0f;
        [Range(1f, 100f)]
        public float ShotSpeed = 15f;

        private float ShotPast = 0;

        public GameObject GranadePrefab;
        public GameObject GranadeMuzzle;
        public GameObject GranadeSight;
        [Range(0.017f, 2f)]
        public float GranadeInterval = 0.5f;
        [Range(10f, 100f)]
        public float GranadeDuration = 50f;
        [Range(0.01f, 2)]
        public float GranadeBlast = 1f;

        private float GranadePast = 0;

        public GameObject MirrorEdge;

        public bool UseShot;

        [Range(0,8)]
        public int MirrorCount = 0;

        [HideInInspector]
        public Vector3 Target = new Vector3();
        private float Distance = 0;
        private float Direction = 0;
        public List<GameObject> MirrirEdgeList;
        public List<MirrorControll> MirrirList;

        public Transform ArmLeftPos;
        public Transform ArmRightPos;
        public Transform CannonPos;

        public PlayableDirector AnimationIdle;
        public PlayableDirector ChargeArm;
        public PlayableDirector ChargeCannon;
        public PlayableDirector ShotArm;
        public PlayableDirector ShotCannon;
        public PlayableDirector WaitArm;
        public PlayableDirector WaitCannon;

        private PlayerTimelineManage PlayerTimeline;

        private Vector3 Position { get { return transform.position; } }

        private static PlayerControll _Instance;
        /// *******************************************************
        /// <summary>Singleton参照</summary>
        /// *******************************************************
        public static PlayerControll Instance
        {
            get
            {
                if (_Instance == null) _Instance = (PlayerControll)FindObjectOfType(typeof(PlayerControll));
                return _Instance;
            }
        }

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        void Awake()
        {
            _Instance = this;
        }

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        private void Start()
        {
            UseShot = true;
            GranadeSight.SetActive(!UseShot);

            if (PlayerTimeline == null)
            {
                PlayerTimeline = new PlayerTimelineManage();
                PlayerTimeline.SetIdle(AnimationIdle);
                PlayerTimeline.SetArm(ChargeArm, ShotArm, WaitArm);
                PlayerTimeline.SetCannon(ChargeCannon, ShotCannon, WaitCannon);
            }
        }

        /// *******************************************************
        /// <summary>更新処理</summary>
        /// *******************************************************
        void Update()
        {
            PlayerTimeline.OnUpdate();
        }

        public void OnShot(float direction_rad, float distance)
        {
            if (StageManager.Instance.IsShootable == false) return;

            if (UseShot)
            {
                Direction = direction_rad * Mathf.Rad2Deg;
                Distance = 2f;

                Vector3 add = new Vector3(Mathf.Sin(direction_rad) * Distance, Mathf.Cos(direction_rad) * Distance, 0);
                Target = Position + add;

                ProcShot();
            }
            else
            {
                // 
                //float dist = distance * 0.2f;
                float dist = 0.07f;
                Vector3 add = new Vector3(Mathf.Sin(direction_rad) * dist, Mathf.Cos(direction_rad) * dist, 0);
                if (distance < 0.45f) add = new Vector3();
                 Target = Target + add;
                if (Target.x < -3.8f) Target.x = -3.8f;
                if (Target.x > 3.8f) Target.x = 3.8f;
                if (Target.y < -5f) Target.y = -5f;
                if (Target.y > 5f) Target.y = 5f;

                Vector3 trg = Target - Position;
                Distance = Vector3.Distance(new Vector3(0, 0, trg.z), trg);
                Direction = Mathf.Atan2(trg.x, trg.y) * Mathf.Rad2Deg;

                ProcGranade();
            }

            ShotTarget.transform.position = Target;
            PlayerImage.transform.rotation = Quaternion.Euler(0, 0, -Direction);
        }

        /// *******************************************************
        /// <summary>射撃切り替え</summary>
        /// *******************************************************
        public void SwitchShot()
        {
            UseShot = !UseShot;
            GranadeSight.SetActive(!UseShot);
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
                    CreateShot(dir, i < (ShotCount / 2) ? ArmLeftPos : ArmRightPos);
                    dir += dist;
                }
                Instantiate(ShotMuzzle, ArmLeftPos.position, Quaternion.Euler(0,0,-Direction));
                Instantiate(ShotMuzzle, ArmRightPos.position, Quaternion.Euler(0,0,-Direction));

                if(UseShot) PlayerTimeline.PlayTimeline(PlayerTimelineManage.TimelineType.ShotArm);

                ShotPast -= ShotInterval;
            }
        }

        /// *******************************************************
        /// <summary>主砲発射処理</summary>
        /// *******************************************************
        private void CreateShot(float direction, Transform pos)
        {
            GameObject self_go = Instantiate(ShotPrefab);
            ShotControll self_ctrl = self_go.GetComponent<ShotControll>();
            self_ctrl.Position = pos.position;
            self_ctrl.Direction = direction;
            self_ctrl.Player = this;
            self_ctrl.ShotSpeed = ShotSpeed;
            self_go.SetActive(true);

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
                if (!UseShot) PlayerTimeline.PlayTimeline(PlayerTimelineManage.TimelineType.ShotCannon);
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
            self_ctrl.Position = CannonPos.position;
            self_ctrl.Direction = Direction;
            self_ctrl.TargetDistance = Distance;
            self_ctrl.MoveDulation = GranadeDuration;
            self_ctrl.BlastDuration = GranadeBlast;
            self_go.SetActive(true);

            Instantiate(GranadeMuzzle, CannonPos.position, Quaternion.Euler(0, 0, -Direction));

        }


    }
}
