using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Paddle : MonoBehaviour
{

    SerialPort stream = new SerialPort("COM5", 9600);
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

public Rigidbody2D ball;
private float d;

private int Counter = 0;
 

    private float movement;
    
    void Start(){
        if(isPlayer1){
        stream.Open();
        }
        startPosition = transform.position;
    }
    void Update()
    {

        //Counter = arduino input
        //potential fix if(stream){do paddle 1 stuff}
        //else{do paddle 2 stuff}

        if(isPlayer1){
        string value = stream.ReadLine();
        Counter = int.Parse(value);
        Debug.Log(Counter);

                if (Counter == 0) {transform.position = new Vector3(-5,-9,0);}
                else if (Counter == 1) { transform.position = new Vector3(-3, -9, 0);}
                else if (Counter == 2) { transform.position = new Vector3(-1, -9, 0);}
                else if (Counter == 3) { transform.position = new Vector3(1, -9, 0);}
                else if (Counter == 4) { transform.position = new Vector3(3, -9, 0);}
                else if (Counter == 5) { transform.position = new Vector3(5, -9, 0);}
                
                else if (Counter == 69) { transform.position = new Vector3(69, -69, 0); }
        }
            
        else{
            d = ball.position.x - transform.position.x;
            if (d > 0)
            {
                movement = speed/4f * Mathf.Min(d, 20);
            }
            if (d < 0)
            {
                movement = -speed/4f * Mathf.Min(-d, 20);
            }
        
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
    }

    public void Reset(){

        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
