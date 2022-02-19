using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastMousePos;
    public float sensitivity = .16f, clampDelta = 42f;

    public float bounds = 5;

    [HideInInspector]
    public bool canMove,gameOver;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y,transform.position.z);//Clamps the given value between the given minimum float and maximum float values
        if(canMove)
            transform.position += FindObjectOfType<CameraMovement>().camVelocity;

        
        if (!canMove && gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }else if (!canMove )
        {
            if (Input.GetMouseButtonDown(0))
                canMove = true;
        }

    }


    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
            

        }
        if (canMove)
        {
            if (Input.GetMouseButton(0))
            {

                Vector3 vector = lastMousePos - Input.mousePosition;
                lastMousePos = Input.mousePosition;
                vector = new Vector3(vector.x, 0, vector.y);

                Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);

                rb.AddForce(-moveForce * sensitivity - rb.velocity / 5, ForceMode.VelocityChange);
                //A forward direction(Vector3.forward*2)
                //independent of the weight of the mass (ForceMod.VelocityChange)
                //mouse not moving backwards(-moveforce)
                //added rigidbody change rate and precision(*sensitivity-rb.velocity/5)

            }
        }


       

        rb.velocity.Normalize();

    }
    private void GameOver()
    {
        canMove = false;
        gameOver = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

    }
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Enemy") ;
        GameOver();
    }

}
