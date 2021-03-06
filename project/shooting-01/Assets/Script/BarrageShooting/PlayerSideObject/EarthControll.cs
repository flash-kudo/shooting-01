﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class EarthControll : CharacterControll
    {
        // [SerializeField]
        // public Vector2 Position;

        // [SerializeField]
        // public float Direction = 0;
        // [SerializeField]
        // public float MoveSpeed = 0;
        // [SerializeField]
        // public float MaxSpeed = 1;

        // [SerializeField]
        // public float RotateSpeed = 0;
        // [SerializeField]
        // public float Accelerate = 0;

        // [SerializeField]
        // public float ShiftSpeed = 0;
        // [SerializeField]
        // public float ShiftAngle = 0;
        // [SerializeField]
        // public float ShiftDamp = 0;

        // [SerializeField]
        // public float ColliderScale = 0.1f;
        // [SerializeField]
        // public Vector2 ColliderPosition;

        // [SerializeField]
        // public float ToughPoint = 1;
        // [SerializeField]
        // public float AttackPoint = 10;

        private int HIT_SETTIME = 3;
        public int HitmarkTime = 0;
        public SpriteRenderer EarthRenderer;

        public GameObject HitEffect;
        public Renderer HitRenderer;
        public Material HitMaterial;

        private int _BaseColor;

        public AudioSource snd_Damage1;
        public AudioSource snd_Damage2;

        private const float DESTROYANIM_WAIT = 2.0f;
        private bool IsDestroyed = false;
        private float DestroyDuration = 0;

        private static EarthControll _Instance;
        /// *******************************************************
        /// <summary>Singleton参照</summary>
        /// *******************************************************
        public static EarthControll Instance
        {
            get
            {
                if (_Instance == null) _Instance = (EarthControll)FindObjectOfType(typeof(EarthControll));
                return _Instance;
            }
        }

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        void Awake()
        {
            _Instance = this;
            IsDestroyed = false;

            HitMaterial = HitRenderer.sharedMaterial;
            _BaseColor = Shader.PropertyToID("_BaseColor");
        }

        /// *******************************************************
        /// <summary>破棄処理</summary>
        /// *******************************************************
        private void OnDestroy()
        {
            _Instance = null;
        }

        // Start is called before the first frame update
        void Start()
        {
            AttackPoint = float.PositiveInfinity;
            base.OnStart();
        }

        // Update is called once per frame
        protected override void OnUpdate()
        {
            if(HitmarkTime > 0)
            {
                EarthRenderer.color = Color.red;
                HitmarkTime--;
            }
            else
            {
                EarthRenderer.color = Color.white;
            }
            base.OnUpdate();

            if(IsDestroyed == true)
            {
                DestroyDuration += Time.deltaTime;
                if(DestroyDuration > DESTROYANIM_WAIT)
                {
                    DestroyDuration = DESTROYANIM_WAIT;
                    Destroy(gameObject);
                }
                HitMaterial.SetColor(_BaseColor, new Color(1, 1, 1, DestroyDuration / DESTROYANIM_WAIT));
            }
        }

        /// *******************************************************
        /// <summary>当たり対象があるか</summary>
        /// <returns>true:ある/false:無い</returns>
        /// *******************************************************
        protected override bool HitCheck()
        {
            if (IsDestroyed == true) return false;
            HitEffect.SetActive(false);

            if (HitTarget == null) return false;
            if (HitTarget.Count == 0) return false;

            HitmarkTime = HIT_SETTIME;

            HitEffect.SetActive(true);
            HitMaterial.SetColor(_BaseColor, Color.white);
            snd_Damage1.time = 0;
            snd_Damage1.Play();
            snd_Damage2.time = 0;
            snd_Damage2.Play();

            HitTarget.ForEach(trg => {
                GameManager.Instance.AddExp(ExperienceData.ExpType.EARTH_DMG);

                ToughPoint -= trg.AttackPoint;
            });
            if (ToughPoint < 0)
            {
                RemoveField();
                return true;
            }
            HitTarget.Clear();
            return false;
        }

        public void AddDamage(float damage)
        {
            ToughPoint -= damage;
            if (ToughPoint < 0)
            {
                RemoveField();
            }
        }

        /// *******************************************************
        /// <summary>破棄処理</summary>
        /// *******************************************************
        protected override void RemoveField()
        {
            // gameover
            StageManager.Instance.OnEndGame();
            //Destroy(gameObject);
            IsDestroyed = true;
            DestroyDuration = 0;
        }


    }
}
