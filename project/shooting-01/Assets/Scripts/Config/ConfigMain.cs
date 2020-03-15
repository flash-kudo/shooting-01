using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMain : MonoBehaviour {
    InputField InputInnsekiLife;
    InputField InputInnsekiSpeed;
    InputField InputTotugekiLife;
    InputField InputTotugekiSpeed;
    InputField InputShagekiLife;
    InputField InputShagekiSpeed;
    InputField InputYousaiLife;
    InputField InputYousaiSpeed;
    InputField InputAllRankRate;

    // Use this for initialization
    void Start () {
        InputInnsekiLife = GameObject.Find("InputInnsekiLife").GetComponent<InputField>();
        InputInnsekiSpeed = GameObject.Find("InputInnsekiSpeed").GetComponent<InputField>();
        InputTotugekiLife = GameObject.Find("InputTotugekiLife").GetComponent<InputField>();
        InputTotugekiSpeed = GameObject.Find("InputTotugekiSpeed").GetComponent<InputField>();
        InputShagekiLife = GameObject.Find("InputShagekiLife").GetComponent<InputField>();
        InputShagekiSpeed = GameObject.Find("InputShagekiSpeed").GetComponent<InputField>();
        InputYousaiLife = GameObject.Find("InputYousaiLife").GetComponent<InputField>();
        InputYousaiSpeed = GameObject.Find("InputYousaiSpeed").GetComponent<InputField>();
        InputAllRankRate = GameObject.Find("InputAllRankRate").GetComponent<InputField>();

        InputInnsekiLife.text = GameStatus.GetInstance().enemyInnsekiPram.life.ToString();
        InputInnsekiSpeed.text = GameStatus.GetInstance().enemyInnsekiPram.speed.ToString();
        InputTotugekiLife.text = GameStatus.GetInstance().enemyTotugekiPram.life.ToString();
        InputTotugekiSpeed.text = GameStatus.GetInstance().enemyTotugekiPram.speed.ToString();
        InputShagekiLife.text = GameStatus.GetInstance().enemySyagekiPram.life.ToString();
        InputShagekiSpeed.text = GameStatus.GetInstance().enemySyagekiPram.speed.ToString();
        InputYousaiLife.text = GameStatus.GetInstance().enemyYousaiPram.life.ToString();
        InputYousaiSpeed.text = GameStatus.GetInstance().enemyYousaiPram.speed.ToString();
        InputAllRankRate.text = GameStatus.GetInstance().EnemyRankRate.ToString();

        if (PlayerPrefs.HasKey("InnsekiLife"))
        {
            InputInnsekiLife.text = PlayerPrefs.GetString("InnsekiLife", "");
        }
        if (PlayerPrefs.HasKey("InnsekiSpeed"))
        {
            InputInnsekiSpeed.text = PlayerPrefs.GetString("InnsekiSpeed", "");
        }
        if (PlayerPrefs.HasKey("TotugekiLife"))
        {
            InputTotugekiLife.text = PlayerPrefs.GetString("TotugekiLife", "");
        }
        if (PlayerPrefs.HasKey("TotugekiSpeed"))
        {
            InputTotugekiSpeed.text = PlayerPrefs.GetString("TotugekiSpeed", "");
        }
        if (PlayerPrefs.HasKey("ShagekiLife"))
        {
            InputShagekiLife.text = PlayerPrefs.GetString("ShagekiLife", "");
        }
        if (PlayerPrefs.HasKey("ShagekiSpeed"))
        {
            InputShagekiSpeed.text = PlayerPrefs.GetString("ShagekiSpeed", "");
        }
        if (PlayerPrefs.HasKey("YousaiLife"))
        {
            InputYousaiLife.text = PlayerPrefs.GetString("YousaiLife", "");
        }
        if (PlayerPrefs.HasKey("YousaiSpeed"))
        {
            InputYousaiSpeed.text = PlayerPrefs.GetString("YousaiSpeed", "");
        }
        if (PlayerPrefs.HasKey("AllRankRate"))
        {
            InputAllRankRate.text = PlayerPrefs.GetString("AllRankRate", "");
        }

    }
	
	// Update is called once per frame
	void Update () {		
	}

    public void OnClickGoToTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void OnClickDecideConfig()
    {
        PlayerPrefs.SetString("InnsekiLife", InputInnsekiLife.text);
        PlayerPrefs.SetString("InnsekiSpeed", InputInnsekiSpeed.text);
        PlayerPrefs.SetString("TotugekiLife", InputTotugekiLife.text);
        PlayerPrefs.SetString("TotugekiSpeed", InputTotugekiSpeed.text);
        PlayerPrefs.SetString("ShagekiLife", InputShagekiLife.text);
        PlayerPrefs.SetString("ShagekiSpeed", InputShagekiSpeed.text);
        PlayerPrefs.SetString("YousaiLife", InputYousaiLife.text);
        PlayerPrefs.SetString("YousaiSpeed", InputYousaiSpeed.text);
        PlayerPrefs.SetString("AllRankRate", InputAllRankRate.text);

        int tmpInt;
        float tmpFloat;
        if (int.TryParse(InputInnsekiLife.text, out tmpInt))
        {
            PlayerPrefs.SetString("InnsekiLife", InputInnsekiLife.text);
            GameStatus.GetInstance().enemyInnsekiPram.life = tmpInt;
        }
        if (float.TryParse(InputInnsekiSpeed.text, out tmpFloat))
        {
            PlayerPrefs.SetString("InnsekiSpeed", InputInnsekiSpeed.text);
            GameStatus.GetInstance().enemyInnsekiPram.speed = tmpFloat;
        }
        if (int.TryParse(InputTotugekiLife.text, out tmpInt))
        {
            PlayerPrefs.SetString("TotugekiLife", InputTotugekiLife.text);
            GameStatus.GetInstance().enemyTotugekiPram.life = tmpInt;
        }
        if (float.TryParse(InputTotugekiSpeed.text, out tmpFloat))
        {
            PlayerPrefs.SetString("TotugekiSpeed", InputTotugekiSpeed.text);
            GameStatus.GetInstance().enemyTotugekiPram.speed = tmpFloat;
        }
        if (int.TryParse(InputShagekiLife.text, out tmpInt))
        {
            PlayerPrefs.SetString("ShagekiLife", InputShagekiLife.text);
            GameStatus.GetInstance().enemySyagekiPram.life = tmpInt;
        }
        if (float.TryParse(InputShagekiSpeed.text, out tmpFloat))
        {
            PlayerPrefs.SetString("ShagekiSpeed", InputShagekiSpeed.text);
            GameStatus.GetInstance().enemySyagekiPram.speed = tmpFloat;
        }
        if (int.TryParse(InputYousaiLife.text, out tmpInt))
        {
            PlayerPrefs.SetString("YousaiLife", InputYousaiLife.text);
            GameStatus.GetInstance().enemyYousaiPram.life = tmpInt;
        }
        if (float.TryParse(InputYousaiSpeed.text, out tmpFloat))
        {
            PlayerPrefs.SetString("YousaiSpeed", InputYousaiSpeed.text);
            GameStatus.GetInstance().enemyYousaiPram.speed = tmpFloat;
        }
        if (float.TryParse(InputAllRankRate.text, out tmpFloat))
        {
            PlayerPrefs.SetString("AllRankRate", InputAllRankRate.text);
            GameStatus.GetInstance().EnemyRankRate = tmpFloat;
        }

    }
}