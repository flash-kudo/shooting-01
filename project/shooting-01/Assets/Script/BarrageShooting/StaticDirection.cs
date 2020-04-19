using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDirection : MonoBehaviour
{
    public float Rotation;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, -transform.parent.rotation.z + Rotation + 1);
        
    }
}
