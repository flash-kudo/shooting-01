using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

public class CharCreater : MonoBehaviour
{
    public GameObject Self;
    public GameObject Enemy;

    private int Counter = 0;
    private int CountLimit = 5;

    // Start is called before the first frame update
    void Start()
    {
        Self.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Counter++;
        if(Counter > CountLimit)
        {
            CreateChar(Self, new Vector3(Random.Range(-3.0f, 3.0f), -5, 0));
            CreateChar(Enemy, new Vector3(-3.0f, Random.Range(-5.0f, 5.0f), 0));
            Counter = 0;
        }
    }

    private void CreateChar(GameObject obj, Vector3 pos)
    {
        GameObject self_go = Instantiate(obj, pos, Quaternion.identity);
        CharacterControll self_ctrl = self_go.GetComponent<CharacterControll>();
        self_ctrl.Position = pos;
        self_go.SetActive(true);
    }
}
