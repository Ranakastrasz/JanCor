using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Moves, impacts, informs the ship it impacted.
    // Carries the equation which the ship must counter.
    // 

    public Ship owner;
    float speed = 3f;
    public Equation equation;

    // Use this for initialization
    void Start()
    {
    }

    public void Init(int currentLevel)
    {
        equation = gameObject.AddComponent<Equation>();



        equation.SetupEquation(currentLevel);

        GameManager.Active.PrintEquation (equation.GetEquation());
        Console.Active.ResetValue();

        speed = GameManager.Active.shipDistance * TimeToImpact(equation.GetDifficulty(), currentLevel);

       // Debug.Log(speed);
        this.transform.parent = GameManager.Active.BulletContainer.transform;
    }

    float TimeToImpact(int difficulty, int level)
    {

        return (Mathf.Max(9f - (difficulty / 5), level + 1) * GameManager.Active.GetSpeedModifier(level))/14f;

    }

    public void Throw(Ship iOwner)
    {
        owner = iOwner;
        this.GetComponent<Rigidbody2D>().velocity = owner.transform.right * speed;

    }


    // Update is called once per frame
    void Update()
    {

    }


    public void Explode()
    {
        Destroy(this.gameObject);
        // Show SFX
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Ship ship = col.gameObject.GetComponent<Ship>();

        if (col.gameObject != owner.gameObject)
        {
            if (ship!= null)
            {
                col.gameObject.GetComponent<Ship>().Impact(this);
            }
            else
            {
                // Something else collided with. No idea what, probably another bullet.
            }
        }
        else
        {
            // If it is the owner, ignore it. Can't collide with your own bullets after all. 
        }
    }
}
