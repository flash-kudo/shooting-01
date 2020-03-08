using UnityEngine;

/// <summary>
/// 誰に対しての攻撃か
/// </summary>
public enum BulletTarget
{
    Enemy,
    Player
}

class GameStatus
{
    static GameStatus entity = null;
    private UnityEngine.UI.Slider earthSlider = null;
    private UnityEngine.UI.Slider rankSlider = null;

    private int score = 0;
    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            // TODO 各ハイスコア(1 ～ 5位？)と比較するようにする
            if (this.score > this.HighScore1)
            {
                PlayerPrefs.SetString("HighScore1", value.ToString());
            }
        }
    }

    public int HighScore1
    {
        get
        {
            if (PlayerPrefs.HasKey("HighScore1"))
            {
                // 整数が入っている前提
                return int.Parse(PlayerPrefs.GetString("HighScore1"));
            }
            return 0;
        }
        set
        {
            int highScore;
            if (PlayerPrefs.HasKey("HighScore1"))
            {
                if (int.TryParse(PlayerPrefs.GetString("HighScore1"), out highScore))
                {
                    // TODO 各ハイスコア(1 ～ 5位？)と比較するようにする
                    if (value > highScore)
                    {
                        // ハイスコアであれば PlayerPrefs に保存する
                        PlayerPrefs.SetString("HighScore1", value.ToString());
                    }
                }
            }
            else
            {
                PlayerPrefs.SetString("HighScore1", value.ToString());
            }
        }
    }

    private float earthHp = 10;
    public float EarthHp
    {
        get
        {
            return this.earthHp;
        }
        set
        {
            // 暫定でゲームシーン以外から呼ばれたらエラーにする
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Game")
            {
                throw new UnityException("game scene only");
            }
            if (this.earthSlider == null)
            {
                this.earthSlider = GameObject.Find("Earth_Panel").transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>();
            }
            this.earthSlider.value = value;
            this.earthHp = value;
        }
    }

    private float rank = 0;
    public float Rank
    {
        get
        {
            return this.rank;
        }
        set
        {
            // 暫定でゲームシーン以外から呼ばれたらエラーにする
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Game")
            {
                throw new UnityException("game scene only");
            }
            if (this.rankSlider == null)
            {
                this.rankSlider = GameObject.Find("Rank_Panel").transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>();
            }
            // ゲージ MAX でさらに増えたらランクアップ
            if (this.rank == 100 && value > 100)
            {
                isShowLvEffect = true;

                // ランクのカウントアップ
                UnityEngine.UI.Text rankText = GameObject.Find("Rank").GetComponent<UnityEngine.UI.Text>();
                int nowRank = int.Parse(rankText.text);
                rankText.text = (nowRank + 1).ToString();
                RankNumber = nowRank + 1;
                // ランクアップしたらリセット
                this.rankSlider.value = 0;
            }
            //
            else if(this.rank == 0 && value < 0)
            {
                UnityEngine.UI.Text rankText = GameObject.Find("Rank").GetComponent<UnityEngine.UI.Text>();
                int nowRank = int.Parse(rankText.text);
                if (nowRank > 0)
                {
                    rankText.text = (nowRank - 1).ToString();
                    RankNumber = nowRank - 1;
                    // ランクダウンしたら100
                    this.rankSlider.value = 100;
                }
            }
            else
            {
                this.rankSlider.value = value;
            }
            this.rank = this.rankSlider.value;
        }
    }

    public int RankNumber = 0;

    public int EnemyKillCount = 0;

    public bool isShowLvEffect = false;

    static public GameStatus GetInstance()
    {
        if (GameStatus.entity == null)
        {
            GameStatus.entity = new GameStatus();
        }
        return GameStatus.entity;
    }

    public float EnemyRankRate = 0.01f;

    public EnemyInnsekiPram enemyInnsekiPram = new EnemyInnsekiPram();
    public class EnemyInnsekiPram
    {
        public int life = 5;
        public float speed = 0.05f;
    }

    public EnemyTotugekiPram enemyTotugekiPram = new EnemyTotugekiPram();
    public class EnemyTotugekiPram
    {
        public int life = 5;
        public float speed = 0.05f;
        public int pettern = 1;
        public int petternCount = 0;
    }

    public EnemySyagekiPram enemySyagekiPram = new EnemySyagekiPram();
    public class EnemySyagekiPram
    {
        public int life = 20;
        public float speed = 0.05f;
    }

    public EnemyYousaiPram enemyYousaiPram = new EnemyYousaiPram();
    public class EnemyYousaiPram
    {
        public int life = 100;
        public float speed = 0.005f;
    }
}