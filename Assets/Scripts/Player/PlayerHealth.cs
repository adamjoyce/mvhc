using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth
{
    public Image lowHealthImage;                                            // The image that pulses on the screen when health is low.
    public Color lowerPulseColour = new Color(1.0f, 0.0f, 0.0f, 0.25f);     // The colour for the lowHealthImage lower bound.
    public Color upperPulseColour = new Color(1.0f, 0.0f, 0.0f, 0.5f);      // The colour for the lowHealthImage upper bound.
    public float pulseSpeed = 15.0f;                                        // The speed at which the one lerped pulse transistion occurs.
    public float lowHealthPercentageBound = 0.25f;                          // When the player reaches this health percentage screen pulsing begins.

    public Image damageImage;                                               // The image that flashes on screen when damage it taken.
    public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);           // The colour of the damage image.
    public float flashSpeed = 5.0f;                                         // The speed at which the damage image will fade.

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
        if (currentHealth <= maxHealth * lowHealthPercentageBound)
        {
            // Pulse low health.
            if (lowerAlphaReached)
            {
                lowHealthImage.color = Color.Lerp(lowHealthImage.color, upperPulseColour, pulseSpeed * Time.deltaTime);
            }
            else
            {
                lowHealthImage.color = Color.Lerp(lowHealthImage.color, lowerPulseColour, pulseSpeed * Time.deltaTime);
            }

            // Check if either aplha bound has been reached.
            if (lowHealthImage.color == lowerPulseColour)
            {
                lowerAlphaReached = true;
            }
            else if (lowHealthImage.color == upperPulseColour)
            {
                lowerAlphaReached = false;
            }
        }

        // Simulate taking damage with LMB - for debugging.
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    TakeDamage(10);
        //}
    }
}