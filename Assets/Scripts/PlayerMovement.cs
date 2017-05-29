using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    //private Animator anim;                                        // Animator for idle and moving animations.
    private UnityEngine.AI.NavMeshAgent navMeshAgent;               // Pathfinding component for click movement.
    private Transform targetedEnemy;                                // The enemy that is being clicked on.
    private bool walking;                                           // Play walk animation when true.
    private bool enemyClicked;                                      // Move towards clicked enemy.
    private int raycastDistance = 1000;                             // Maximum distance from the player camera to any terrain.

    /* Use this for initialization. */
    private void Awake()
    {
        //anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    /* Update is called once per frame. */
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.CompareTag("EnemyTarget"))
                {
                    // Enemy clicked.
                    targetedEnemy = hit.transform;
                    enemyClicked = true;
                    navMeshAgent.isStopped = true;
                    //Debug.Log("Enemy clicked.");
                }
                else if (hit.collider.CompareTag("NavMesh"))
                {
                    // Ground clicked.
                    walking = true;
                    enemyClicked = false;
                    navMeshAgent.destination = hit.point;
                    navMeshAgent.isStopped = false;
                    //Debug.Log("NavMesh clicked.");
                }
                else
                {
                    // Non-navigable terrain clicked.
                    walking = false;
                    enemyClicked = false;
                    //Debug.Log("Empty-space clicked.");
                }
            }
        }

        if (enemyClicked && Input.GetButton("Fire2") && !targetedEnemy.GetComponent<EnemyGUI>().IsEnabled)
        {
            // Display the enemy GUI options while the button is held.
            targetedEnemy.GetComponent<EnemyGUI>().IsEnabled = true;
        }
        else if (targetedEnemy && targetedEnemy.GetComponent<EnemyGUI>().IsEnabled && !Input.GetButton("Fire2"))
        {
            targetedEnemy.GetComponent<EnemyGUI>().IsEnabled = false;
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