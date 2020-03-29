using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Scaler : MonoBehaviour
{
    public const float SCALE = 0.002f;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(SCALE, SCALE, 1f);
    }
}
