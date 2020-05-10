using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MinPlayerLevel = 1;

    public int CurrentPlayerLevel = 1;

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
