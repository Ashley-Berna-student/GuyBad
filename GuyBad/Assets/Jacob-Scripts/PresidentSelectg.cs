using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public class PresidentSelectg : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) { return; }
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Object in front of player.");
            }
            else
            {
                Debug.Log("Looking at smth");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Object in front of player.");
            }
            else
            {
                Debug.Log("Looking at smth");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.forward * 10);
    }
}
