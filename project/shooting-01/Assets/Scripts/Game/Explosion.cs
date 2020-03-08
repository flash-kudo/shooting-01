using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : BulletMain
{

    [SerializeField] private float lifeTime = 0.8f;

    // Use this for initialization
    void Start()
    {
        this.damage = 1;
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    protected override void OnTriggerEnter(Collider other)
    {
        // 何もしない
    }
}
