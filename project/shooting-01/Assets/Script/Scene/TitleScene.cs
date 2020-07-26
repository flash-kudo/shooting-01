using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        RankingManager.LoadRankScore();
    }


    public void OnStartButton()
    {
        SceneManager.LoadScene("game-main");
    }

    public void OnTutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnHighscoreButton()
    {
        SceneManager.LoadScene("RankingScene");
    }
}
