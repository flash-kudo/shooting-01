using BarrageShooting.StageScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public class StageManager : MonoBehaviour
    {
        public const int BOMB_USE_COUNT = 3;

        private Dictionary<string, GameObject> ResourceList;

        [SerializeField]
        public TextAsset StageScript;

        public int WaveNumber;
        public int InitBombCount;
        public int BombCount;
        public bool OnBombRunning { get; private set; }

        public StageScriptMain StageProc;
        private List<EnemyControll> EnemyList;

        public BuildScreenManager BuildScreen;
        public bool IsBuildScreen = false;
        public bool IsMovableWall = false;
        public bool IsMovableMirror = false;

        public IngameScreenManager IngameScreen;
        public StaticScreenManager StaticScreen;

        private const float ENDGAME_WAIT = 5.0f;
        private bool IsEndGame;
        private float EndGamePast;

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
            IsEndGame = false;
            StageProc = new StageScriptMain(this);
            OnBombRunning = false;
            if (StageScript != null)
            {
                StageProc.ReadScriptText(StageScript.text);
                BombCount = InitBombCount;
                StageProc.StartStage();
            }
        }

        private void Update()
        {
            if(StageProc != null) StageProc.OnUpdate();

            if(IsEndGame == true)
            {
                EndGamePast += Time.deltaTime;
                if(EndGamePast > ENDGAME_WAIT)
                {
                    GameManager.Instance.OnEndGame();
                }
            }
        }
        
        public void SetWaveNumber(int num)
        {
            WaveNumber = num;
        }
        public int GetWaveNumber()
        {
            return WaveNumber;
        }

        // ########################################################

        /// *******************************************************
        /// <summary>ゲーム終了</summary>
        /// *******************************************************
        public void OnEndGame()
        {
            StageProc.IsProcStage = false;
            IsEndGame = true;
            EndGamePast = 0;
        }

        // ########################################################

        /// *******************************************************
        /// <summary>Bombの更新</summary>
        /// *******************************************************
        public void SetBombCount(int num)
        {
            BombCount = num;
            IngameScreen.UpdateIngameScreen();
        }

        /// *******************************************************
        /// <summary>Waveメッセージの表示</summary>
        /// *******************************************************
        public void ShowMessage(string msg)
        {
            StaticScreen.ShowMessage(msg);
        }

        /// *******************************************************
        /// <summary>Waveメッセージの消去</summary>
        /// *******************************************************
        public void HideMessage()
        {
            StaticScreen.HideMessage();
        }

        // ########################################################

        /// *******************************************************
        /// <summary>射撃可能な状態か</summary>
        /// *******************************************************
        public bool IsShootable { get
        {
            if (IsBuildScreen == true) return false;
                if (IsEndGame == true) return false;

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
        /// *******************************************************
        /// <summary>Bomb使用アニメーション中</summary>
        /// *******************************************************
        public void StartBomb()
        {
            OnBombRunning = true;
        }

        /// *******************************************************
        /// <summary>Bomb使う</summary>
        /// *******************************************************
        public void UseBomb()
        {
            if (BombCount < BOMB_USE_COUNT) return;
            if (EnemyList == null) return;

            EnemyList.ForEach(enemy => { enemy.Bomb(); });
            EnemyList.Clear();

            if (FortressControll.Instance != null) FortressControll.Instance.Bomb();

            BombCount -= BOMB_USE_COUNT;
            IngameScreen.UpdateIngameScreen();
            OnBombRunning = false; ;
        }

        /// *******************************************************
        /// <summary>ビルドスクリーンを開く</summary>
        /// *******************************************************
        public void OpenBuildScreen()
        {
            BuildScreen.OpenScreen();
        }
    }
}
