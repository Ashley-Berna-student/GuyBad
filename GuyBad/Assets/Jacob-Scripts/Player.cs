using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public GameObject playerCapsule; 
    private Color playerColor;
    public string playerName;
    private bool vote = false;
    private GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
       Renderer ma = playerCapsule.GetComponent<MeshRenderer>();
        playerColor = Random.ColorHSV();
        ma.material.SetColor("_Color", playerColor);
       //players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
