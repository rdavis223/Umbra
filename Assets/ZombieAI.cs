using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange){
            Attack();
        } else if (playerInSightRange){
            Chase();
        } else {
            Patrolling();
        }
    }

    void Patrolling(){
        if (!walkPointSet){
            float z = Random.Range(-walkPointRange, walkPointRange);
            float x = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
                walkPointSet = true;
            }   
        } 
        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distance = transform.position - walkPoint;
        if (distance.magnitude < 1f){
            walkPointSet = false;
        }
    }

    void Chase(){
        agent.SetDestination(player.transform.position);
    }

    void Attack(){
        agent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
    }
}
