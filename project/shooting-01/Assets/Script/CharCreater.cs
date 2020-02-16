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
        if(Count > 80)
        {
            if(Random.value < 0.5f) CreateChar(Enemy, null);
            else CreateChar(Enemy, "right");
            Count = 0;
        }
    }

    private void CreateChar(GameObject obj, string keyword)
    {
        GameObject prefab = (GameObject)Resources.Load("TestEnemy");
        // GameObject self_go = Instantiate(obj);
        GameObject self_go = Instantiate(prefab);
        EnemyControll self_ctrl = self_go.GetComponent<EnemyControll>();
        self_ctrl.SpawnKey = keyword;
        self_go.SetActive(true);
    }
}
