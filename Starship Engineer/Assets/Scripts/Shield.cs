using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shield : MonoBehaviour
{
    // Animation Module.
    // Intended to draw the shield fluxuating or changing color or w.e. based on conditions.
    

    public SpriteRenderer[] shieldSprites;
    public int[] shieldThreshhold;

    public float[] shieldWantedAlpha;

    public SpriteRenderer PulseSprite;
    public float PulseWantedAlpha = 0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        for (int x = 0; x < shieldThreshhold.Length; x++)
        {
            float shieldAlpha = Mathf.MoveTowards(shieldSprites[x].color.a, shieldWantedAlpha[x], Time.deltaTime * 5f);

            shieldSprites[x].color = new Color(1, 1, 1, shieldAlpha);
        }
        float pulseAlpha = Mathf.MoveTowards(PulseSprite.color.a,PulseWantedAlpha,Time.deltaTime*5f);

        PulseSprite.color = new Color(1, 1, 1, pulseAlpha);
        
    }


    // Maybe make it all fade-towards instead, like the shield pulse, but later.
    public void UpdateShield(int shieldHealth, bool pulse = false)
    {
        for (int x = 0; x < shieldThreshhold.Length; x++)
        {
            if (shieldHealth >= shieldThreshhold[x])
            {
                if (shieldHealth > shieldThreshhold[x])
                {
                    shieldWantedAlpha[x] = 1f;
                }
                else
                {
                    shieldWantedAlpha[x] = 0.5f;
                }
            }
            else
            {
                shieldWantedAlpha[x] = 0.0f;
            }
        }
        if (pulse)
        {
            // reveal the Shield Pulse, which will rapidly fade out again
            PulseSprite.color = new Color(1, 1, 1, 1f);
        }
    }
}
