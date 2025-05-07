using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class PresidentSelectg : NetworkBehaviour
{
    RaycastHit hit;
    Ray ray;
    private float distance = 3f;
    [SerializeField] public VoteCount vc; 
    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }
        GameObject vt = GameObject.FindWithTag("VoteManager");
        vc = vt.GetComponent<VoteCount>();
        Vector3 fwd = transform.TransformDirection(Vector3.back);

        if (Physics.Raycast(transform.position, fwd, out hit, 100f))
        {
            if (hit.collider.CompareTag("Player"))
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
        if(!IsOwner) { return; }
        ray = new Ray(transform.position + Vector3.up, -transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.magenta);
        if (Physics.Raycast(ray, out hit, 100f))
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectChancellor();
        }
    }

    private void SelectChancellor()
    {
        if(Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<Player>().chancellor = true;
                vc.GetStartRpc(true);
            }
        }
    }
}