using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 6;
    public Vector3 camVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * cameraSpeed * Time.deltaTime;
        camVelocity= Vector3.forward * cameraSpeed * Time.deltaTime;
    }
}
