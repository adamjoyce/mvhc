using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    public float attackRange = 5.0f;                                // The disatnce from a target the player must be before performing an attack.

    private Animator anim;                                          // Animator for idle and moving animations.
    private UnityEngine.AI.NavMeshAgent navMeshAgent;               // Pathfinding component for click movement.
    private GameObject targetedEnemy;                               // The enemy that is being clicked on.
    private bool isWalking;                                         // Play walk animation when true.
    private bool enemyClicked;                                      // Move towards clicked enemy.
    private bool performAttack;                                     // True is an attack option has been selected.
    private int raycastDistance = 1000;                             // Maximum distance from the player camera to any terrain.
    private float originalStoppingDistance = 0.0f;                  // The base stopping distance for the player nav mesh compoennt.

    /* Use this for initialization. */
    private void Awake()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        isWalking = false;
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
                }
                else if (hit.collider.CompareTag("NavMesh"))
                {
                    // Ground clicked.
                    //isWalking = true;
                    enemyClicked = false;
                    navMeshAgent.destination = hit.point;
                    navMeshAgent.isStopped = false;
                }
                performAttack = false;
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

        // Signals an attack may be coming.
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

        // Has the player reached its destination?
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
                isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        // Switch between walk and idle animation when necessary.
        anim.SetBool("IsWalking", isWalking);
    }

    /* Move the player into target range and perform an attack. */
    private void MoveAndAttack(GameObject target, bool killTarget)
    {
        navMeshAgent.destination = target.transform.position;

        // Alter the stopping distance to allow for transistion to idle animation.
        if (navMeshAgent.stoppingDistance != attackRange)
        {
            navMeshAgent.stoppingDistance = attackRange;
        }

        float distanceToTarget = (target.transform.position - transform.position).magnitude;
        if (distanceToTarget >= attackRange)
        {
            navMeshAgent.isStopped = false;
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
                }
                else if (!target.GetComponent<EnemyHealth>().IsSubdued)
                {
                    // Subdue the target.
                    playerAbilities.SubdueTarget(target);
                }
                else
                {
                    // Amalgamate with target.
                    playerAbilities.AmalgamateWithTarget(target);
                }
                enemyClicked = false;
            }
            else
            {
                Debug.LogWarning("Warning: Player missing PlayerAbilities.cs script.");
            }

            navMeshAgent.isStopped = true;
        }
    }
}