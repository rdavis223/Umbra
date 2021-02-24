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
    bool walkPointSet = false;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private float attackCooldown = 0f;

    private GameObject playerHealth;

    private Animator animator;

    private bool justAttacked = false;
    private float animationCooldown = 0f;

    private bool takingDamage = false;

    private int zombieHealth = 100;

    public Vector3 properPos;

    private bool playedSound = false;

    public bool first_run = true;

    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        player = GameObject.Find("Player");
        playerHealth = GameObject.Find("PlayerHealthMgr");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        transform.position = properPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (first_run){
            agent.Warp(properPos);
            first_run = false;
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (justAttacked || isDead){
            return;
        }

        if (takingDamage){
            agent.enabled = false;
            return;
        } else {
            agent.enabled = true;
        }
        if (attackCooldown > 0f){
            attackCooldown -= Time.deltaTime;
        }

        if (attackCooldown > 0f && playerInAttackRange){
            animator.SetTrigger("Idle");
        }
        else if (playerInAttackRange && attackCooldown <= 0f){
            justAttacked = true;
            animator.SetTrigger("Idle");
            Invoke("Attack", 0.7f);
        } else if (playerInSightRange){
            animator.SetTrigger("Run");
            Chase();
        } else {
            animator.SetTrigger("Walk");
            Patrolling();
        }
    }

    void Patrolling(){
        playedSound = false;
        this.GetComponent<NavMeshAgent>().speed = 1.5f;
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
        if (!takingDamage){
            this.GetComponent<NavMeshAgent>().speed = 7f;
            if (Vector3.Distance(player.transform.position, transform.position) < 10 && !playedSound){
                this.GetComponents<AudioSource>()[0].Play();
                playedSound = true;
            }
            agent.SetDestination(player.transform.position);
        }
    }

    void Attack(){
        if (!takingDamage && !isDead){
            animator.SetTrigger("Attack");
            this.GetComponents<AudioSource>()[1].Play();
            agent.SetDestination(transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            attackCooldown = 1.5f;
            playerHealth.GetComponent<PlayerHealthMgr>().dealDamage(10);
            justAttacked = false;
        }
        justAttacked = false;
    }

    public void dealDamage(int damage){
        if (!isDead){
            CancelInvoke("Attack");
            justAttacked = false;
            if (!takingDamage){
                agent.enabled = false;
                takingDamage = true;
                animator.SetTrigger("Idle");
                animator.SetTrigger("Damage");
            } 
            //Do Damage here regardless
            zombieHealth -= damage;
            if (zombieHealth <= 0){
                agent.enabled = false;
                animator.SetTrigger("Die");
                isDead = true;
            }
        }
    }

    public void finishDamage(){
        if (!isDead){
            takingDamage = false;
            agent.enabled = true;
        }
    }

    public void Die(){
        Destroy(animator.gameObject);
    }
}
