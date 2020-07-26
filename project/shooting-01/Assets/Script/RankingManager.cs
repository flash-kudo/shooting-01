using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager
{
    private const int RANK_LENGTH = 10;

    private static readonly float[] DefaultRankScore = new float[]{
        0,
        0,
        0,
        0,
        0,

        0,
        0,
        0,
        0,
        0,
    };

    private static List<float> RankScore;

    public static void LoadRankScore()
    {
        RankScore = new List<float>();
        for(int i = 0; i < RANK_LENGTH; i++)
        {
            RankScore.Add(PlayerPrefs.GetFloat("rank" + i.ToString(), DefaultRankScore[i]));
        }
        RankScore.Add(0); // 追加スコア用buffer
    }

    public static void SaveRankScore()
    {
        if ((RankScore == null) || (RankScore.Count < RANK_LENGTH)) LoadRankScore();
        for (int i = 0; i < RANK_LENGTH; i++)
        {
            PlayerPrefs.SetFloat("rank" + i.ToString(), RankScore[i]);
        }
        //PlayerPrefs.Save();
    }
    public static void ResetRankScore()
    {
        RankScore = new List<float>();
        for (int i = 0; i < RANK_LENGTH; i++)
        {
            RankScore.Add(DefaultRankScore[i]);
        }
        RankScore.Add(0); // 追加スコア用buffer
        SaveRankScore();
    }

    public static string GetScoreScring(float score)
    {
        string score_num = Mathf.Floor(score).ToString();
        string add_string = "00000000";
        string str = add_string + score_num;
        int lng = Mathf.Max(score_num.Length, add_string.Length);
        return str.Substring(str.Length - lng);
    }

    public static float GetRankScore(int rank)
    {
        if ((RankScore == null) || (RankScore.Count < RANK_LENGTH)) LoadRankScore();
        return Mathf.Floor(RankScore[rank]);
    }

    public static string GetRankScoreString(int rank)
    {
        return GetScoreScring(GetRankScore(rank));
    }

    public static int SetNewScore(float score)
    {
        if ((RankScore == null) || (RankScore.Count < RANK_LENGTH))
        {
            LoadRankScore();
        }

        RankScore[RANK_LENGTH] = score;
        int rank = RANK_LENGTH;

        for (int i = RANK_LENGTH; i > 0; i--)
        {
            if(RankScore[i] > RankScore[i - 1])
            {
                float buff = RankScore[i - 1];
                RankScore[i - 1] = RankScore[i];
                RankScore[i] = buff;
            }
            else
            {
                rank = i;
                break;
            }
        }
        SaveRankScore();
        return rank;
    }
}
