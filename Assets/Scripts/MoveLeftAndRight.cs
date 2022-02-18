using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndRight : MonoBehaviour
{
    public float speed,distance;
    private float minX, maxX;

    public bool right, dontMove;
    private bool stop;
    void Start()
    {
        maxX = transform.position.x+distance;
        minX = transform.position.x - distance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
