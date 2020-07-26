using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{

    public GameObject Page1Screen;
    public GameObject Page2Screen;
    public GameObject Page3Screen;
    public GameObject Page4Screen;
    public GameObject Page5Screen;

    private int _Page;

    // Start is called before the first frame update
    void Start()
    {
        Page = 0;
    }

    public void OnPrev()
    {
        Page = (Page + 4) % 5;
    }
    public void OnNext()
    {
        Page = (Page + 1) % 5;
    }

    public void OnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private int Page
    {
        get
        {
            return _Page;
        }
        set
        {
            _Page = value;
            SetPage();
        }
    }

    private void SetPage()
    {
        Page1Screen.SetActive(_Page == 0);
        Page2Screen.SetActive(_Page == 1);
        Page3Screen.SetActive(_Page == 2);
        Page4Screen.SetActive(_Page == 3);
        Page5Screen.SetActive(_Page == 4);
    }

}
