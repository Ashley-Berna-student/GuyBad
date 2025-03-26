using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public class Player
    {
        public Color color;
        public string role;
        public bool vote;
    }

    [SerializeField] public int cardCount;
    public int voteCount;
    [SerializeField] public Player[] players;
    // Start is called before the first frame update

    void Start()
    {
        GameObject[] intCount = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length < 5)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
