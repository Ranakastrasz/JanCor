using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

    // A ship is the closest thing to the player's avatar in the game.
    // It is mostly just a model, and tracks the player, and enemy's hitpoints.
    // And tells the GameManager when it dies.
    // It launchs attacks

    public int hitpoints; // Represents shields in effect.
    public int maxHitpoints; // that it regenerates between battles is what matters. Its largely abstract anyway.

    public GameObject bulletPrefab;

    Shield shield; // purely Graphical.
    
    public Text healthText;

    // Use this for initialization
    void Start ()
    {
        shield = this.GetComponentInChildren<Shield>();
        shield.UpdateShield(hitpoints, false);
        RefreshUI ();
    }

    // Update is called once per frame
    void Update ()
    {
    }

    public void Impact(Bullet source)
    {
        bool deflect = false;
        if (GameManager.Active.player == this)
        {
            deflect = false;
        }
        else
        {
            deflect = true;
        }

        if (source.equation.GetAnswer() == GameManager.Active.console.GetValue())
        {
            deflect = !deflect;
        }
        else
        {
            
        }

        if (deflect)
        {
            SoundManager.Active.PlaySound(SoundManager.Active.reflect, 1f);
            Invoke("Attack", 0.5f);
            shield.UpdateShield(hitpoints, true);
        }
        else
        {
            TakeDamage(source.owner,1);
        }
        source.Explode();
    }

    public void TakeDamage(Ship source, int damage)
    {
        hitpoints -= damage;
        shield.UpdateShield(hitpoints,false);
        RefreshUI ();


        if (hitpoints <= 0)
        {
            SoundManager.Active.PlaySound(SoundManager.Active.explosion, 0.5f);
            Die();
        }
        else
        {
            SoundManager.Active.PlaySound(SoundManager.Active.explosion,1f, 0.2f);
            GameManager.Active.NewTurn ();
        }
    }

    public void Repair(int amount, bool set = false)
    {
        if (set)
        {
            hitpoints = amount;
            maxHitpoints = amount;
        }
        else
        {
            hitpoints = Mathf.Min(maxHitpoints, hitpoints + amount);
        }
        shield.UpdateShield(hitpoints,false);
        gameObject.SetActive(true);
        RefreshUI();
    }

    void Die()
    {
        GameManager.Active.Die(this);
        gameObject.SetActive (false);
        CancelInvoke("Attack");
        // Explode SFX.
    }

    public void Attack()
    {
        // Throw bullet.
        GameObject obj = Instantiate(bulletPrefab, transform.position,transform.rotation);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.Init(GameManager.Active.currentLevel);
        bullet.Throw(this);
        SoundManager.Active.PlaySound(SoundManager.Active.shoot,1, 0.2f);

    }

    public void RefreshUI()
    {
        healthText.text = hitpoints + "/" + maxHitpoints;

    }

}
