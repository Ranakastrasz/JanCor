using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep_Spawner : MonoBehaviour {
    // Spawner creates creeps on demand, and issues orders to them.
    // Also sets their stats.


    public GameObject CreepPrefab;

    private void spawnCreep()
    {
        GameObject myCreep = (GameObject)
            Instantiate(CreepPrefab, transform.position, Quaternion.identity);

        //creepClone

        // You can also acccess other components / scripts of the clone
        //rocketClone.GetComponent<MyRocketScript>().DoSomething();
    }

    // Use this for initialization

    void Start ()
    {
        InvokeRepeating("spawnCreep", 0.0f, 3.0f);
        
        

    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
