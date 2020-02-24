using System.Collections;
using System.Collections.Generic;
using BarrageShooting;
using UnityEngine;

public class CharCreater : MonoBehaviour
{
    private int Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Count = 0;
    }

#if false
    // Update is called once per frame
    void Update()
    {
        Count++;
        if(Count > 80)
        {
            int rnd = Mathf.FloorToInt(Random.value * 6);
            switch (rnd)
            {
                case 0: CreateChar("left1"); break;
                case 1: CreateChar("left2"); break;
                case 2: CreateChar("left3"); break;
                case 3: CreateChar("right1"); break;
                case 4: CreateChar("right2"); break;
                case 5: CreateChar("right3"); break;
                case 6: CreateChar("right3"); break;
            }
            Count = 0;
        }
    }

    private void CreateChar(string keyword)
    {
        GameObject self_go = StageManager.Instance.InstantiateObject("TestEnemy2");
        EnemyControll self_ctrl = self_go.GetComponent<EnemyControll>();
        self_ctrl.SpawnKey = keyword;
        self_go.SetActive(true);
    }

#endif

}
