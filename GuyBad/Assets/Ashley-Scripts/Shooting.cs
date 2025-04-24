using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject perjectile;
    public Transform shooter;

    public float force;
    public bool shootPlayer = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            shootPlayer = true;
        }
        if (shootPlayer)
        {
            GameObject bullet = Instantiate(perjectile, shooter.position, shooter.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shooter.forward * force * Time.deltaTime;
            shootPlayer = false;
        }
    }
}
