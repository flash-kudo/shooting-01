using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticScreenManager : MonoBehaviour
{
    public Text WaveMessage1;
    public Text WaveMessage2;

    // Start is called before the first frame update
    void Start()
    {
        HideMessage();
    }

    public void ShowMessage(string msg)
    {
        if(msg == null)
        {
            HideMessage();
            return;
        }
        string[] msg_list = msg.Split(';');

        WaveMessage1.text = (msg_list.Length <= 0) ? "" : msg_list[0];
        WaveMessage2.text = (msg_list.Length <= 1) ? "" : msg_list[1];
    }
    public void HideMessage()
    {
        WaveMessage1.text = "";
        WaveMessage2.text = "";
    }

}
