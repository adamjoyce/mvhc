using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public abstract class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;                 // The amount of health the character begins the game with.
    public int currentHealth;                   // The character's current health total.
    public AudioClip deathAudio;                // The audio clip that is played when the character dies.

    private Animator anim;                      // Reference to the character's Animator component.
    private AudioSource characterAudio;         // Reference to the character's AudioSource component.
    private bool isDead;                        // Whether the character is dead.
    private bool damaged;                       // True when the player gets damaged.

    /* Use this for initialization. */
    protected virtual void Awake()
    {
        // Reference setup.
        anim = GetComponent<Animator>();
        characterAudio = GetComponent<AudioSource>();

        // Health begins at maximum.
        currentHealth = maxHealth;
    }

    /* Called when the character takes damage from a source. */
    protected virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        characterAudio.Play();

        if (!isDead && currentHealth <= 0)
        {
            OnDeath();
        }
    }
    
    /* Called when the character's hitpoints reach zero for character death. */
    protected virtual void OnDeath()
    {
        isDead = true;

        // Disabled character movement...

        // Turn off snu remaining effects...

        // Tell the animator that the player is dead...

        // Play the character's death audio.
        characterAudio.clip = deathAudio;
        characterAudio.Play();
    }
}