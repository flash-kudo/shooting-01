using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSogeki : BulletMain
{
    [SerializeField] private float lifeTime = 0.2f;
    [SerializeField] private float margin = 0.06f;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        this.lifeTime -= Time.deltaTime;
        if(this.lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void Fire(Vector3 position, float angle, float speed, int damage, BulletTarget target)
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

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject hitEffectObject = Instantiate((GameObject)Resources.Load("Scenes/Game/Prefabs/Hit_spark"));
            hitEffectObject.gameObject.transform.position = other.ClosestPointOnBounds(this.gameObject.transform.position);
            // Destroy(this.gameObject) はしない
        }
    }
}
