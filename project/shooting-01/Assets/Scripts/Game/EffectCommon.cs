using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCommon : MonoBehaviour {

    public float lifeTime = 0.3f;
    public bool rotateRandom = false;
    public float randomRotateMin = -20;
    public float randomRotateMax = 20;

    // Use this for initialization
    void Start () {
        if (rotateRandom)
        {
            float rotateZ = this.gameObject.transform.rotation.z + Random.Range(randomRotateMin, randomRotateMax);
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        }
    }
	
	// Update is called once per frame
	void Update () {
        this.lifeTime -= Time.deltaTime;
        if (this.lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
