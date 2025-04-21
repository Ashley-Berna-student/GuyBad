using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Editor.Configuration;
using UnityEngine;

public class ChoosePolicyCard : MonoBehaviour
{
    public GameObject[] policyCards;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PickACard();
        }
    }
    public void PickACard()
    {
        policyCards = GameObject.FindGameObjectsWithTag("card");

        List<GameObject> availableCards = new List<GameObject>(policyCards);
        List<GameObject> selectedCards = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, availableCards.Count);
            GameObject chosenCard = availableCards[randIndex];
            selectedCards.Add(chosenCard);
            print($"Picked card: {chosenCard.name}");
            availableCards.RemoveAt(randIndex);
        }
    }
}
