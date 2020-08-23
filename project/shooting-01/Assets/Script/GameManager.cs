using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int EXP2RANK_COUNT = 100;

    public int MinPlayerLevel = 1;

    public int CurrentExpPoints = 0;

    public float CurrentScore = 0;

    public Func<Vector2, float, bool> FortressHitCheck;

    /// *******************************************************
    /// <summary>要塞ヒットチェック</summary>
    /// *******************************************************
    public bool IsHitFortress(Vector2 position, float scale)
    {
        if (FortressHitCheck == null) return false;
        return FortressHitCheck(position, scale);
    }

    public void AddExp(int addexp)
    {
        CurrentExpPoints = CurrentExpPoints + addexp;
        if (CurrentExpPoints < 0) CurrentExpPoints = 0;
    }

    public int LevelNumber()
    {
        return (CurrentExpPoints / EXP2RANK_COUNT) + MinPlayerLevel;
    }

    public string LevelString()
    {
        return RankingManager.GetLevelString(LevelNumber());
    }

    public int ExpNumber()
    {
        return (CurrentExpPoints % EXP2RANK_COUNT);
    }

    public float ExpRate()
    {
        return ((float)ExpNumber() / (float)EXP2RANK_COUNT) * 100.0f;
    }

    /// *******************************************************
    /// <summary>スコア追加</summary>
    /// *******************************************************
    public void AddScore(float base_score, float add_score, float power_score)
    {
        float add = base_score * power_score + add_score;
        CurrentScore = CurrentScore + Mathf.Floor(add) * LevelNumber();
    }

    /// *******************************************************
    /// <summary>スコア減少</summary>
    /// *******************************************************
    public void SubScore(float base_score)
    {
        CurrentScore = Mathf.Max(CurrentScore - Mathf.Floor(base_score));
    }

    /// *******************************************************
    /// <summary>スコア文字列</summary>
    /// *******************************************************
    public string ScoreString()
    {
        return RankingManager.GetScoreString(CurrentScore);
    }

    /// *******************************************************
    /// <summary>ランキングスコア文字列</summary>
    /// *******************************************************
    public string RankScoreString()
    {
        float score = Mathf.Max(CurrentScore, RankingManager.GetRankScore(0));
        return RankingManager.GetScoreString(score);
    }

    /// *******************************************************
    /// <summary>ゲーム終了時処理</summary>
    /// *******************************************************
    public void OnEndGame()
    {
        GameResult.Score = CurrentScore;
        GameResult.Level = LevelNumber();
        SceneManager.LoadScene("ResultScene");
    }


    // ########################################################
    // ########################################################

    public static GameManager _Instance;

    /// *******************************************************
    /// <summary>シングルトンインスタンス</summary>
    /// *******************************************************
    public static GameManager Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = FindObjectOfType<GameManager>();
            }
            return _Instance;
        }
    }

    /// *******************************************************
    /// <summary>早期初期処理</summary>
    /// *******************************************************
    void Awake()
    {
        if((_Instance != null) && (_Instance != this))
        {
            Destroy(this);
        }
        else
        {
            _Instance = this;
        }
    }



}
