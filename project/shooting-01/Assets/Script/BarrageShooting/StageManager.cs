using BarrageShooting.StageScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class StageManager : MonoBehaviour
    {

        private Dictionary<string, GameObject> ResourceList;

        [SerializeField]
        public TextAsset StageScript;


        public StageScriptMain StageProc;
        private List<EnemyControll> EnemyList;

        private static StageManager _Instance;
        /// *******************************************************
        /// <summary>Singleton参照</summary>
        /// *******************************************************
        public static StageManager Instance
        {
            get
            {
                if (_Instance == null) _Instance = (StageManager)FindObjectOfType(typeof(StageManager));
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
        void Start()
        {
            StageProc = new StageScriptMain();
            if(StageScript != null)
            {
                StageProc.ReadScriptText(StageScript.text);
                StageProc.StartStage();
            }
        }

        private void Update()
        {
            if(StageProc != null) StageProc.OnUpdate();
        }

        // ########################################################

        /// *******************************************************
        /// <summary>リソース取得</summary>
        /// *******************************************************
        public GameObject InstantiateObject(string path)
        {
            if (ResourceList == null) ResourceList = new Dictionary<string, GameObject>();

            GameObject source = null;
            if (ResourceList.TryGetValue(path, out source) == false)
            {
                source = (GameObject)Resources.Load(path);
                source.SetActive(false);
                ResourceList.Add(path, source);
            }
            if (source == null) return null;
            return Object.Instantiate(source);
        }

        /// *******************************************************
        /// <summary>敵出現リスト追加</summary>
        /// *******************************************************
        public void AddEnemyList(EnemyControll ctrl)
        {
            if (EnemyList == null) EnemyList = new List<EnemyControll>();
            EnemyList.Add(ctrl);
        }

        /// *******************************************************
        /// <summary>敵出現リスト削除</summary>
        /// *******************************************************
        public void RemoveEnemyList(EnemyControll ctrl)
        {
            if (EnemyList == null) return;
            EnemyList.Remove(ctrl);
        }

        /// *******************************************************
        /// <summary>敵出現数</summary>
        /// *******************************************************
        public int EnemyCount
        {
            get
            {
                if (EnemyList == null) return 0;
                return EnemyList.Count;
            }
        }
    }
}
