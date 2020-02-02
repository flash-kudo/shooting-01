using System.Collections;
using System.Collections.Generic;
using BarrageShooting;
using UnityEngine;

public class CharCreater : MonoBehaviour
{
    public GameObject Enemy;

    private int Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Count++;
        if(Count > 60)
        {
            if(Random.value < 0.5f) CreateChar(Enemy, new Vector3(-2.0f, 5.0f, 0));
            else CreateChar(Enemy, new Vector3(2.0f, 5.0f, 0));
            Count = 0;
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
