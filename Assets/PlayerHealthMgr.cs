using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthMgr : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 100;

    public Slider healthBar;

    public GameObject fillArea;

    public GameObject UiMgr;


    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 1)
        {
            healthBar.value = health;
        } 
        else 
        {
            fillArea.SetActive(false);
            UiMgr.GetComponent<UiMgr>().GameLose();
        }
        
    }
    
    public void dealDamage(int damage){
        health -= damage;
    }
}
