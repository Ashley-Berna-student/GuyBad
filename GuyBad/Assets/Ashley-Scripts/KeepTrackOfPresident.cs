using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTrackOfPresident : MonoBehaviour
{
    // President locations
    public Transform president1;
    public Transform president2;
    public Transform president3;
    public Transform president4;
    public Transform president5;
    public Transform president6;
    public Transform president7;
    public Transform president8;
    public Transform president9;
    public Transform president10;

    // President sign locations
    public Transform sign1;
    public Transform sign2;
    public Transform sign3;
    public Transform sign4;
    public Transform sign5;
    public Transform sign6;
    public Transform sign7;
    public Transform sign8;
    public Transform sign9;
    public Transform sign10;

    void Update()
    {
        UpdatePresidentTag();
    }

    private GameObject lastPresident = null;

    void UpdatePresidentTag()
    {
        GameObject[] playersWithPlayerTag = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] playersWithHostTag = GameObject.FindGameObjectsWithTag("Host");
        GameObject currentChancellor = GameObject.FindGameObjectWithTag("Chancellor");

        List<GameObject> allPlayers = new List<GameObject>();
        allPlayers.AddRange(playersWithPlayerTag);
        allPlayers.AddRange(playersWithHostTag);

        Vector3 targetPosition = Vector3.zero;

        if (IsAt(transform.position, sign1.position))
        {
            targetPosition = president1.position;
        }
        else if (IsAt(transform.position, sign2.position)) targetPosition = president2.position;
        else if (IsAt(transform.position, sign3.position)) targetPosition = president3.position;
        else if (IsAt(transform.position, sign4.position)) targetPosition = president4.position;
        else if (IsAt(transform.position, sign5.position)) targetPosition = president5.position;
        else if (IsAt(transform.position, sign6.position)) targetPosition = president6.position;
        else if (IsAt(transform.position, sign7.position)) targetPosition = president7.position;
        else if (IsAt(transform.position, sign8.position)) targetPosition = president8.position;
        else if (IsAt(transform.position, sign9.position)) targetPosition = president9.position;
        else if (IsAt(transform.position, sign10.position)) targetPosition = president10.position;
        else return;

        GameObject newPresident = null;
        float closestDistance = float.MaxValue;
        float threshold = 0.5f; // only within this distance is allowed to become president

        foreach (GameObject player in allPlayers)
        {
            float dist = Vector3.Distance(player.transform.position, targetPosition);
            if (dist < closestDistance && dist < threshold)
            {
                closestDistance = dist;
                newPresident = player;
            }
        }

        if (newPresident != null && newPresident != lastPresident)
        {
            if (lastPresident != null)
            {
                lastPresident.tag = "Player";

                // Null check for currentChancellor before modifying it
                if (currentChancellor != null)
                {
                    currentChancellor.tag = "Player";
                    print("chancler is reset");
                }
                else
                {
                    Debug.LogWarning("Current Chancellor is null, cannot reset tag.");
                }
            }

            newPresident.tag = "President";
            PlayerListItemUI.ResetChancellorChoice();
            lastPresident = newPresident;
        }
    }

    GameObject FindClosestPlayer(List<GameObject> players, Vector3 targetPos)
    {
        GameObject closest = null;
        float minDist = float.MaxValue;

        foreach (GameObject player in players)
        {
            float dist = Vector3.Distance(player.transform.position, targetPos);
            if (dist < minDist)
            {
                minDist = dist;
                closest = player;
            }
        }

        return closest;
    }


    bool IsAt(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b) < 0.1f;
    }
}
