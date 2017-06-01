using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    public float attackRange = 5.0f;                                // The disatnce from a target the player must be before performing an attack.

    //private Animator anim;                                        // Animator for idle and moving animations.
    private UnityEngine.AI.NavMeshAgent navMeshAgent;               // Pathfinding component for click movement.
    private GameObject targetedEnemy;                               // The enemy that is being clicked on.
    private bool walking;                                           // Play walk animation when true.
    private bool enemyClicked;                                      // Move towards clicked enemy.
    private bool performAttack;                                     // True is an attack option has been selected.
    private int raycastDistance = 1000;                             // Maximum distance from the player camera to any terrain.

    /* Use this for initialization. */
    private void Awake()
    {
        //anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        walking = false;
        enemyClicked = false;
        performAttack = false;
    }

    /* Update is called once per frame. */
    private void Update()
    {
        // Raycast for player movement.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.CompareTag("EnemyTarget"))
                {
                    // Enemy clicked.
                    targetedEnemy = hit.transform.gameObject;
                    enemyClicked = true;
                    performAttack = false;
                    navMeshAgent.isStopped = true;
                    //Debug.Log("Enemy clicked.");
                }
                else if (hit.collider.CompareTag("NavMesh"))
                {
                    // Ground clicked.
                    walking = true;
                    enemyClicked = false;
                    performAttack = false;
                    navMeshAgent.destination = hit.point;
                    navMeshAgent.isStopped = false;
                    //Debug.Log("NavMesh clicked.");
                }
                else
                {
                    // Non-navigable terrain clicked.
                    walking = false;
                    enemyClicked = false;
                    performAttack = false;
                    //Debug.Log("Empty-space clicked.");
                }
            }
        }

        // Update enemy GUI components.
        if (enemyClicked && Input.GetButton("Fire2") && !targetedEnemy.GetComponent<EnemyGUI>().IsEnabled)
        {
            // Display the enemy GUI options while the button is held.
            targetedEnemy.GetComponent<EnemyGUI>().IsEnabled = true;
        }
        else if (targetedEnemy && targetedEnemy.GetComponent<EnemyGUI>().IsEnabled && !Input.GetButton("Fire2"))
        {
            targetedEnemy.GetComponent<EnemyGUI>().IsEnabled = false;
        }

        // Signals an attack is coming.
        if (Input.GetButtonUp("Fire2"))
        {
            performAttack = true;
        }

        // Is an enemy option button being selected?
        if (enemyClicked && performAttack)
        {
            if (targetedEnemy.GetComponent<EnemyGUI>().HoverLeftButton)
            {
                MoveAndAttack(targetedEnemy, true);
            }
            else if (targetedEnemy.GetComponent<EnemyGUI>().HoverRightButton)
            {
                MoveAndAttack(targetedEnemy, false);
            }
        }
    }

    /* Move the player into target range and perform an attack. */
    private void MoveAndAttack(GameObject target, bool killTarget)
    {
        navMeshAgent.destination = target.transform.position;

        float distanceToTarget = (target.transform.position - transform.position).magnitude;
        if (distanceToTarget >= attackRange)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        else
        {
            PlayerAbilities playerAbilities = GetComponent<PlayerAbilities>();
            if (playerAbilities)
            {
                if (killTarget)
                {
                    // Eliminate the target.
                    playerAbilities.EliminateTarget(target);
                    performAttack = false;
                }
                else
                {
                    // Subdue the target.
                    playerAbilities.SubdueTarget(target);
                    performAttack = false;
                }
            }
            else
            {
                Debug.LogWarning("Warning: Player missing PlayerAbilities.cs script.");
            }

            navMeshAgent.isStopped = true;
            walking = false;
        }
    }

    /* Sets the player's NavMesh agent movement - facilitates an abrupt stop. */
    private void SetMovement(bool isStationary, bool forceAbruptStop = false)
    {
        if (forceAbruptStop)
            navMeshAgent.velocity = Vector3.zero;

        navMeshAgent.isStopped = isStationary;
    }
}