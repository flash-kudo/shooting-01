using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MinPlayerLevel = 1;
    public int MaxPlayerLevel = 100;

    public int CurrentPlayerLevel = 1;

    /// *******************************************************
    /// <summary>プレイヤーレベル割合</summary>
    /// *******************************************************
    public float PlayerLevelRate()
    {
        return Mathf.Clamp01((float)(CurrentPlayerLevel - MinPlayerLevel) / (float)(MaxPlayerLevel - MinPlayerLevel));
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
