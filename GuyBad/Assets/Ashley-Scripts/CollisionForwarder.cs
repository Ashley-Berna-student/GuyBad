using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionForwarder : MonoBehaviour
{
    private KillAnimations killAnimations;

    void Start()
    {
        killAnimations = GetComponentInChildren<KillAnimations>();
    }

    void OnCollisionEnter(Collision collision)
    {
        killAnimations?.HandleCollision(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        killAnimations?.HandleCollisionExit(collision);
    }
}
