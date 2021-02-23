using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{

    private GameObject playerHealth;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake(){
        playerHealth = GameObject.Find("PlayerHealthMgr");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "Player"){
            playerHealth.GetComponent<PlayerHealthMgr>().health = 100; 
            Destroy(this.gameObject);
        }

    }
}
