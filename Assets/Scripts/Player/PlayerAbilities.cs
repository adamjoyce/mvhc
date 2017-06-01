using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    /* Eliminates the given target. */
    public void EliminateTarget(GameObject target)
    {
        EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
        if (targetHealth && !targetHealth.IsDead)
        {
            targetHealth.TakeDamage(targetHealth.currentHealth);
        }
    }

    /* Subdues the given target. */
    public void SubdueTarget(GameObject target)
    {
        EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
        if (targetHealth && !targetHealth.IsDead)
        {
            // Subdue the target...
            // AKA set subdued state...
            Debug.Log("SUBDUE!");
        }
    }
}