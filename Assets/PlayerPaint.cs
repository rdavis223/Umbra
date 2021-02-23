using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaint : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)){
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.name.Contains("RedWood")){
                    GameObject go = Instantiate(prefab);
                    go.transform.position = (hit.point);
                    go.transform.LookAt(transform.position);
                    go.transform.Rotate(90,0,0);
                }
            }
        }
    }
}
