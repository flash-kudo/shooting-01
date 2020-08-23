using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    public Text ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = RankingManager.GetScoreString(GameResult.Score);
        string rank = GameResult.Level.ToString();
        RankingManager.SetNewScore(GameResult.Score);
    }

    public void OnRanking()
    {
        SceneManager.LoadScene("RankingScene");
    }

    public void OnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
