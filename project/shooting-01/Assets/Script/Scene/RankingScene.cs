using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingScene : MonoBehaviour
{
    public Text Rank0Text;
    public Text Rank1Text;
    public Text Rank2Text;
    public Text Rank3Text;
    public Text Rank4Text;
    public Text Rank5Text;
    public Text Rank6Text;
    public Text Rank7Text;
    public Text Rank8Text;
    public Text Rank9Text;

    public void OnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ResetScore()
    {
        RankingManager.ResetRankScore();
        UpdateRank();
    }

    void Start()
    {
        UpdateRank();
    }

    public void UpdateRank()
    {
        Rank0Text.text = RankingManager.GetRankScoreString(0);
        Rank1Text.text = RankingManager.GetRankScoreString(1);
        Rank2Text.text = RankingManager.GetRankScoreString(2);
        Rank3Text.text = RankingManager.GetRankScoreString(3);
        Rank4Text.text = RankingManager.GetRankScoreString(4);
        Rank5Text.text = RankingManager.GetRankScoreString(5);
        Rank6Text.text = RankingManager.GetRankScoreString(6);
        Rank7Text.text = RankingManager.GetRankScoreString(7);
        Rank8Text.text = RankingManager.GetRankScoreString(8);
        Rank9Text.text = RankingManager.GetRankScoreString(9);
    }
}
