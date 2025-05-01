using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresidentSelectg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.back);
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            if (hit.transform.CompareTag("Player"))
            {
                print("Object in front of player.");
            }
        }
    }
}
