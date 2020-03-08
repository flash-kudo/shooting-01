using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingLaser : MonoBehaviour {
    float lifeTime = 0.5f;
    float scale = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.scale += 2.0f;
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, scale, this.gameObject.transform.localScale.z);
        this.lifeTime -= Time.deltaTime;
        if (this.lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
