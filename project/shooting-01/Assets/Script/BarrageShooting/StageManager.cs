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

        public int WaveNumber;
        public int WavePlayerLevel;

        public StageScriptMain StageProc;
        private List<EnemyControll> EnemyList;

        public BuildScreenManager BuildScreen;
        public bool IsBuildScreen = false;
        public bool IsMovableWall = false;
        public bool IsMovableMirror = false;


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
            StageProc = new StageScriptMain(this);
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
        
        public void SetWaveNumber(int num)
        {
            WaveNumber = num;
            if (GameManager.Instance != null)
                WavePlayerLevel = Mathf.FloorToInt(GameManager.Instance.PlayerCalcLevel());
        }
        public int GetWaveNumber()
        {
            return WaveNumber;
        }

        // ########################################################

        /// *******************************************************
        /// <summary>射撃可能な状態か</summary>
        /// *******************************************************
        public bool IsShootable { get
        {
            if (IsBuildScreen == true) return false;

            return true;
        } }

        /// *******************************************************
        /// <summary>リソース取得</summary>
        /// *******************************************************
        public GameObject InstantiateObject(string source_id)
        {
            if (ResourceList == null) ResourceList = new Dictionary<string, GameObject>();

            string path = GameResourceList.PathList[source_id];

            GameObject gameobj_source = null;
            if (ResourceList.TryGetValue(path, out gameobj_source) == false)
            {
                gameobj_source = (GameObject)Resources.Load(path);
                gameobj_source.SetActive(false);
                ResourceList.Add(path, gameobj_source);
            }
            if (gameobj_source == null) return null;
            return Object.Instantiate(gameobj_source);
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
                if (EnemyList.Count > 0) return EnemyList.Count;
                return EnemyList.Count;
            }
        }

        public void OpenBuildScreen()
        {
            BuildScreen.OpenScreen();
        }
    }
}
