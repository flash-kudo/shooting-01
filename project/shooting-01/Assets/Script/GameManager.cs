using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int MinPlayerLevel = 1;

    public int CurrentPlayerLevel = 1;

    public float CurrentScore = 0;

    public Func<Vector2, float, bool> FortressHitCheck;

    /// *******************************************************
    /// <summary>プレイヤーレベル割合</summary>
    /// *******************************************************
    public float PlayerCalcLevel()
    {
        return Mathf.Max(CurrentPlayerLevel - MinPlayerLevel, 0);
    }

    /// *******************************************************
    /// <summary>要塞ヒットチェック</summary>
    /// *******************************************************
    public bool IsHitFortress(Vector2 position, float scale)
    {
        if (FortressHitCheck == null) return false;
        return FortressHitCheck(position, scale);
    }

    /// *******************************************************
    /// <summary>スコア追加</summary>
    /// *******************************************************
    public void AddScore(float base_score, float add_score, float power_score)
    {
        float add = base_score * power_score + add_score;
        CurrentScore = CurrentScore + Mathf.Floor(add);
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
        return RankingManager.GetScoreScring(CurrentScore);
    }

    /// *******************************************************
    /// <summary>ランキングスコア文字列</summary>
    /// *******************************************************
    public string RankScoreString()
    {
        float score = Mathf.Max(CurrentScore, RankingManager.GetRankScore(0));
        return RankingManager.GetScoreScring(score);
    }

    /// *******************************************************
    /// <summary>ゲーム終了時処理</summary>
    /// *******************************************************
    public void OnEndGame()
    {
        GameResult.Score = CurrentScore;
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
