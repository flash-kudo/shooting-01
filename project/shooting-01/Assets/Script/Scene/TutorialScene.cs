using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeScene", 5f);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("game-main");
    }
}
