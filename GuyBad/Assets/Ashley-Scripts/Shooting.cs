using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject[] perjectiles;
    public Transform shooter;
    private GameObject randomObject;
    public GameObject rayGun;

    public float force;
    public bool shootPlayer = false;

    public Transform location1;
    public Transform location2;
    public Transform location3;
    public Transform location4;
    public Transform location5;
    public Transform location6;
    public Transform location7;
    public Transform location8;
    public Transform location9;
    public Transform location10;
    private float rotationSpeed = 5000f;
    private Transform currentTarget;

    public GameObject badGuyCard1;
    public GameObject badGuyCard2;
    private bool alreadyShot = true;
    private bool wasBadGuy1Active = false;
    private bool wasBadGuy2Active = false;
    
    void Update()
    {
        bool justActivated1 = badGuyCard1.activeSelf && !wasBadGuy1Active;
        bool justActivated2 = badGuyCard2.activeSelf && !wasBadGuy2Active;

        if((justActivated1 || justActivated2) && !shootPlayer)
        {
            shootPlayer = true;
            GetRandomObject();
            alreadyShot = false;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            currentTarget = location1;
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            currentTarget = location2;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentTarget = location3;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentTarget = location4;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTarget = location5;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentTarget = location6;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentTarget = location7;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentTarget = location8;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            currentTarget = location9;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentTarget = location10;
            Shoot();
        }

        wasBadGuy1Active = badGuyCard1.activeSelf;
        wasBadGuy2Active = badGuyCard2.activeSelf;
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

    public void Shoot()
    {
        if (shootPlayer && !alreadyShot)
        {
            TurnShooter();

            rayGun.SetActive(true);
            GameObject bullet = Instantiate(randomObject, shooter.position, shooter.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shooter.forward * force * Time.deltaTime;
            shootPlayer = false;
            alreadyShot = true;
            StartCoroutine(DeactivateRayGun());
        }
    }

    IEnumerator DeactivateRayGun()
    {
        yield return new WaitForSeconds(1f);
        rayGun.SetActive(false);
    }
}
