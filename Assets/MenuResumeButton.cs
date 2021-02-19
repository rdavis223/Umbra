using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuResumeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        this.GetComponent<Button>().onClick.AddListener(ResumeGame);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ResumeGame(){
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        this.transform.parent.gameObject.SetActive(false);
    }
}
