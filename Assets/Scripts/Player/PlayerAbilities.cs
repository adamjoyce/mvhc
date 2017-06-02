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
            Debug.Log("ELIMINATED");
        }
    }

    /* Subdues the given target. */
    public void SubdueTarget(GameObject target)
    {
        EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
        if (targetHealth && !targetHealth.IsDead)
        {
            target.GetComponent<EnemyHealth>().IsSubdued = true;
            Debug.Log("SUBDUED");
        }
    }

    /* Amalgamates with the given target. */
    public void AmalgamateWithTarget(GameObject target)
    {
        EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
        if (targetHealth && !targetHealth.IsDead)
        {
            Debug.Log("AMALGAMATED");
        }
    }
}