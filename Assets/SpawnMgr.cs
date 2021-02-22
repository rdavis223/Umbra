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
        int i = 0;
        int j = 0;
        int zombieNum = currentZombies;
        if (zombieNum > 30){
            zombieNum = 30;
        }
        while (i < 800){
            Debug.Log(i);
            j = 0;
            while (j < 800){
                Debug.Log(j);
                int x = 0;
                while (x < zombieNum){
                    GameObject zombie = Instantiate(zombiePrefab);
                    zombie.GetComponent<ZombieAI>().properPos = new Vector3(Random.Range((float) i, (float) i +quadSize), 0 , Random.Range((float)j, (float) j+quadSize));
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
