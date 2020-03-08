using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickGoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnClickGoToRanking()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Ranking");
        // 開発用
        UnityEngine.SceneManagement.SceneManager.LoadScene("Config");
    }

    public void OnClickGoToRule()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Rule");
    }
}
