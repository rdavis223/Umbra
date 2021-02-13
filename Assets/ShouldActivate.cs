using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldActivate : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;

    void Start()
    {
    }

    void OnEnable(){
        Player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if (distance < 50f){
            this.gameObject.SetActive(true);
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
