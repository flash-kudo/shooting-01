using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMain : MonoBehaviour
{
    public float reduceEarthHpWhenHitEnemy = 20f;
    public float reduceRankWhenHitEnemy = 10000f;
    public float reduceEarthHpWhenHitBullet = 10f;
    public float reduceRankWhenHitBullet = 5000f;
    GameStatus gameStatus = null;
    private int flashWait = 0;
    private SoundManager soundManager;

    // Use this for initialization
    void Start()
    {
        gameStatus = GameStatus.GetInstance();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.localEulerAngles.z + 0.05f);//new Vector3(0, 0, this.gameObject.transform.localEulerAngles.z);

        if (this.flashWait > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            this.flashWait--;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //地球ゲージを 10 減らす
            gameStatus.EarthHp -= reduceEarthHpWhenHitEnemy;
            gameStatus.Rank -= reduceRankWhenHitEnemy;
            this.flashWait = 1;
            SoundManager.AudioClipType et_damage;
            if(Random.Range(0, 2) == 0)
            {
                et_damage = SoundManager.AudioClipType.ET_DAMAGE1;
            }
            else
            {
                et_damage = SoundManager.AudioClipType.ET_DAMAGE2;
            }
            soundManager.PlayAudioClip(et_damage);
        }
        if (other.gameObject.tag == "DamageObject")
        {
            BulletMain bulletMain = other.gameObject.GetComponent<BulletMain>();
            if (bulletMain.target == BulletTarget.Player)
            {
                //地球ゲージを 10 減らす
                gameStatus.EarthHp -= reduceEarthHpWhenHitBullet;
                gameStatus.Rank -= reduceRankWhenHitBullet;
                this.flashWait = 1;
                SoundManager.AudioClipType et_damage;
                if (Random.Range(0, 2) == 0)
                {
                    et_damage = SoundManager.AudioClipType.ET_DAMAGE1;
                }
                else
                {
                    et_damage = SoundManager.AudioClipType.ET_DAMAGE2;
                }
                soundManager.PlayAudioClip(et_damage);
            }
        }
    }
}
