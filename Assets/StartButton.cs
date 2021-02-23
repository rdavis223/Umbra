﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(StartButtonActivate);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartButtonActivate()
    {
          SceneManager.LoadScene("Prototype");
    }
}
