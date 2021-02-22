using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMgr : MonoBehaviour
{
    public float respawnRate;
    public float respawnTime;
    public int quadSize = 400;
    public int initalZombies = 4;
    private int currentZombies = 0;
    public GameObject zombiePrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentZombies = initalZombies;
        StartCoroutine("SpawnZombies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnZombies(){
        int i = 1;
        int j = 1;
        int zombieNum = currentZombies;
        if (zombieNum > 30){
            zombieNum = 30;
        }
        while (i < quadSize*2){
            j = 0;
            while (j < quadSize*2){
                int x = 0;
                while (x < zombieNum){
                    GameObject zombie = Instantiate(zombiePrefab);
                    zombie.transform.position = new Vector3(Random.Range(i, i +quadSize), 0 , Random.Range(j, j+quadSize));
                    x++;
                }
                j+=quadSize;
            }
            i+=quadSize;
        }
        currentZombies = Mathf.FloorToInt(currentZombies * respawnRate);;
        yield return new WaitForSeconds(respawnTime);
        StartCoroutine("SpawnZombies");
    }
}
