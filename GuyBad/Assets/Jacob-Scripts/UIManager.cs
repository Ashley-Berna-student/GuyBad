using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public Transform canvasTransform;
    [SerializeField] private GameObject liberalCard;
    [SerializeField] private GameObject badGuyCard;
    [SerializeField] private GameObject cardSpawn;

    private void Start()
    {
        canvasTransform = canvas.transform;
    }
    public void GivePresidentCards()
    {
        canvasTransform = canvas.transform;

        Instantiate(RandomCardGenerator(), cardSpawn.transform);
    }
    //Randomly gets a value and based on that value selects a liberalCard or badGuyCard
    public GameObject RandomCardGenerator()
    {
        return liberalCard;
    }
}
