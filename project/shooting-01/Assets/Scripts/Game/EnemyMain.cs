using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour {

    Enemy enemy = null;
    private int flashWait = 0;
    private SoundManager soundManager;

    // Use this for initialization
    void Start () {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        // オブジェクト名前方一致
        if (this.gameObject.name.StartsWith("Enemy_00_innseki_0"))
        {
            enemy = new EnemyInnseki();
            // 乱数から特定パターンの出現位置を決定する
            int pattern = Random.Range(-2, 3);
            this.gameObject.transform.position = new Vector3((pattern * 0.75f), 4f, 0);
        }
        else if (this.gameObject.name.StartsWith("Enemy_01_totugeki"))
        {
            this.gameObject.transform.position = new Vector3(-3.5f, 4f, 0);

            enemy = new EnemyTotugeki(GameStatus.GetInstance().enemyTotugekiPram.pettern);
            GameStatus.GetInstance().enemyTotugekiPram.petternCount += 1;
            if(GameStatus.GetInstance().enemyTotugekiPram.petternCount >= 5)
            {
                GameStatus.GetInstance().enemyTotugekiPram.petternCount = 0;
                // 最大だったら 1 にするが必要
                GameStatus.GetInstance().enemyTotugekiPram.pettern += 1;
                if (GameStatus.GetInstance().enemyTotugekiPram.pettern > 4)
                {
                    GameStatus.GetInstance().enemyTotugekiPram.pettern = 1;
                }
            }
            // 突撃敵は音を出すらしい
            soundManager.PlayAudioClip(SoundManager.AudioClipType.NORIMONOUFO);
        }
        else if (this.gameObject.name.StartsWith("Enemy_02_syageki_01"))
        {
            // TODO とりあえず場所ランダム生成だが、既に出ている場所には出ないなど作りを考える
            enemy = new EnemySyageki(Random.Range(1, 3), 1);
            this.gameObject.transform.position = new Vector3(-3f, 4f, 0);
        }
        else if (this.gameObject.name.StartsWith("Enemy_02_syageki_02"))
        {
            // TODO とりあえず場所固定
            enemy = new EnemySyageki(3, 2);
            this.gameObject.transform.position = new Vector3(-3f, 4f, 0);
        }
        else if (this.gameObject.name.StartsWith("Enemy_03_yousai"))
        {
            enemy = new EnemyYousai();
            this.gameObject.transform.position = new Vector3(0f, 6f, 0);
            // ↓この二つの数字を変えて要塞のランダム回転幅を調整してください。現在は 0 度 ～ 360 度の範囲でランダムです。
            float randomRangeMin = 0;
            float randomRangeMax = 360;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(randomRangeMin, randomRangeMax));
            // ノコギリ発動音との事だが、敵出現で出すことにする
            soundManager.PlayAudioClip(SoundManager.AudioClipType.SAW);
        }
        else
        {
            //定義されていない敵種
            Debug.Log("Enemy Is Not Define");
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (this.flashWait > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            this.flashWait--;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        // TODO 突撃はアニメーションで動かしたい
        enemy.Move(this.gameObject);
        
        if (enemy.IsDestroy())
        {
            /*
            GameObject explosionObject = new GameObject();
            explosionObject = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Explosion_sml"), this.gameObject.transform.position, Quaternion.identity);
            */
            GameObject explosionObject;
            if (this.gameObject.name.StartsWith("Enemy_03_yousai"))
            {
                explosionObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Explosion_big 1"));
                soundManager.PlayAudioClip(SoundManager.AudioClipType.FORT_EXPLOSION);
            }
            else
            {
                explosionObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Explosion_sml"));
                GameObject explosionParObject;
                Animator explosionParAnimator;
                for(int i = 0; i < 3; i++)
                {
                    explosionParObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Explosion_par"));
                    explosionParObject.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + Random.Range(-0.8f,0.8f), this.gameObject.transform.position.y + Random.Range(-0.8f, 0.8f), this.gameObject.transform.position.z);
                    explosionParObject.gameObject.transform.localScale = new Vector3(Random.Range(1, 3), Random.Range(1, 3));
                    explosionParAnimator = explosionParObject.GetComponent<Animator>();
                    explosionParAnimator.Play("efc_exp_par", 0, 0.7f);
                }
                SoundManager.AudioClipType explosion;
                if (Random.Range(0, 2) == 0)
                {
                    explosion = SoundManager.AudioClipType.EXPLOSION1;
                }
                else
                {
                    explosion = SoundManager.AudioClipType.EXPLOSION2;
                }
                soundManager.PlayAudioClip(explosion);
            }
            explosionObject.gameObject.transform.position = this.gameObject.transform.position;

            enemy.ScoreUp();
            Destroy(this.gameObject);
            GameStatus.GetInstance().EnemyKillCount += 1;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        // TODO ダメージ受けてる自機の場合はすり抜けるように
        if (other.gameObject.tag == "Player")
        {
            // 要塞は消えない
            if (!this.gameObject.name.StartsWith("Enemy_03_yousai"))
            {
                enemy.Destroy();
            }
            PlayerMain playerMain = GameObject.Find("PlayerMain").GetComponent<PlayerMain>();
            if (other.gameObject.name == "Player_shield(Clone)")
            {
                playerMain.Damage(other.gameObject.transform.parent.name);
            }
            else
            {
                playerMain.Damage(other.gameObject.name);
            }
        }
        if (other.gameObject.tag == "DamageObject")
        {
            BulletMain bulletMain = other.gameObject.GetComponent<BulletMain>();
            if(bulletMain.target == BulletTarget.Player)
            {
                return;
            }

            enemy.Damage(bulletMain.GetDamage());
            this.flashWait = 1;

            GameStatus.GetInstance().Rank += 25f;
        }
        if (other.gameObject.tag == "Earth")
        {
            if(this.gameObject.name.StartsWith("Enemy_03_yousai"))
            {
                GameStatus.GetInstance().EarthHp = 0;
            }

            enemy.score = 0;
            enemy.Destroy();
        }
    }

    public void HitBullet(int power)
    {
        //enemyInnseki.Damage(power);
    }
}
