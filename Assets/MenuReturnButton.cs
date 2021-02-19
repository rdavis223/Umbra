using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuReturnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        this.GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ReturnToMainMenu(){
        Debug.Log("Return to Main Menu");
    }
}
