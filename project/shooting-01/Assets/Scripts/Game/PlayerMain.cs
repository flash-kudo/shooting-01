using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{   
    private Player selectPlayer;
    private PlayerRensha playerRensha;
    private PlayerKakusan playerKakusan;
    private PlayerSogeki playerSogeki;
    // とりあえず
    private float sogekiChargeCount = 0;
    private int nowChargeLevel = 0;
    private GameObject[] sogekiChargeEffect;
    private float invincibleTime = 0;
    // 自機切り替え時の無敵時間(秒)
    private float defaultInvincibleTime = 1f;
    private SoundManager soundManager;
    private float seRoopCount = 0;

    private enum SelectedPlayer
    {
        PLAYER_RENSHA,
        PLAYER_KAKUSAN,
        PLAYER_SOGEKI
    }

    // Use this for initialization
    void Start()
    {
        this.playerRensha = new PlayerRensha();
        this.playerKakusan = new PlayerKakusan();
        this.playerSogeki = new PlayerSogeki();
        this.sogekiChargeEffect = new GameObject[3];
        // 狙撃チャージエフェクトを生成して子として登録
        this.sogekiChargeEffect[0] = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_02_sogeki_sht_chg_L1"), this.playerSogeki.gameObject.transform.position, Quaternion.identity);        
        this.sogekiChargeEffect[1] = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_02_sogeki_sht_chg_L2"), this.playerSogeki.gameObject.transform.position, Quaternion.identity);
        this.sogekiChargeEffect[2] = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_02_sogeki_sht_chg_L3"), this.playerSogeki.gameObject.transform.position, Quaternion.identity);
        this.sogekiChargeEffect[0].transform.parent = this.playerSogeki.gameObject.transform;
        this.sogekiChargeEffect[1].transform.parent = this.playerSogeki.gameObject.transform;
        this.sogekiChargeEffect[2].transform.parent = this.playerSogeki.gameObject.transform;

        this.sogekiChargeEffect[0].gameObject.SetActive(false);
        this.sogekiChargeEffect[1].gameObject.SetActive(false);
        this.sogekiChargeEffect[2].gameObject.SetActive(false);

        this.selectPlayer = playerRensha; // 初期選択自機
        this.selectPlayer.DisableShield(true);

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.ChangeSelectPlayer();

        // androidであれば無視する(暫定対応)
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.selectPlayer.SetMoveDestination(Input.mousePosition);
            }
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    this.selectPlayer.SetMoveDestination(touch.position);
                    break;
                default:
                    break;
            }
        }

        this.Move();
        this.Fire();
        this.CountDamageWait();

        // チャージカウントを増やす
        this.sogekiChargeCount += Time.deltaTime;
        if(this.invincibleTime > 0f)
        {
            this.invincibleTime -= Time.deltaTime;
        }
    }

    public void Damage(string playerType)
    {
        // 無敵時間中は何もしない
        if (this.invincibleTime > 0f)
        {
            return;
        }

        //TODO 操作中なら一定時間行動不能にする
        if (selectPlayer.gameObjectName == playerType)
        {
            this.selectPlayer.damageWait = 3f;
            // バリアーで防いだ時はランク増減なし(暫定)
            GameStatus.GetInstance().Rank -= 500f;
            soundManager.PlayAudioClip(SoundManager.AudioClipType.DAMAGE01);
        }
        else
        {
            Vector3 shieldEffectPosition = new Vector3();

            if(this.playerKakusan.gameObjectName == playerType)
            {
                shieldEffectPosition = this.playerKakusan.gameObject.transform.position;
            }
            else if(this.playerRensha.gameObjectName == playerType)
            {
                shieldEffectPosition = this.playerRensha.gameObject.transform.position;
            }
            else if(this.playerSogeki.gameObjectName == playerType)
            {
                shieldEffectPosition = this.playerSogeki.gameObject.transform.position;
            }

            // ここにバリヤエフェクト表示
            // バリアーを子オブジェクトとして生成する TODO プレハブを自機分ロードするのは無駄だが・・・
            GameObject shieldEffectObject = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_shield_hit"), shieldEffectPosition, Quaternion.identity);
            // 作成したオブジェクトを子として登録
            shieldEffectObject.transform.parent = this.gameObject.transform;
            soundManager.PlayAudioClip(SoundManager.AudioClipType.REFLECTION);
        }
    }

    private void Move()
    {
        // 別の自機を選択しても移動が続く場合もあるため、全機移動させる
        this.playerRensha.Move();
        this.playerKakusan.Move();
        this.playerSogeki.Move();
    }

    private void Fire()
    {
        // ショットアニメーション(とりあえず連射だけ)
        if (this.selectPlayer.gameObjectName == this.playerRensha.gameObjectName)
        {
            this.selectPlayer.gameObject.GetComponent<Animator>().SetBool("ModelShotPlayerRensha", this.selectPlayer.fireConfig.isFire);
        }

        if (this.selectPlayer.IsDamageWait())
        {
            return;
        }

        if (!this.selectPlayer.fireConfig.isFire)
        {
            this.selectPlayer.fireConfig.wait = 0;
            return;
        }

        this.selectPlayer.fireConfig.wait -= Time.deltaTime;

        if (this.selectPlayer.fireConfig.wait <= 0.0)
        {
            // 拡散
            if (this.selectPlayer.gameObjectName == this.playerKakusan.gameObjectName)
            {
                this.selectPlayer.fireConfig.wait = 1.0f / selectPlayer.fireConfig.firePerFrame;
                // TODO ウェイトを変える
                float rad;
                // 押下時に複数弾を作って攻撃し、フラグは立てない(長押しで何もしない)
                for (int i = -5; i <= 5; i++)
                {
                    Vector3 bulletPosition = this.selectPlayer.gameObject.transform.position;
                    rad = (this.selectPlayer.angle + i * 5) * Mathf.Deg2Rad;
                    bulletPosition.x -= Mathf.Sin(rad) * this.selectPlayer.fireConfig.margin;
                    bulletPosition.y += Mathf.Cos(rad) * this.selectPlayer.fireConfig.margin;
                    GameObject bulletObject = Instantiate(this.selectPlayer.fireConfig.bulletGameObject);
                    BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
                    bulletMain.Fire(bulletPosition, (this.selectPlayer.angle + i * 5), 0.1f, this.selectPlayer.fireConfig.damage, BulletTarget.Enemy);

                }
                GameObject muzzleObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_01_kakusan_muzzle"));
                Vector3 muzzlePosition = this.selectPlayer.gameObject.transform.position;
                rad = this.selectPlayer.angle * Mathf.Deg2Rad;
                muzzlePosition.x -= Mathf.Sin(rad) * 0.6f;
                muzzlePosition.y += Mathf.Cos(rad) * 0.6f;
                muzzleObject.gameObject.transform.position = muzzlePosition;
                muzzleObject.gameObject.transform.rotation = this.selectPlayer.gameObject.transform.rotation;
            }
            // 連射
            else
            {
                this.selectPlayer.fireConfig.wait = 1.0f / selectPlayer.fireConfig.firePerFrame;
                // 左側の弾の初期位置を砲台の位置までずらす
                Vector3 bulletPosition = this.selectPlayer.gameObject.transform.position;
                float rad = (this.selectPlayer.angle + 7) * Mathf.Deg2Rad;
                bulletPosition.x -= Mathf.Sin(rad) * this.selectPlayer.fireConfig.margin;
                bulletPosition.y += Mathf.Cos(rad) * this.selectPlayer.fireConfig.margin;
                GameObject bulletObject = Instantiate(this.selectPlayer.fireConfig.bulletGameObject);
                BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
                bulletMain.Fire(bulletPosition, this.selectPlayer.angle, 0.20f, this.selectPlayer.fireConfig.damage, BulletTarget.Enemy);

                // 右側の弾
                bulletPosition = this.selectPlayer.gameObject.transform.position;
                rad = (this.selectPlayer.angle - 7) * Mathf.Deg2Rad;
                bulletPosition.x -= Mathf.Sin(rad) * this.selectPlayer.fireConfig.margin;
                bulletPosition.y += Mathf.Cos(rad) * this.selectPlayer.fireConfig.margin;
                bulletObject = Instantiate(this.selectPlayer.fireConfig.bulletGameObject);
                bulletMain = bulletObject.GetComponent<BulletMain>();
                bulletMain.Fire(bulletPosition, this.selectPlayer.angle, 0.20f, this.selectPlayer.fireConfig.damage, BulletTarget.Enemy);

                // 発射エフェクト
                this.selectPlayer.gameObject.transform.Find("Player_00_rensya_muzzle_left").gameObject.SetActive(true);
                this.selectPlayer.gameObject.transform.Find("Player_00_rensya_muzzle_right").gameObject.SetActive(true);

                soundManager.PlayAudioClip(SoundManager.AudioClipType.MACHINEGUN);
            }
        }
    }

    public void StartFire()
    {
        // 狙撃の場合
        if (selectPlayer.gameObjectName == playerSogeki.gameObjectName)
        {
            if (this.selectPlayer.IsDamageWait())
            {
                return;
            }
            this.sogekiChargeCount = 0;
            // チャージエフェクト表示
            this.SetSogekiEffect(1);
        }
        else if(selectPlayer.gameObjectName == playerKakusan.gameObjectName)
        {
            this.selectPlayer.fireConfig.wait = 1.0f / selectPlayer.fireConfig.firePerFrame;
            this.selectPlayer.fireConfig.isFire = true;
        }
        else
        {
            this.selectPlayer.fireConfig.isFire = true;
        }
    }

    public void SetPlayerAngle(float angle)
    {
        if (this.selectPlayer.IsDamageWait())
        {
            this.SetSogekiEffect(0);
            return;
        }
        if (this.nowChargeLevel == 1 && this.sogekiChargeCount > 0.5f)
        {
            this.SetSogekiEffect(2);
        }
        else if(this.nowChargeLevel == 2 && this.sogekiChargeCount > 1f)
        {
            this.SetSogekiEffect(3);
            this.seRoopCount = 0;
        }
        else if(this.nowChargeLevel == 3)
        {
            this.seRoopCount += Time.deltaTime; ;
            if(this.seRoopCount > 0.5f)
            {
                soundManager.PlayAudioClip(SoundManager.AudioClipType.CHARGE1);
                this.seRoopCount = 0;
            }
        }

        this.selectPlayer.angle = angle;
        this.selectPlayer.gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void EndFire()
    {
        // 狙撃の場合
        if (selectPlayer.gameObjectName == playerSogeki.gameObjectName)
        {
            if (this.selectPlayer.IsDamageWait())
            {
                this.SetSogekiEffect(0);
                return;
            }
            if (this.nowChargeLevel >= 3)
            {
                // 弾の初期位置を砲台の位置までずらす
                Vector3 bulletPosition = this.selectPlayer.gameObject.transform.position;
                float rad = this.selectPlayer.angle * Mathf.Deg2Rad;
                bulletPosition.x -= Mathf.Sin(rad) * this.selectPlayer.fireConfig.margin;
                bulletPosition.y += Mathf.Cos(rad) * this.selectPlayer.fireConfig.margin;

                // 狙撃は発射位置が右に少しずれているため調整する
                float rightMargin = 0.06f;
                rad = (this.selectPlayer.angle - 90) * Mathf.Deg2Rad;
                bulletPosition.x -= Mathf.Sin(rad) * rightMargin;
                bulletPosition.y += Mathf.Cos(rad) * rightMargin;

                GameObject bulletObject = Instantiate(this.selectPlayer.fireConfig.bulletGameObject);
                BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
                // TODO 長押し時間に応じてangleを散らす
                bulletMain.Fire(bulletPosition, this.selectPlayer.angle, 0.20f, this.selectPlayer.fireConfig.damage, BulletTarget.Enemy);

                soundManager.PlayAudioClip(SoundManager.AudioClipType.GUNSHOT_FANTOM01);
            }
            this.sogekiChargeCount = 0;
            this.SetSogekiEffect(0);
        }
        // 拡散の場合
        else if (this.selectPlayer.gameObjectName == this.playerKakusan.gameObjectName)
        {
            this.selectPlayer.fireConfig.isFire = false;

            if (this.selectPlayer.IsDamageWait())
            {
                return;
            }

            float rad;

            // 11 方向に弾を出す
            for (int i = -5; i <= 5; i++)
            {
                Vector3 bulletPosition = this.selectPlayer.gameObject.transform.position;
                rad = (this.selectPlayer.angle + i * 5) * Mathf.Deg2Rad;
                bulletPosition.x -= Mathf.Sin(rad) * this.selectPlayer.fireConfig.margin;
                bulletPosition.y += Mathf.Cos(rad) * this.selectPlayer.fireConfig.margin;
                GameObject bulletObject = Instantiate(this.selectPlayer.fireConfig.bulletGameObject);
                BulletMain bulletMain = bulletObject.GetComponent<BulletMain>();
                bulletMain.Fire(bulletPosition, (this.selectPlayer.angle + i * 5), 0.1f, this.selectPlayer.fireConfig.damage, BulletTarget.Enemy);

            }
            GameObject muzzleObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_01_kakusan_muzzle"));
            Vector3 muzzlePosition = this.selectPlayer.gameObject.transform.position;
            rad = this.selectPlayer.angle * Mathf.Deg2Rad;
            muzzlePosition.x -= Mathf.Sin(rad) * 0.6f;
            muzzlePosition.y += Mathf.Cos(rad) * 0.6f;
            muzzleObject.gameObject.transform.position = muzzlePosition;
            muzzleObject.gameObject.transform.rotation = this.selectPlayer.gameObject.transform.rotation;

            soundManager.PlayAudioClip(SoundManager.AudioClipType.MISSILE);
        }
        else
        {
            this.selectPlayer.gameObject.transform.Find("Player_00_rensya_muzzle_left").gameObject.SetActive(false);
            this.selectPlayer.gameObject.transform.Find("Player_00_rensya_muzzle_right").gameObject.SetActive(false);
            this.selectPlayer.fireConfig.isFire = false;
        }
    }

    // 3 機のどれがクリックされたかのみに使う想定。シールドクリックなら親要素を
    private string GetClickObjectName()
    {
        GameObject clickObject = null;

        // 左クリックされた場所のオブジェクトを取得
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    clickObject = hit.collider.gameObject;
                }
            }
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit))
                    {
                        clickObject = hit.collider.gameObject;
                    }
                    break;
                default:
                    break;
            }
        }

        if(clickObject == null)
        {
            return "";
        }

        // シールドだったら親を取る
        if (clickObject.name.StartsWith("Player_shield") || clickObject.name.StartsWith("Player_shield_hit"))
        {
            clickObject = clickObject.transform.parent.gameObject;
        }

        return clickObject.name;
    }

    private void ChangeSelectPlayer()
    {
        string clickObjectName = GetClickObjectName();
        if(clickObjectName != "" && clickObjectName != this.selectPlayer.gameObjectName)
        {
            soundManager.PlayAudioClip(SoundManager.AudioClipType.PY_SELECT);
        }

        if(clickObjectName == this.playerRensha.gameObjectName)
        {
            this.selectPlayer.EnableShield();
            this.selectPlayer = this.playerRensha;
            // 無敵時間 かならず DisableShield より先に DisableShield に true false つけたほうがいいかも
            this.invincibleTime = this.defaultInvincibleTime;
            this.selectPlayer.DisableShield(false);
        }
        else if(clickObjectName == this.playerKakusan.gameObjectName)
        {
            this.selectPlayer.EnableShield();
            this.selectPlayer = this.playerKakusan;
            // 無敵時間 かならず DisableShield より先に
            this.invincibleTime = this.defaultInvincibleTime;
            this.selectPlayer.DisableShield(false);
        }
        else if(clickObjectName == this.playerSogeki.gameObjectName)
        {
            this.selectPlayer.EnableShield();
            this.selectPlayer = this.playerSogeki;
            // 無敵時間 かならず DisableShield より先に
            this.invincibleTime = this.defaultInvincibleTime;
            this.selectPlayer.DisableShield(false);
        }
    }

    private void CountDamageWait()
    {
        // TODO 作りが悪い
        if (this.playerRensha.IsDamageWait())
        {
            this.playerRensha.damageWait -= Time.deltaTime;
            this.playerRensha.ShowDamageEffect();
            this.playerRensha.moveConfig.isMove = false;
            this.playerRensha.gameObject.transform.position = new Vector3(this.playerRensha.gameObject.transform.position.x, this.playerRensha.gameObject.transform.position.y, 1);
        }
        else
        {
            this.playerRensha.HideDamageEffect();
            this.playerRensha.gameObject.transform.position = new Vector3(this.playerRensha.gameObject.transform.position.x, this.playerRensha.gameObject.transform.position.y, 0);
        }
        if (this.playerKakusan.IsDamageWait())
        {
            this.playerKakusan.damageWait -= Time.deltaTime;
            this.playerKakusan.ShowDamageEffect();
            this.playerKakusan.moveConfig.isMove = false;
            this.playerKakusan.gameObject.transform.position = new Vector3(this.playerKakusan.gameObject.transform.position.x, this.playerKakusan.gameObject.transform.position.y, 1);
        }
        else
        {
            this.playerKakusan.HideDamageEffect();
            this.playerKakusan.gameObject.transform.position = new Vector3(this.playerKakusan.gameObject.transform.position.x, this.playerKakusan.gameObject.transform.position.y, 0);
        }
        if (this.playerSogeki.IsDamageWait())
        {
            this.playerSogeki.damageWait -= Time.deltaTime;
            this.playerSogeki.ShowDamageEffect();
            this.playerSogeki.moveConfig.isMove = false;
            this.playerSogeki.gameObject.transform.position = new Vector3(this.playerSogeki.gameObject.transform.position.x, this.playerSogeki.gameObject.transform.position.y, 1);
        }
        else
        {
            this.playerSogeki.HideDamageEffect();
            this.playerSogeki.gameObject.transform.position = new Vector3(this.playerSogeki.gameObject.transform.position.x, this.playerSogeki.gameObject.transform.position.y, 0);
        }
    }

    // クリア時は 0 で呼ぶ
    private void SetSogekiEffect(int chargeLevel)
    {
        this.nowChargeLevel = chargeLevel;
        for (int i = 1; i <= 3; i++)
        {
            if (i == chargeLevel)
            {
                this.sogekiChargeEffect[i-1].gameObject.SetActive(true);
                if(chargeLevel == 1)
                {
                    soundManager.PlayAudioClip(SoundManager.AudioClipType.CHARGE3);
                }
                else if (chargeLevel == 2)
                {
                    soundManager.PlayAudioClip(SoundManager.AudioClipType.CHARGE2);
                }
                else if (chargeLevel == 3)
                {
                    soundManager.PlayAudioClip(SoundManager.AudioClipType.CHARGE1);
                }
            }
            else
            {
                this.sogekiChargeEffect[i-1].gameObject.SetActive(false);
            }
        }
        
    }
}

abstract class Player
{
    public readonly GameObject gameObject = null;
    public readonly string gameObjectName = "";
    public float angle = 0f;                    // 自機の角度 なくても問題ないが、便利なので
    public float damageWait = 0f;               // ダメージを受けたら値を設定し、規定時間行動不能

    public MoveConfig moveConfig = null;
    public FireConfig fireConfig = null;
    public ShieldConfig shieldConfig = null;
    public float damageEffectDelay = 0f; // 被弾時の点滅を切り替えるフレーム
    public float damageEffectDelayDefault = 0.2f; // 被弾時の点滅を切り替えるフレーム設定値
    public Vector3 defaultScale;
    public GameObject bulletLine; // 攻撃予測線

    // 移動に関する設定
    public class MoveConfig
    {
        public bool isMove = false;
        public float speed = 0.05f;                                             // 移動スピード
        public Vector3 destinationPositionWorldSpace = new Vector3();           // 移動目的地
        public readonly float endDistance = 0.1f;                               // この距離より近づいた場合は目的地に到着したと判断
        public readonly Vector2 limitMinWorldSpace = new Vector2(-2.5f, -3f);   // 移動可能範囲左下
        public readonly Vector2 limitMaxWorldSpace = new Vector2(2.5f, 4.5f);   // 移動可能範囲右上
    }

    // 攻撃に関する設定
    public class FireConfig
    {
        public bool isFire = false;                 // 攻撃するかどうか
        public float firePerFrame = 0f;             // 1フレーム間に何発撃つか
        public float wait = 0f;                     // 次攻撃までの待ち時間(フレーム)
        public int damage = 0;                      // ヒット時のダメージ
        public GameObject bulletGameObject = null;  // 発射する弾オブジェクト
        public float margin = 0f;                   // 発射する弾と自機のマージン
    }

    // バリアーに関する設定
    public class ShieldConfig
    {
        // TODO 被弾時オブジェクトの追加など
        public GameObject shieldGameObject = null;
    }

    protected Player(string gameObjectName, string bulletPrefabName, float firePerFrame, float bulletMargin, int fireDamage)
    {
        this.moveConfig = new MoveConfig();
        this.fireConfig = new FireConfig();
        this.shieldConfig = new ShieldConfig();

        this.gameObject = GameObject.Find(gameObjectName);
        this.gameObjectName = gameObjectName;
        this.fireConfig.bulletGameObject = (GameObject)Resources.Load("Scenes/Game/Prefabs/" + bulletPrefabName);
        this.fireConfig.firePerFrame = firePerFrame;
        this.fireConfig.margin = bulletMargin;
        this.fireConfig.damage = fireDamage;

        // バリアーを子オブジェクトとして生成する TODO プレハブを自機分ロードするのは無駄だが・・・
        this.shieldConfig.shieldGameObject = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_shield"), this.gameObject.transform.position, Quaternion.identity);
        // 作成したオブジェクトを子として登録
        this.shieldConfig.shieldGameObject.transform.parent = this.gameObject.transform;
        this.defaultScale = gameObject.transform.localScale;
        // 予測線
        this.bulletLine = (GameObject)Object.Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/ShotLine"), this.gameObject.transform.position, Quaternion.identity);
        this.bulletLine.transform.parent = this.gameObject.transform;
        this.bulletLine.transform.position = new Vector3(this.bulletLine.transform.position.x, this.bulletLine.transform.position.y + this.bulletLine.GetComponent<SpriteRenderer>().bounds.size.y / 2, 0);
        this.bulletLine.SetActive(false);
    }

    // 移動座標の設定
    // 引数はスクリーン座標
    // 範囲外なら無視される
    public void SetMoveDestination(Vector3 destinationPosition)
    {
        if(this.IsDamageWait())
        {
            return;
        }

        // 規定範囲内への移動かどうか
        if (this.IsMoveDestinationInPermissionArea(destinationPosition))
        {
            this.moveConfig.destinationPositionWorldSpace = Camera.main.ScreenToWorldPoint(destinationPosition);
            this.moveConfig.isMove = true;
        }
    }

    public void Move()
    {
        if (this.IsDamageWait())
        {
            return;
        }

        if (!this.moveConfig.isMove)
        {
            return;
        }

        Vector3 nowPosition = this.gameObject.transform.position;
        Vector2 sub = Camera.main.ScreenToWorldPoint(nowPosition) - Camera.main.ScreenToWorldPoint(this.moveConfig.destinationPositionWorldSpace);
        float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg + 90f;
        float rad = angle * Mathf.Deg2Rad;
        nowPosition.x -= Mathf.Sin(rad) * this.moveConfig.speed;
        nowPosition.y += Mathf.Cos(rad) * this.moveConfig.speed;
        this.gameObject.transform.position = nowPosition;

        // 目的地との距離が規定値(endDistance)より近づいたら移動終了
        if (Vector2.Distance(nowPosition, this.moveConfig.destinationPositionWorldSpace) < this.moveConfig.endDistance)
        {
            this.moveConfig.isMove = false;
        }
    }

    // 移動許可されたエリアかどうか(固定で設定した座標内に移動したい場所があるかどうか)
    // 引数はスクリーン座標
    private bool IsMoveDestinationInPermissionArea(Vector3 targetPosition)
    {
        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
        if (targetPosition.x > this.moveConfig.limitMinWorldSpace.x && targetPosition.y > this.moveConfig.limitMinWorldSpace.y &&
            targetPosition.x < this.moveConfig.limitMaxWorldSpace.x && targetPosition.y < this.moveConfig.limitMaxWorldSpace.y)
        {
            // 移動先が自機とほぼ同じ位置ではないことを確認
            if (Vector2.Distance(targetPosition, this.gameObject.transform.position) > this.moveConfig.endDistance)
            {
                return true;
            }
        }

        return false;
    }

    public void EnableShield()
    {
        this.shieldConfig.shieldGameObject.SetActive(true);
        this.bulletLine.SetActive(false);
    }

    public void DisableShield(bool isInitial)
    {
        if (isInitial)
        {
            this.shieldConfig.shieldGameObject.SetActive(false);
        }
        else
        {
            // シールド解除時に拡大して～をやろうとしたが、当たり判定が残ってしまうので保留
            // 別のものを使う？
            /*
            Vector3 scale = this.shieldConfig.shieldGameObject.transform.localScale;
            this.shieldConfig.shieldGameObject.transform.localScale = new Vector3(scale.x * 2, scale.y * 2, scale.z);
            */
            this.shieldConfig.shieldGameObject.SetActive(false);
        }

        this.bulletLine.SetActive(true);
    }

    public virtual bool IsDamageWait()
    {
        if(this.damageWait > 0)
        {
            return true;
        }
        return false;
    }

    // 被弾時のちかちか設定
    public void ShowDamageEffect()
    {
        if (this.damageWait > 0)
        {
            this.bulletLine.SetActive(false);
            this.damageEffectDelay -= Time.deltaTime;
            if (this.damageEffectDelay <= 0)
            {
                if (this.gameObject.transform.localScale == this.defaultScale)
                {
                    this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    this.gameObject.transform.localScale = this.defaultScale;
                }
                this.damageEffectDelay = this.damageEffectDelayDefault;
            }
        }
    }

    public void HideDamageEffect()
    {
        this.gameObject.transform.localScale = this.defaultScale;
        this.damageEffectDelay = this.damageEffectDelayDefault;
        // シールドが消えていれば選択中なので、予測線を表示する
        if (!this.shieldConfig.shieldGameObject.activeSelf)
        {
            this.bulletLine.SetActive(true);
        }
    }
    // TODO 被攻撃関数

}

class PlayerRensha : Player
{
    public PlayerRensha() : base("Player_rensya", "Player_00_rensya_tama", 15f, 0.8f, 1)
    {
    }
}

class PlayerKakusan : Player
{
    public PlayerKakusan() : base("Player_kakusan", "Player_01_kakusan_missile", 1f, 0.3f, 5)
    {
    }
}

class PlayerSogeki : Player
{
    public PlayerSogeki() : base("Player_sogeki", "Player_02_sogeki_sht_bm", 0f, 5.35f, 500)
    {
    }
}
