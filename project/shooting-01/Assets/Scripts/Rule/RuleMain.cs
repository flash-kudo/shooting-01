using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickGoToTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
