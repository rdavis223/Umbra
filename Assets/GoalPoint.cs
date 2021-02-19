using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    private GameObject UiMgr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        UiMgr = GameObject.Find("UiMgr");
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "Player"){
            UiMgr.GetComponent<UiMgr>().GameWin();
        }
    }
}
