using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChancellor : MonoBehaviour
{
    public Transform[] playerPositions;
    public Transform[] signPositions;
    public Transform chancellorSign;
    public float moveSpeed = 10f;
    public float rotateSpeed = 200f;

    private Transform targetSignPosition;

    public bool chancellorChosen = false;

    public void SetChancellor(GameObject chosenPlayer)
    {
        int closestIndex = GetClosestPositionIndex(chosenPlayer.transform.position);
        targetSignPosition = signPositions[closestIndex];
    }

    void Update()
    {
        if (targetSignPosition != null)
        {
            chancellorSign.position = Vector3.MoveTowards(chancellorSign.position, targetSignPosition.position, moveSpeed * Time.deltaTime);
            chancellorSign.rotation = Quaternion.RotateTowards(chancellorSign.rotation, targetSignPosition.rotation, rotateSpeed * Time.deltaTime); 
            chancellorChosen = true;
        }
    }

    int GetClosestPositionIndex(Vector3 playerPos)
    {
        float closestDist = float.MaxValue;
        int index = 0;

        for (int i = 0; i < playerPositions.Length; i++)
        {
            float dist = Vector3.Distance(playerPos, playerPositions[i].position);
            if (dist < closestDist)
            {
                closestDist = dist;
                index = 1;
            }
        }
        return index;
    }
}
