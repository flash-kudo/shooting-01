using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract class Enemy : MonoBehaviour
{
    public int life = 0;
    public int score = 0;   // 倒して得られるスコア
    protected Vector2[] moveDestination;  // 移動先配列
    protected int nextDestination = 0;        // 次は moveDestination の何番目要素を移動先にするか
    protected float speed;
    protected readonly float endDistance = 0.1f; // この距離より近づいた場合は目的地に到着したと判断
    protected float defaultAttackWait = 0f;
    protected float attackWait = 0;
    protected float bulletSpeed = 0;
    protected int type = 0; // 敵タイプ 射撃タイプの連射とレーザー分ける用など

    public Enemy(int life, int score)
    {
        this.life = life;
        this.score = score;
    }

    public void Damage(int power)
    {
        this.life -= power;
    }

    // 敵が要塞の場合は無効にする
    public void Destroy()
    {
        this.life = 0;
    }

    public bool IsDestroy()
    {
        if(this.life <= 0)
        {
            return true;
        }
        return false;
    }

    // 一部のみ使用
    public virtual void Attack(GameObject enemyGameObject)
    {
    }

    public virtual void Move(GameObject enemyGameObject)
    {
    }

    public void ScoreUp()
    {
        UnityEngine.UI.Text nowScore = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        nowScore.text = (int.Parse(nowScore.text) + GameStatus.GetInstance().RankNumber * this.score).ToString();
    }
}

class EnemyInnseki : Enemy
{
    public EnemyInnseki() : base(5, 100)
    {
        // TODO ここでランダム生成ではなく設定できるようにする
        moveDestination = new Vector2[1];
        int pattern = Random.Range(-1, 2); // パターン 1 なら左、2 なら真ん中、3 なら右に移動
        moveDestination[0] = new Vector2((pattern * 1.5f), -3.5f);
        //speed = 0.05f;
        life = GameStatus.GetInstance().enemyInnsekiPram.life;
        speed = GameStatus.GetInstance().enemyInnsekiPram.speed;
        // 高ランク時は難易度を上げる
        {
            this.life *= (int)(1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate);
            this.speed *= 1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate;
            // 巨大化させる
        }
    }

    // TODO オブジェクト渡さないでこちらで持つか、作りを変える
    public override void Move(GameObject enemyGameObject)
    {
        Vector3 nowPosition = enemyGameObject.transform.position;

        if (nowPosition.y < -3)
        {
            GameObject.Destroy(enemyGameObject);
        }

        Vector2 sub = Camera.main.ScreenToWorldPoint(nowPosition) - Camera.main.ScreenToWorldPoint(moveDestination[0]);
        float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg + 90f;
        float rad = angle * Mathf.Deg2Rad;
        nowPosition.x -= Mathf.Sin(rad) * this.speed;
        nowPosition.y += Mathf.Cos(rad) * this.speed;
        enemyGameObject.transform.position = nowPosition;
    }
}

class EnemyTotugeki : Enemy
{
       public int pettern = 0;
    public EnemyTotugeki() : base(5, 200)
    {
        this.pettern = Random.Range(1, 5);
        this.Init();
    }
    // とりあえず作成 ちゃんとやるならこれはやめる
    public EnemyTotugeki(int pattern) : base(5, 200)
    {
        this.pettern = pattern;
        this.Init();
    }

    private void Init()
    {
        //TODO 暫定で座標設定して移動するのでアニメーションにする
        // 移動パターン 1 
        if (this.pettern == 1)
        {
            moveDestination = new Vector2[3];
            moveDestination[0] = new Vector2(2f, 4f);
            moveDestination[1] = new Vector2(-1f, 2.5f);
            moveDestination[2] = new Vector2(-2f, -3.5f);
        }
        else if(this.pettern == 2)
        {
            // 移動パターン 2
            moveDestination = new Vector2[7];
            moveDestination[0] = new Vector2(-1f, 3f);
            moveDestination[1] = new Vector2(-0.5f, 2.5f);
            moveDestination[2] = new Vector2(-1f, 2f);
            moveDestination[3] = new Vector2(-1.5f, 2.5f);
            moveDestination[4] = new Vector2(-1f, 3f);
            moveDestination[5] = new Vector2(2f, 3.5f);
            moveDestination[6] = new Vector2(1f, -3.5f);
        }
        else if (this.pettern == 3)
        {
            // 移動パターン 3 (エクセルでは5)
            moveDestination = new Vector2[3];
            moveDestination[0] = new Vector2(-1f, 2.5f);
            moveDestination[1] = new Vector2(2f, 3.5f);
            moveDestination[2] = new Vector2(1f, -3.5f);
        }
        else
        {
            // 移動パターン 4 (エクセルでは7)
            moveDestination = new Vector2[6];
            moveDestination[0] = new Vector2(-1f, 5f);
            moveDestination[1] = new Vector2(2f, 3.5f);
            moveDestination[2] = new Vector2(1.5f, 3f);
            moveDestination[3] = new Vector2(1f, 3.5f);
            moveDestination[4] = new Vector2(2f, 4f);
            moveDestination[5] = new Vector2(1f, -3.5f);
        }
        //speed = 0.05f;
        life = GameStatus.GetInstance().enemyTotugekiPram.life;
        speed = GameStatus.GetInstance().enemyTotugekiPram.speed;
        // 高ランク時は難易度を上げる
        {
            this.life *= (int)(1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate);
            this.speed *= 1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate;
            // 編隊を増やす
        }
    }

    public int GetPettern()
    {
        return this.pettern;
    }

    // TODO オブジェクト渡さないでこちらで持つか、作りを変える
    public override void Move(GameObject enemyGameObject)
    {
        Vector3 nowPosition = enemyGameObject.transform.position;

        if (nowPosition.y < -3)
        {
            GameObject.Destroy(enemyGameObject);
        }

        Vector2 sub = Camera.main.ScreenToWorldPoint(nowPosition) - Camera.main.ScreenToWorldPoint(this.moveDestination[this.nextDestination]);
        float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg + 90f;
        float rad = angle * Mathf.Deg2Rad;
        nowPosition.x -= Mathf.Sin(rad) * this.speed;
        nowPosition.y += Mathf.Cos(rad) * this.speed;
        enemyGameObject.transform.position = nowPosition;
        enemyGameObject.transform.eulerAngles = new Vector3(0, 0, angle + 180);

        // 目的地との距離が規定値(endDistance)より近づいたら次の移動先へ
        if (Vector2.Distance(nowPosition, this.moveDestination[this.nextDestination]) < this.endDistance)
        {
            // 最後の移動先に到達したら何もしない (到達前に地球に当たるはず)
            if(this.nextDestination < (this.moveDestination.Length - 1))
            {
                this.nextDestination++;
                if(this.nextDestination == (this.moveDestination.Length - 1))
                {
                    this.speed = 0.3f;
                }
            }
        }
    }
}

class EnemySyageki : Enemy
{
    //private GameObject bulletLine = null; // 攻撃予測線
    public EnemySyageki(int pattern, int type) : base(20, 500)
    {
        // TODO ここでランダム生成ではなく設定できるようにする
        moveDestination = new Vector2[1];
        // パターン 1 なら左、2 なら真ん中、3 なら右に移動
        moveDestination[0] = new Vector2(((pattern - 2) * 1.5f), 4f);
        //speed = 0.05f;
        life = GameStatus.GetInstance().enemySyagekiPram.life;
        speed = GameStatus.GetInstance().enemySyagekiPram.speed;
        this.type = type;
        if (this.type == 1)
        {
            this.bulletSpeed = 0.02f;
            defaultAttackWait = 0.3f;
        }
        else
        {
            this.bulletSpeed = 0.2f;
            defaultAttackWait = 4f;
        }
        // 高ランク時は難易度を上げる
        {
            this.life *= (int)(1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate);
            this.speed *= 1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate;
            this.bulletSpeed *= 1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate;
            // 弾の威力を上げる
        }
    }

    // TODO オブジェクト渡さないでこちらで持つか、作りを変える
    public override void Move(GameObject enemyGameObject)
    {
        Vector3 nowPosition = enemyGameObject.transform.position;

        // 目的地との距離が規定値(endDistance)より近づいたら移動終了
        if (Vector2.Distance(nowPosition, moveDestination[0]) < this.endDistance)
        {
            this.Attack(enemyGameObject);
            return;
        }

        if (nowPosition.y < -3)
        {
            GameObject.Destroy(enemyGameObject);
        }

        Vector2 sub = Camera.main.ScreenToWorldPoint(nowPosition) - Camera.main.ScreenToWorldPoint(moveDestination[0]);
        float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg + 90f;
        float rad = angle * Mathf.Deg2Rad;
        nowPosition.x -= Mathf.Sin(rad) * this.speed;
        nowPosition.y += Mathf.Cos(rad) * this.speed;
        enemyGameObject.transform.position = nowPosition;
    }

    public override void Attack(GameObject enemyGameObject)
    {
        if(attackWait > 0)
        {
            attackWait -= Time.deltaTime;

            if (this.type == 2)
            {
                /*
                if (this.bulletLine == null)
                {
                    this.bulletLine = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/ShotLine"), enemyGameObject.transform.position, Quaternion.identity);
                    this.bulletLine.transform.parent = enemyGameObject.transform;
                    this.bulletLine.transform.position = new Vector3(this.bulletLine.transform.position.x, this.bulletLine.transform.position.y -50 / 2, 0);
                    this.bulletLine.SetActive(true);
                }
                */
            }            
            return;
        }

        // TODO 自機を狙うようにする
        // 連射タイプ
        if (this.type == 1)
        {
            Vector3 bulletPosition = enemyGameObject.transform.position;
            bulletPosition.y -= 0.5f;
            GameObject bulletObject = GameObject.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Enemy_Bullet_00_small"));
            BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
            bulletMain.Fire(bulletPosition, Random.Range(160, 200), this.bulletSpeed, 1, BulletTarget.Player);
        }
        // レーザータイプ
        else
        {
            Vector3 bulletPosition = enemyGameObject.transform.position;
            // 位置が変なので調整
            bulletPosition.x -= 0.04f;
            bulletPosition.y -= 0.33f;
            GameObject bulletObject = GameObject.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Enemy_Bullet_02_laser"));
            bulletObject.transform.position = bulletPosition;
            Vector3 rot = bulletObject.transform.eulerAngles;
            int targetNum = Random.Range(0, 4);
            string targetObjectName = "";
            switch(targetNum)
            {
                case 0:
                    targetObjectName = "Player_rensya";
                    break;
                case 1:
                    targetObjectName = "Player_kakusan";
                    break;
                case 2:
                    targetObjectName = "Player_sogeki";
                    break;
                case 3:
                    targetObjectName = "Background_Earth";
                    break;
                default:
                    break;
            }
            GameObject targetObject = GameObject.Find(targetObjectName);
            Vector2 sub = Camera.main.ScreenToWorldPoint(bulletObject.transform.position) - Camera.main.ScreenToWorldPoint(targetObject.transform.position);
            float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg -90f;
            rot.z = angle;
            bulletObject.transform.eulerAngles = rot;
            //BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
            //bulletMain.Fire(bulletPosition, Random.Range(-45, 45), 0, 1, BulletTarget.Player);
            GameObject shotEffectObject = GameObject.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Enemy_Bullet_01_laser_moto"));
            // 位置が変なのでさらに調整
            bulletPosition.y -= 0.25f;
            shotEffectObject.transform.position = bulletPosition;
        }

        /*
        float rad = this.selectPlayer.angle * Mathf.Deg2Rad;
        bulletPosition.x -= Mathf.Sin(rad) * this.selectPlayer.fireConfig.margin;
        bulletPosition.y += Mathf.Cos(rad) * this.selectPlayer.fireConfig.margin;

        GameObject bulletObject = Instantiate(this.selectPlayer.fireConfig.bulletGameObject);
        BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
        bulletMain.Fire(bulletPosition, this.selectPlayer.angle, 0.20f, this.selectPlayer.fireConfig.damage, BulletTarget.Enemy);
        */

        attackWait = defaultAttackWait;
    }
}

class EnemyYousai : Enemy
{
    public EnemyYousai() : base(100, 10000)
    {
        moveDestination = new Vector2[1];
        moveDestination[0] = new Vector2(0f, -3.5f);
        //speed = 0.005f;
        life = GameStatus.GetInstance().enemyYousaiPram.life;
        speed = GameStatus.GetInstance().enemyYousaiPram.speed;
        defaultAttackWait = 0.3f;
        this.bulletSpeed = 0.02f;
        // 高ランク時は難易度を上げる
        {
            this.life *= (int)(1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate);
            this.speed *= 1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate;
            this.bulletSpeed *= 1 + GameStatus.GetInstance().RankNumber * GameStatus.GetInstance().EnemyRankRate;
            this.defaultAttackWait = 0.1f;
            // 弾の威力を上げる
        }
    }

    // TODO オブジェクト渡さないでこちらで持つか、作りを変える
    public override void Move(GameObject enemyGameObject)
    {
        Vector3 nowPosition = enemyGameObject.transform.position;

        if (nowPosition.y < -3)
        {
            GameObject.Destroy(enemyGameObject);
        }

        Vector2 sub = Camera.main.ScreenToWorldPoint(nowPosition) - Camera.main.ScreenToWorldPoint(moveDestination[0]);
        float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg + 90f;
        float rad = angle * Mathf.Deg2Rad;
        nowPosition.x -= Mathf.Sin(rad) * this.speed;
        nowPosition.y += Mathf.Cos(rad) * this.speed;
        enemyGameObject.transform.position = nowPosition;

        // 要塞は移動しながら常に攻撃する
        this.Attack(enemyGameObject);
    }

    public override void Attack(GameObject enemyGameObject)
    {
        if (attackWait > 0)
        {
            attackWait -= Time.deltaTime;
            return;
        }

        Vector3 bulletPosition = enemyGameObject.transform.position;
        // ランダムな角度に弾を撃つ
        float angle = Random.Range(-90, 291);
        float rad = angle * Mathf.Deg2Rad;
        bulletPosition.x -= Mathf.Sin(rad) * 1.3f;
        bulletPosition.y += Mathf.Cos(rad) * 1.3f;
        GameObject bulletObject = GameObject.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Enemy_Bullet_00_small"));
        BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
        bulletMain.Fire(bulletPosition, angle, this.bulletSpeed, 1, BulletTarget.Player);

        attackWait = defaultAttackWait;
    }
}