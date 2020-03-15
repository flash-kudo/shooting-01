using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorGraphics : MonoBehaviour
{

    public GameObject AnotherEdge;
    [HideInInspector]
    public LineRenderer Line;

    // Start is called before the first frame update
    void Start()
    {
        if (AnotherEdge == null) return;

        Line = GetComponent<LineRenderer>();

        Line.startWidth = 0.02f;
        Line.endWidth = 0.02f;
        Line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (AnotherEdge == null) return;

        Line.SetPosition(0, this.transform.position);
        Line.SetPosition(1, AnotherEdge.transform.position);
    }
}
