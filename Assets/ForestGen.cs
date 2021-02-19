﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGen : MonoBehaviour
{

    public int forestSize = 25;
    public int elementSpacing = 40;
    public GameObject treePrefab;

    public float randomRange = 2f;

    public LayerMask whatIsGround;

    public Collider terrain;

    private List<ActivatorItem> activatorItems;

    public GameObject player;

    public int cullingRange = 80;

    public GameObject heliPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int i = (int) this.transform.position.x;
        int j = (int) this.transform.position.z;
        int max_i = (int) terrain.bounds.max.x;
        int max_j = (int) terrain.bounds.max.z;

        activatorItems = new List<ActivatorItem>();
        Vector3 heliPos = new Vector3(0,0,0);
        bool run = true;
        //place helicopter
        while (run){
            int heliX = Random.Range(10, max_i - 10);
            int heliZ = Random.Range(10, max_j - 10);
            heliPos = new Vector3(heliX, 0, heliZ);
            if (Vector3.Distance(heliPos, player.transform.position) > 250)
            {
                run = false;
            }
        }

        GameObject heli = Instantiate(heliPrefab);
        heli.transform.position = heliPos;
        ActivatorItem item = new ActivatorItem();
        item.itemRef = heli;
        item.itemPos = heli.transform.position;
        item.isHidden = true;
        heli.SetActive(false); 
        activatorItems.Add(item);

        int treeCount = 0; 
        while (i < max_i){
            j = (int) this.transform.position.z;
            while (j < max_j){
                Vector3 position = new Vector3(i + Random.Range(-randomRange, randomRange), 0, j + Random.Range(-randomRange, randomRange));
                if (Vector3.Distance(heli.transform.position, position) < 12){
                    j+= elementSpacing;
                }
                else if (position.x > 0 && position.z > 0 && position.x < max_i && position.z < max_j){
                    GameObject newTree = Instantiate(treePrefab);
                    treeCount++;
                    newTree.transform.position = position;
                    item = new ActivatorItem();
                    item.itemRef = newTree;
                    item.itemPos = newTree.transform.position;
                    item.isHidden = true;
                    newTree.SetActive(false); 
                    activatorItems.Add(item);
                    j+= elementSpacing;
                }
            } 
            i+= elementSpacing;

        }
        StartCoroutine("CheckActivation");
        Debug.Log(treeCount);
    }

    IEnumerator CheckActivation(){
        foreach (ActivatorItem item in activatorItems){
            if (Vector3.Distance(player.transform.position, item.itemPos) > cullingRange){
                if (!item.isHidden){
                    item.isHidden = true;
                    item.itemRef.SetActive(false);
                }
            } else {
                if (item.isHidden){
                    item.isHidden = false;
                    item.itemRef.SetActive(true);
                }
            }
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("CheckActivation");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ActivatorItem
{
    public GameObject itemRef;
    public Vector3 itemPos;
    public bool isHidden;
}