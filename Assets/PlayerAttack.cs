using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;

    private bool attacking = false;
    private GameObject zombie = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None){
            if (Input.GetMouseButtonDown(0)){
                if (!attacking){
                    animator.SetTrigger("Attack");
                    this.GetComponent<AudioSource>().Play();
                    attacking = true;
                }
            }
        }
    }

    public void attack()
    {
        //This is a coroutine
        //Wait one frame
        if (zombie != null){
            Debug.Log("DealDamage");
            zombie.GetComponent<ZombieAI>().dealDamage(40);
        }
        attacking = false;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.name.Contains("Zombie")){
            zombie = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.name.Contains("Zombie")){
            zombie = null;
        }
    }
}
