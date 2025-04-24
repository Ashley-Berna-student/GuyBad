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

    public Transform location1;
    public Transform location2;
    private float rotationSpeed = 5000f;
    private Transform currentTarget;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            shootPlayer = true;
            GetRandomObject();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            currentTarget = location1;
            print("location = 1");
            TurnShooter();
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            currentTarget = location2;
            print("location = 2");
            TurnShooter();
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

    public void TurnShooter()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
