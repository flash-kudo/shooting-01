using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMain : MonoBehaviour {

    protected float angle = 0.0f;
    protected float speed = 0.0f;
    protected int damage = 0;
    public BulletTarget target = 0;
    private string bulletName;
    private SoundManager soundManager;

    // Use this for initialization
    void Start () {
        this.bulletName = this.gameObject.name;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        if (gameObject.name.StartsWith("Enemy_Bullet_02_laser"))
        {
            soundManager.PlayAudioClip(SoundManager.AudioClipType.EN_BEAM);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // 進行方向に向かって進む
        Vector3 nowPosition = this.transform.position;
        float rad;
        rad = angle * Mathf.Deg2Rad;
        nowPosition.x -= Mathf.Sin(rad) * speed;
        nowPosition.y += Mathf.Cos(rad) * speed;
        this.transform.position = nowPosition;

        // 画面端判定
        bool overScreen = false;
        if (this.transform.position.x < -3.5f) { overScreen = true; }
        if (this.transform.position.x > 3.5f) { overScreen = true; }
        if (this.transform.position.y < -5.5f) { overScreen = true; }
        if (this.transform.position.y > 5.5f) { overScreen = true; }
        if (overScreen) { Destroy(this.gameObject); }

    }

    public virtual void Fire(Vector3 position, float angle, float speed, int damage, BulletTarget target)
    {
        // 座標を設定
        this.transform.position = position;

        // 向きを設定
        this.angle = angle;
        Vector3 rot = this.transform.eulerAngles;
        rot.z = angle;
        this.transform.eulerAngles = rot;

        // 速度を設定
        this.speed = speed;

        // 敵味方どちらが対象かを設定
        this.target = target;

        this.damage = damage;
    }

    public int GetDamage()
    {
        return this.damage;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (target == BulletTarget.Player)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log(this.gameObject);
                PlayerMain playerMain = GameObject.Find("PlayerMain").GetComponent<PlayerMain>();
                {
                    if (other.gameObject.name == "Player_shield(Clone)")
                    {
                        playerMain.Damage(other.gameObject.transform.parent.name);
                    }
                    else
                    {
                        playerMain.Damage(other.gameObject.name);
                    }
                }
                if (gameObject.name.StartsWith("Enemy_Bullet_02_laser"))
                {
                    GameObject offsetEffectObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Enemy_Bullet_01_laser_hit"));
                    offsetEffectObject.gameObject.transform.position = other.ClosestPointOnBounds(this.gameObject.transform.position);
                }
                Destroy(this.gameObject);
            }

            if (other.gameObject.tag == "DamageObject")
            {
                BulletMain bulletMain = other.gameObject.GetComponent<BulletMain>();
                if (bulletMain.target == BulletTarget.Enemy)
                {
                    GameObject offsetEffectObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Offset"));
                    offsetEffectObject.gameObject.transform.position = other.ClosestPointOnBounds(this.gameObject.transform.position);
                    soundManager.PlayAudioClip(SoundManager.AudioClipType.OFFSET);
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                }
            }

            if (other.gameObject.tag == "Earth")
            {
                if (gameObject.name.StartsWith("Enemy_Bullet_02_laser"))
                {
                    GameObject offsetEffectObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Enemy_Bullet_01_laser_hit"));
                    offsetEffectObject.gameObject.transform.position = other.ClosestPointOnBounds(this.gameObject.transform.position);
                }
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                GameObject hitEffectObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Hit_spark"));
                hitEffectObject.gameObject.transform.position = other.ClosestPointOnBounds(this.gameObject.transform.position);
                Destroy(this.gameObject);
            }
        }
    }

    /*
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "DamageObject")
        {
            Debug.Log("弾衝突発生");
        }
    }
    */
}
