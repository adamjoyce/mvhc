using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth
{
    public Image lowHealthImage;                                            // The image that pulses on the screen when health is low.
    public Color pulseColour = new Color(0.0f, 0.0f, 0.0f, 0.5f);           // 

    public Image damageImage;                                               // The image that flashes on screen when damage it taken.
    public float flashSpeed = 5.0f;                                         // The speed at which the damage image will fade.
    public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);           // The colour of the damage image.

    private bool lowerAlphaReached = true;

    /* Use this for initialization. */
    void Start()
    {
        if (!damageImage)
        {
            Debug.LogWarning("Warning: Player missing damageImage reference");
        }
    }

    /* Update is called once per frame. */
    protected override void Update()
    {
        // Visual feedback for when damage is taken.
        if (Damaged)
        {
            // Flash the damage image.
            damageImage.color = flashColour;
        }
        else if (damageImage.color != Color.clear)
        {
            // Fade damageImage to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        Damaged = false;

        // Visual feedback for low health status.
        if (currentHealth <= maxHealth * 0.25f)
        {
            // Pulse low health.
            if (lowerAlphaReached)
            {
                lowHealthImage.color = Color.Lerp(lowHealthImage.color, pulseColour, 15.0f * Time.deltaTime);
            }
            else
            {
                lowHealthImage.color = Color.Lerp(lowHealthImage.color, new Color(0, 0, 0, 0.25f), 15.0f * Time.deltaTime);
            }

            if (lowHealthImage.color == new Color(0, 0, 0, 0.25f))
            {
                lowerAlphaReached = true;
            }
            else if (lowHealthImage.color == pulseColour)
            {
                lowerAlphaReached = false;
            }
        }

        // Simulate taking damage with LMB - for debugging.
        if (Input.GetButtonDown("Fire1"))
        {
            TakeDamage(10);
        }
    }
}