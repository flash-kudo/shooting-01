using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKakusan : BulletMain {
    // 現在の詳細ダメージ値
    private float damageDetail = 0;
    private Vector3 startPosition;
    private float startHeight;

    // Use this for initialization
    void Start () {
        this.damageDetail = (float)this.damage;
        startPosition = this.transform.position;
        GameObject smokeObject = this.gameObject.transform.Find("Player_01_kakusan_smoke").gameObject;
        startHeight = smokeObject.GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    private void Update()
    {
        // 進行方向に向かって進む
        Vector3 nowPosition = this.transform.position;
        float rad = angle * Mathf.Deg2Rad;
        nowPosition.x -= Mathf.Sin(rad) * speed;
        nowPosition.y += Mathf.Cos(rad) * speed;
        this.transform.position = nowPosition;

        GameObject smokeObject = this.gameObject.transform.Find("Player_01_kakusan_smoke").gameObject;
        float newHeight = Vector3.Distance(nowPosition, startPosition) / startHeight;
        smokeObject.transform.position = Vector3.Lerp(nowPosition, startPosition, 0.5f);
        smokeObject.transform.localScale = new Vector3(1, newHeight, 1);

        // 徐々にスピードと威力を下げる
        if ( this.speed > 0.06f)
        {
            this.speed *= 0.99f;
            this.damageDetail *= 0.96f;
            this.damage = Mathf.CeilToInt(this.damageDetail);
        }
        else
        {
            DestroyWithExplosion();
        }

        // 画面端判定
        bool overScreen = false;
        if (this.transform.position.x < -3.5f) { overScreen = true; }
        if (this.transform.position.x > 3.5f) { overScreen = true; }
        if (this.transform.position.y < -5.5f) { overScreen = true; }
        if (this.transform.position.y > 5.5f) { overScreen = true; }
        if (overScreen) { Destroy(this.gameObject); }
    }

    private void DestroyWithExplosion()
    {
        GameObject explosionObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Player_01_kakusan_missile_explosion"));
        explosionObject.gameObject.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject hitEffectObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Hit_spark"));
            hitEffectObject.gameObject.transform.position = other.ClosestPointOnBounds(this.gameObject.transform.position);
            DestroyWithExplosion();
        }
    }
}
