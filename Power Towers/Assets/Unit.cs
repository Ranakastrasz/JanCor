using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    protected Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    public void Start()
    {
    }


    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        // Nothing Special for units unless clicked
    }

    void OnMouseDown()
    {
        if (Input.GetKey("mouse 0"))
        {
            print("Box Clicked!");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        //if (other.gameObject.CompareTag("PickUp"))
        //{
        //this.GetComponent<Collider2D>().gameObject.SetActive(false);
        //other.gameObject.SetActive(false);
        //}
    }

}