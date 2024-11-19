using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public Transform player;
    public List<Transform> waypoints;
    public enum State { Patrol, Chase, Attack, Retreat, Aggressive }
    private State currentState;
    public float speed = 2f;
    public float chaseRange = 10f;
    public float detectionDistance = 2f;
    public float rotationSpeed = 2f;
    private int currentWaypoint = 0;
    private float distanceToPlayer;
    private Vector3 targetPosition;
    private PlayerStates playerScript; //used to access treasure count
    private PlayerHealth playerHealthScript; //used to check if player has lost
    private EnemyHealth enemyHealthScript; //used to checkon enemy's health
                                           // Start is called before the first frame update
    public float attackRange = 5f;
    public GameObject projectilePrefab; // Drag the projectile prefab in the inspector
    public float projectileSpeed = 10f;

    void Start()
    {
        currentState = State.Patrol;
        if (waypoints.Count > 0)
        {
            targetPosition = waypoints[currentWaypoint].position;
        }
        playerScript = player.GetComponent<PlayerStates>();
        playerHealthScript = player.GetComponent<PlayerHealth>();
        enemyHealthScript = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
             //   Debug.Log("Patrol State is running");
                if (PlayerDetected())
                {
                    currentState = State.Chase;
                    Debug.Log("Enemy has entered Chase State.");
                }
                break;
            case State.Chase:
                Chase();
                Debug.Log("Chase State is running");
                if (InAttackRange())
                {
                    currentState = State.Attack;
                    Debug.Log("Enemy has entered Attack State");
                }
                else
                {
                    currentState = State.Patrol;

                }
                break;
            case State.Attack:
                Attack();
                Debug.Log("Attack State is running");
                if ( PlayerDetected()==false)
                {
                    currentState = State.Patrol;
                    Debug.Log("Enemy has entered Patrol State");
                }
                else if (LowHealth())
                {
                    currentState = State.Retreat;
                    Debug.Log("Enemy has entered Retreat State");
                }
                break;
            case State.Retreat:
                Retreat();
                Debug.Log("Retreat state is running");
                if (distanceToPlayer > chaseRange) currentState = State.Patrol;
                break;
                // case State.Aggressive:
                // Aggressive();
                // break;
        }
    }

    //Patrol State
    private void Patrol()
    {
        MoveTowardsTarget();
        // Raycasting for wall detection
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, detectionDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                SelectNewWaypoint();
                //you can change this to make your agent do something else!

            }
        }
        // Check if reached the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            SelectNextWaypoint();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void SelectNextWaypoint()
    {
        
        if (waypoints.Count == 0)
            return;
        var Rnd = new System.Random();
        var w = Rnd.Next(0,6);
        currentWaypoint = (currentWaypoint + w) % waypoints.Count;
        targetPosition = waypoints[currentWaypoint].position;
    }

    private void SelectNewWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        // Select a random waypoint different from the current one
        int newWaypoint = currentWaypoint;
        while (newWaypoint == currentWaypoint)
        {
            newWaypoint = Random.Range(0, waypoints.Count);
        }
        currentWaypoint = newWaypoint;
        targetPosition = waypoints[currentWaypoint].position;
    }

    // Optional: Visualize Raycast in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
    }

    private bool PlayerDetected()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= chaseRange;
    }

    //Chase state
    private void Chase()
    {
      //  float distanceToPlayer = Vector3.Distance(transform.position, player.position);
       // if (distanceToPlayer < chaseRange)
        //{
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
       // }
    }

    private bool InAttackRange()
    {
        return distanceToPlayer <= attackRange;
    }

   

    //Attack State
    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, attackRange))
            {
                if (hit.transform == player)
                {
                    AttackPlayer();
                }
            }
        }

    }
    private void AttackPlayer()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (player.position - transform.position).normalized * projectileSpeed*4;


        Destroy(projectile, 1.5f); // Destroy the projectile after 2 seconds
    }

   /* private bool PlayerLost()
    {
        return playerHealthScript.health <= 0;
    }*/

    private bool LowHealth()
    {
        return enemyHealthScript.health <= 6;
    }


    //Retreat State
    private void Retreat()
    {
        Vector3 directionAway = transform.position - player.position;
        directionAway.Normalize();
        float step = speed * Time.deltaTime;
        // Move away from player
        transform.position = Vector3.MoveTowards(transform.position, transform.position + directionAway, step);
    }
}
