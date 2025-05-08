using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject setActiveObject;
    public float timer;

    void Start()
    {
        StartCoroutine(Appear(timer));
    }

    IEnumerator Appear(float delay)
    {
        yield return new WaitForSeconds(delay);
        setActiveObject.SetActive(true);
    }
}
