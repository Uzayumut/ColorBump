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
    public bool canMove,gameOver,finish;

    //public GameObject breakableSphere;
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
                //Time.timeScale = 1f;
            }
        }else if (!canMove &&!finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<GameManager>().RemoveUI();
                canMove = true;
            }
                
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
        //GameObject shatterSphere = Instantiate(breakableSphere, transform.position, Quaternion.identity);
        //foreach (Transform o in shatterSphere.transform)
        //{
        //    o.GetComponent<Rigidbody>().AddForce(Vector3.forward *5, ForceMode.Impulse);
        //}
        //shatterSphere.transform.position = Vector3.zero;
        canMove = false;
        gameOver = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //Time.timeScale = .3f;
    }

    IEnumerator NextLevel()
    {
        finish = true;
        canMove = false;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
    }
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            print("Hit enemy");
            GameOver();
        }



    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.name == "Finish")
        {
            StartCoroutine(NextLevel());
        }
    }

}
