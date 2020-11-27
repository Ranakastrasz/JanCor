using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep_Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        //if (other.gameObject.CompareTag("PickUp"))
        //{
        //this.GetComponent<Collider2D>().gameObject.SetActive(false);
        Destroy(other.gameObject);
        //}
    }
}
