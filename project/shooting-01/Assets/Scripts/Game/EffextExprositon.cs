using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffextExprositon : MonoBehaviour {
    [SerializeField]
    float lifeTime = 0.3f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.lifeTime -= Time.deltaTime;
        if (this.lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
