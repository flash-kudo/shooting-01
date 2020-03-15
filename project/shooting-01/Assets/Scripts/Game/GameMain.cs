using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

    // TODO とりあえずのカウント あとで敵残数などの管理方法は変える必要がある
    int enemyCount = 0;
    private UnityEngine.UI.Text scoreText;
    private UnityEngine.UI.Text highScoreText;
    private UnityEngine.UI.Text timeText;
    private GameStatus gameStatus;
    private float phaseTime = 0f;   // このカウントが経過してから敵が全滅したら次の敵を出すようにする
    private int createEnemyCount = 0; // この数に応じて次のフェイズに行くようにする
    public int phase = -1;          // とりあえずこのフェイズに応じて出す敵を変える
    private readonly float defaultPhaseTime = 10f; // 10にする？
    private float tutotialTime = 5.0f;
    private float intervalTime = 3.0f;
    [SerializeField]private bool noEnemyMode = false; // true で敵が出てこない
    public float bossLimitTime = 30f;
    private float forceBossTime = 30f;
    private int thisPhaseRandomNumber = 1;
    private bool isAddEnemy = false;
    public bool forceAddEnemy = false;
    private int gameTimerMinute = 1;
    private float gameTimerSecond = 30;
    private SoundManager soundManager;
    private int totsugekiTeamCount = 0;
    private int totsugekiTeamWait = 10;
    // Use this for initialization
    void Start () {
        gameStatus = GameStatus.GetInstance();
        this.scoreText = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        this.highScoreText = GameObject.Find("HI_Score").GetComponent<UnityEngine.UI.Text>();
        this.timeText = GameObject.Find("Timer").GetComponent<UnityEngine.UI.Text>();
        this.highScoreText.text = gameStatus.HighScore1.ToString();
        this.gameStatus.EarthHp = 100;
        this.gameStatus.Rank = 0;
        this.gameStatus.RankNumber = 0;
        this.gameStatus.EnemyKillCount = 0;
        this.forceBossTime = bossLimitTime;
        this.createEnemyCount = 0;
        this.thisPhaseRandomNumber = Random.Range(1, 3);
        this.isAddEnemy = forceAddEnemy;
        //this.phase = 3; // -1 チュートリアル 0 インターバル 1 隕石 2 突撃 3 連射 4 隕石+突撃 5 隕石+連射 6 突撃+連射 100 要塞
        this.ResetPhaseTime();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        // チュートリアルでなければ消す
        if (this.phase > -1)
        {
            GameObject.Find("Tutorial_Panel").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー制御
        this.gameTimerSecond -= Time.deltaTime;
        if(this.gameTimerSecond <= 0)
        {
            if(this.gameTimerMinute <= 0)
            {
                // 必ず数字が入っている前提
                gameStatus.Score = int.Parse(this.scoreText.text);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            }
            else
            {
                this.gameTimerMinute -= 1;
                this.gameTimerSecond = 60;
            }
        }
        this.timeText.text = string.Format("{0:00}:{1:00}:00", this.gameTimerMinute, this.gameTimerSecond);

        //this.phaseTime -= Time.deltaTime;
        if (this.phase != 100)
        {
            this.forceBossTime -= Time.deltaTime;
        }
        if (this.gameStatus.EnemyKillCount > 100 || this.forceBossTime < 0)
        {
            this.ResetPhaseTime();
            this.phase = 100;
        }
        if (this.phaseTime <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            {
                if(!noEnemyMode)
                {
                    if(this.phase == 100)
                    {
                        this.phase = -1; // 結果 0 になる。
                        this.intervalTime = 1.0f;// 要塞撃破後、次に敵が出るまでの待ち時間
                    }
                    this.phase++;
                }
                this.ResetPhaseTime();
            }
        }
        else
        {
            // チュートリアル
            if( this.phase == -1)
            {
                this.tutotialTime -= Time.deltaTime;

                // androidであれば無視する(暫定対応)
                if (Application.platform != RuntimePlatform.Android)
                {
                    if (Input.GetMouseButton(0))
                    {
                        Vector3 position = Input.mousePosition;
                        if (position.x > 800 && position.y < 1790 && position.x < 1055 && position.y > 1725)
                        {
                            this.tutotialTime = 0;
                        }
                    }
                }
                if (Input.touchCount > 0)
                {
                    Vector3 position = Input.GetTouch(0).position;
                    if (position.x > 800 && position.y < 1790 && position.x < 1055 && position.y > 1725)
                    {
                        this.tutotialTime = 0;
                    }
                }

                if (this.tutotialTime < 0)
                {
                    this.phaseTime = 0;
                    GameObject.Find("Tutorial_Panel").SetActive(false);
                }
            }
            // インターバル
            else if (this.phase == 0)
            {
                this.intervalTime -= Time.deltaTime;
                if(this.intervalTime < 0)
                {
                    this.phaseTime = 0;
                }
            }
            // 隕石
            else if (this.phase == 1)
            {
                GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Enemy");
                int enemyCount = gameObjectArray.Length;
                int insekiCount = 0;
                int enemyLimit = 20;
                foreach (GameObject gameObject in gameObjectArray)
                {
                    if (gameObject.name.StartsWith("Enemy_00_innseki_0"))
                    {
                        insekiCount++;
                    }
                }
                if (insekiCount < 3)
                {
                    //Enemy_00_innseki_01 と Enemy_00_innseki_02 をランダムで出す
                    string enemyObjectName = "Enemy_00_innseki_0" + Random.Range(1, 3);
                    GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                    createEnemyCount++;
                }

                if (this.isAddEnemy)
                {
                    enemyLimit = 30;
                    if (this.thisPhaseRandomNumber == 1)
                    {
                        if (enemyCount - insekiCount < 1)
                        {
                            string enemyObjectName = "Enemy_01_totugeki";
                            GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            createEnemyCount++;
                        }
                    }
                    else
                    {
                        if (enemyCount - insekiCount < 2)
                        {
                            int pattern = Random.Range(1, 3);
                            if (pattern == 1)
                            {
                                //連射タイプ
                                string enemyObjectName = "Enemy_02_syageki_01";
                                GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            }
                            else
                            {
                                // レーザー撃つタイプ
                                string enemyObjectName = "Enemy_02_syageki_02";
                                GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            }
                            createEnemyCount++;
                        }
                    }                    
                }

                if (createEnemyCount >= enemyLimit)
                {
                    this.phaseTime = 0;
                    this.thisPhaseRandomNumber = Random.Range(1, 3);
                    createEnemyCount = 0;
                }
            }
            else if (this.phase == 2)
            {
                GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Enemy");
                int enemyCount = gameObjectArray.Length;
                int totsugekiCount = 0;
                int enemyLimit = 20;
                foreach (GameObject gameObject in gameObjectArray)
                {
                    if (gameObject.name.StartsWith("Enemy_01_totugeki"))
                    {
                        totsugekiCount++;
                    }
                }
                if (totsugekiCount < 1)
                {
                    string enemyObjectName = "Enemy_01_totugeki";
                    GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                    //EnemyMain enemyMain = enemyObject.GetComponent<EnemyMain>();
                    createEnemyCount++;
                    this.totsugekiTeamCount = 4;
                }
                else
                {
                    if(this.totsugekiTeamCount > 0)
                    {
                        this.totsugekiTeamWait -= 1;
                        if (this.totsugekiTeamWait <= 0)
                        {
                            string enemyObjectName = "Enemy_01_totugeki";
                            Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            createEnemyCount++;
                            this.totsugekiTeamCount--;
                            //Debug.Log("totsugeki:" + totsugekiCount);
                            this.totsugekiTeamWait = 10;
                        }
                    }
                }

                if (this.isAddEnemy)
                {
                    enemyLimit = 30;
                    if (this.thisPhaseRandomNumber == 1)
                    {
                        if (enemyCount - totsugekiCount < 3)
                        {
                            //Enemy_00_innseki_01 と Enemy_00_innseki_02 をランダムで出す
                            string enemyObjectName = "Enemy_00_innseki_0" + Random.Range(1, 3);
                            GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            createEnemyCount++;
                        }
                    }
                    else
                    {
                        if (enemyCount - totsugekiCount < 2)
                        {
                            int pattern = Random.Range(1, 3);
                            if (pattern == 1)
                            {
                                //連射タイプ
                                string enemyObjectName = "Enemy_02_syageki_01";
                                GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            }
                            else
                            {
                                // レーザー撃つタイプ
                                string enemyObjectName = "Enemy_02_syageki_02";
                                GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            }
                            createEnemyCount++;
                        }
                    }
                }

                if (createEnemyCount >= enemyLimit)
                {
                    this.phaseTime = 0;
                    this.thisPhaseRandomNumber = Random.Range(1, 3);
                    createEnemyCount = 0;
                }
            }
            else if (this.phase == 3)
            {
                GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Enemy");
                int enemyCount = gameObjectArray.Length;
                int shagekiCount = 0;
                int shagekiRenshaCount = 0;
                int shagekiLaserCount = 0;
                int enemyLimit = 20;
                foreach (GameObject gameObject in gameObjectArray)
                {
                    if (gameObject.name.StartsWith("Enemy_02_syageki_01"))
                    {
                        shagekiRenshaCount++;
                    }
                    else if (gameObject.name.StartsWith("Enemy_02_syageki_02"))
                    {
                        shagekiLaserCount++;
                    }
                }
                shagekiCount = shagekiRenshaCount + shagekiLaserCount;
                if (shagekiRenshaCount < 1)
                {
                    //連射タイプ
                    string enemyObjectName = "Enemy_02_syageki_01";
                    GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                    createEnemyCount++;
                }
                else if (shagekiLaserCount < 1)
                {
                    // レーザー撃つタイプ
                    string enemyObjectName = "Enemy_02_syageki_02";
                    GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                    createEnemyCount++;
                }

                if (this.isAddEnemy)
                {
                    enemyLimit = 30;
                    if (this.thisPhaseRandomNumber == 1)
                    {
                        if (enemyCount - shagekiCount < 3)
                        {
                            //Enemy_00_innseki_01 と Enemy_00_innseki_02 をランダムで出す
                            string enemyObjectName = "Enemy_00_innseki_0" + Random.Range(1, 3);
                            GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            createEnemyCount++;
                        }
                    }
                    else
                    {
                        if (enemyCount - shagekiCount < 3)
                        {
                            string enemyObjectName = "Enemy_01_totugeki";
                            GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
                            createEnemyCount++;
                        }
                    }
                }

                if (createEnemyCount >= enemyLimit)
                {
                    this.phaseTime = 0;
                    createEnemyCount = 0;
                    this.isAddEnemy = true;
                    this.thisPhaseRandomNumber = Random.Range(1, 3);
                    this.phase = 0; // 次は 1 に行きたいので 0 に戻す。作りが悪い
                }
            }
            else if (this.phase == 100)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
                {
                    string enemyObjectName = "Enemy_03_yousai";
                    GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));

                    this.phaseTime = 0;
                    this.forceBossTime = bossLimitTime;
                    this.gameStatus.EnemyKillCount = 0;
                }
            }
            else
            {
                // 変な状態になっているので、 1 に戻す(要検討)
                this.ResetPhaseTime();
                this.phase = 1;
            }
        }
        if (gameStatus.EarthHp <= 0)
        {
            soundManager.PlayAudioClip(SoundManager.AudioClipType.ET_EXPLOSION);
            // 必ず数字が入っている前提
            gameStatus.Score = int.Parse(this.scoreText.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetString("HighScore1", GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text);
            Debug.Log("ハイスコア保存");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteKey("HighScore1");
            Debug.Log("ハイスコア削除");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 現在のランクゲージ取得
            float nowEnemyLevel = GameStatus.GetInstance().Rank;
        }
        // 上キー
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameStatus.GetInstance().Rank += 10f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameStatus.GetInstance().Rank -= 10f;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 隕石生成
            string enemyObjectName = "Enemy_00_innseki_0" + Random.Range(1, 3).ToString();
            GameObject enemyObject = (GameObject)Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/" + enemyObjectName));
        }
        if(GameStatus.GetInstance().isShowLvEffect)
        {
            Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/LV_up"));
            GameStatus.GetInstance().isShowLvEffect = false;
            soundManager.PlayAudioClip(SoundManager.AudioClipType.LVUP);
        }
    }

    private void ResetPhaseTime()
    {
        this.phaseTime = this.defaultPhaseTime;
    }
}
