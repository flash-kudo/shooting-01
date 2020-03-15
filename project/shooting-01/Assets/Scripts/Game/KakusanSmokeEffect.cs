using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakusanSmokeEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 nowPosition = this.transform.position;
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y * 1.01f, this.gameObject.transform.localScale.z);
        this.transform.position = nowPosition;
        // TODO 初期位置と差分を求めて、Y 位置をずらす
    }
}
