﻿using System.Collections;
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

    private float attackCooldown = 0f;

    private GameObject playerHealth;

    private Animator animator;

    private bool justAttacked = false;
    private float animationCooldown = 0f;

    private bool takingDamage = false;

    private int zombieHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        player = GameObject.Find("Player");
        playerHealth = GameObject.Find("PlayerHealthMgr");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (justAttacked || takingDamage){
            return;
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
            Invoke("Attack", 0.5f);
        } else if (playerInSightRange){
            animator.SetTrigger("Run");
            Chase();
        } else {
            animator.SetTrigger("Walk");
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
        if (!takingDamage){
            animator.SetTrigger("Attack");
            agent.SetDestination(transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            attackCooldown = 1.5f;
            playerHealth.GetComponent<PlayerHealthMgr>().dealDamage(10);
            justAttacked = false;
        }
        justAttacked = false;
    }

    public void dealDamage(int damage){
        if (!takingDamage){
            agent.enabled = false;
            takingDamage = true;
            animator.SetTrigger("Idle");
            animator.SetTrigger("Damage");
        } 
        //Do Damage here regardless
        zombieHealth -= damage;
        if (zombieHealth <= 0){
            Destroy(this.gameObject);
        }
    }

    public void finishDamage(){
        takingDamage = false;
        agent.enabled = true;
    }
}
