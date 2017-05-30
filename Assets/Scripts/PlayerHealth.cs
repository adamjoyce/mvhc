using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth
{
    public Image damageImage;                                               // The image that flashes on screen when damage it taken.
    public float flashSpeed = 5.0f;                                         // The speed at which the damage image will fade.
    public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);           // The colour of the damage image.

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
        if (Damaged)
        {
            // Flash the damage image.
            damageImage.color = flashColour;
        }
        else if (damageImage.color != Color.clear)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        Damaged = false;

        if (Input.GetButtonDown("Fire1"))
        {
            TakeDamage(10);
        }
    }
}