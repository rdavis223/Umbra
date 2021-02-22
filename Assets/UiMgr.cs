using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMgr : MonoBehaviour
{
    public GameObject EscapeMenu;

    public GameObject GameOverMenu;

    public GameObject GameWinMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            GamePause();
        }
    }

    public void GameWin(){
        Time.timeScale = 0;
        GameWinMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameLose(){
        // Time.timeScale = 0;
        // GameOverMenu.SetActive(true);
        // Cursor.lockState = CursorLockMode.None;
    }

    void GamePause(){
        Time.timeScale = 0;
        EscapeMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
