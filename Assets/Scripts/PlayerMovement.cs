using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //private Animator anim;                          // Animator for idle and moving animations.
    private UnityEngine.AI.NavMeshAgent navMeshAgent;              // Pathfinding component for click movement.
    private Transform targetedEnemy;                // The enemy that is being clicked on.
    private bool walking;                           // Play walk animation when true.
    private bool enemyClicked;                      // Move towards clicked enemy.
    private int raycastDistance = 100;              // Maximum distance from the player camera to any terrain.

    // Use this for initialization.
    void Awake()
    {
        //anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame.
    void Update()
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
                }
                else
                {
                    // Ground clicked.
                    walking = true;
                    enemyClicked = false;
                    navMeshAgent.destination = hit.point;
                    navMeshAgent.isStopped = false;
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
}