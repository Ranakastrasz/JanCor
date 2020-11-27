using UnityEngine;
using System.Collections;

public class Creep : Unit
{

    public float speed;  //Floating point variable to store the player's movement speed.
    private Vector2 targetPosition; // Where is it going
    
    // Currently, figure out how to setup movement queue thing.
    

    new private void Start()
    {
        //targetPosition = ; 
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        Vector2 postion = rb2d.position;
        Vector2 movement = targetPosition- postion;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
    void OnMouseDown()
    {
        if (Input.GetKey("mouse 0"))
        {
            print("Creep Clicked!");
        }
    }
}