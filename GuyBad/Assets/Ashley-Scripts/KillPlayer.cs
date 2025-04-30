using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject particles;
    public float timeBeforeExploding = 1f;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Host"))
        {
            StartCoroutine(DisableAfterDelay(collision.gameObject));
        }
    }

    IEnumerator DisableAfterDelay(GameObject target)
    {
        yield return new WaitForSeconds(timeBeforeExploding);
        target.SetActive(false);
        particles.SetActive(true);
        yield return new WaitForSeconds(timeBeforeExploding);
        gameObject.SetActive(false);
    }
}
