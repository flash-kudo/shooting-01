using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int score = GameStatus.GetInstance().Score;
        GameObject.Find("Text_Panel").transform.Find("Score").GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void OnClickGoToTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void OnClickGoToRanking()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ranking");
    }

}
