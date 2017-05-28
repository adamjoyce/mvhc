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
                    Debug.Log("EnemyClicked");
                }
                else
                {
                    // Ground clicked.
                    walking = true;
                    enemyClicked = false;
                    navMeshAgent.destination = hit.point;
                    navMeshAgent.Resume();
                }
            }
        }

        if (enemyClicked)
        {
            // Move within range and kill enemy.
        }
    }
}