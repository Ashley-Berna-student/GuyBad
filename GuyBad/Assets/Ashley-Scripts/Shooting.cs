using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject[] perjectiles;
    public Transform shooter;
    private GameObject randomObject;

    public float force;
    public bool shootPlayer = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            shootPlayer = true;
            GetRandomObject();
        }
        if (shootPlayer)
        {
            GameObject bullet = Instantiate(randomObject, shooter.position, shooter.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shooter.forward * force * Time.deltaTime;
            shootPlayer = false;
        }
    }

    public void GetRandomObject()
    {
        int randomIndex = UnityEngine.Random.Range(0, perjectiles.Length);
        randomObject = perjectiles[randomIndex];
    }
}
