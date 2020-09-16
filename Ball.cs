using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed, rand;
    public int direcx, direcy;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        source = GetComponent<AudioSource>();
        Launch();
    }
    private Vector2 velocityBeforePhysicsUpdate;
    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rb.velocity;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        source.pitch = Random.Range(1,5);
        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        //x
        if (velocityBeforePhysicsUpdate.x > 0 && collision.gameObject.name == "Player2")
        {
            rand = Random.Range(0.5f, 2.5f);
            direcx = 1;
        }
        else if (velocityBeforePhysicsUpdate.x < 0 && collision.gameObject.name == "Player2")
        {
            rand = Random.Range(0.5f, 2.5f);
            direcx = -1;
        }
        else if (velocityBeforePhysicsUpdate.x > 0 && collision.gameObject.name == "Player1")
        {
            rand = Random.Range(0.5f, 2.5f);
            direcx = 1;
        }
        else if (velocityBeforePhysicsUpdate.x < 0 && collision.gameObject.name == "Player1")
        {
            rand = Random.Range(0.5f, 2.5f);

            direcx = -1;
        }

        else if(velocityBeforePhysicsUpdate.x <0 && collision.gameObject.name == "SideWallL"){
            direcx = 1;
            rand = 1;
        }

        else if (velocityBeforePhysicsUpdate.x > 0 && collision.gameObject.name == "SideWallR")
        { direcx = -1; 
        rand = 1; }

        //y
        if (velocityBeforePhysicsUpdate.y > 0 && collision.gameObject.name == "Player2")
        {
            
            direcy = -1;
        }
        else if(velocityBeforePhysicsUpdate.y < 0 && collision.gameObject.name == "Player1") 
        { direcy = 1; 
        }
        else if(velocityBeforePhysicsUpdate.y < 0 && collision.gameObject.name == "SideWallL")
        { direcy = -1;
            rand = 1;
        }
        else if (velocityBeforePhysicsUpdate.y > 0 && collision.gameObject.name == "SideWallL")
        { direcy = 1;
            rand = 1;
        }
        else if (velocityBeforePhysicsUpdate.y < 0 && collision.gameObject.name == "SideWallR")
        { direcy = -1;
            rand = 1;
        }
        else if (velocityBeforePhysicsUpdate.y > 0 && collision.gameObject.name == "SideWallR")
        { direcy = 1;
            rand = 1;
        }

        Debug.Log("Hit");
        speed+= 0.25f;
        
        
        rb.velocity = new Vector2(rand* speed * direcx, speed * direcy);
        //Debug.Log(transform.position.x);
        source.Play();
    }
    public void Reset()
    {

        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        speed = 5;
        Launch();
    }
    // Update is called once per frame

   

    void Update()
    {
        
    }

    private void Launch(){

        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }


}
