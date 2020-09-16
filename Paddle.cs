using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Paddle : MonoBehaviour
{

    SerialPort stream = new SerialPort("COM5", 9600);
    public bool isPlayer1, isPlayer2, bar3, bar4, bar5, bar6;
    public float speed;
    public float AI = 2f;
    public Rigidbody2D rb;
    public Vector3 startPosition;

public Rigidbody2D ball;
private float d;

private int Counter = 0;
public SpriteRenderer sprite3, sprite4, sprite5, sprite6;
 
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
           if(Counter < 10){
                sprite6.color = new Color(1, 1, 1, 1);
                sprite5.color = new Color(1, 1, 1, 1);
                sprite4.color = new Color(1, 1, 1, 1);
                sprite3.color = new Color(1, 1, 1, 1);
                
                if (Counter == 0) {transform.position = new Vector3(-10,-9,0);}
                else if (Counter == 1) { transform.position = new Vector3(-6, -9, 0);}
                else if (Counter == 2) { transform.position = new Vector3(-2, -9, 0);}
                else if (Counter == 3) { transform.position = new Vector3(2, -9, 0);}
                else if (Counter == 4) { transform.position = new Vector3(6, -9, 0);}
                else if (Counter == 5) { transform.position = new Vector3(10, -9, 0);}
           }
                else if (Counter == 13) 
                { 
                transform.position = new Vector3(69, 69, 0);
                sprite3.color = new Color(1,0,0,1);
                }
            else if (Counter == 14)
            {
                transform.position = new Vector3(69, 69, 0);
                sprite4.color = new Color(1, 0, 0, 1);
            }
            else if (Counter == 15)
            {
                transform.position = new Vector3(69, 69, 0);
                sprite5.color = new Color(1, 0, 0, 1);
            }
            else if (Counter == 16)
            {
                transform.position = new Vector3(69, 69, 0);
                sprite6.color = new Color(1, 0, 0, 1);
            }
        }
            
        else if(isPlayer2){
            d = ball.position.x - transform.position.x;
            if (d > 0)
            {
                movement = speed/AI * Mathf.Min(d, 20);
            }
            if (d < 0)
            {
                movement = -speed/AI * Mathf.Min(-d, 20);
            }
        
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        
    }

    public void Reset(){
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
