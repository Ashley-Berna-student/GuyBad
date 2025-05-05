using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Editor.Configuration;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePolicyCard : MonoBehaviour
{
    public GameObject[] policyCards;
    public RectTransform[] cardSlots;

    private List<GameObject> selectedCards = new List<GameObject>();
    private GameObject discardedCard;
    private List<GameObject> cardsToSendToChancelor = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PickACard();
        }
    }
    public void PickACard()
    {
        GameObject[] goodCards = GameObject.FindGameObjectsWithTag("GoodCard");
        GameObject[] badCards = GameObject.FindGameObjectsWithTag("BadCard");

        List<GameObject> availableCards = new List<GameObject>();
        availableCards.AddRange(goodCards);
        availableCards.AddRange(badCards);
        selectedCards.Clear();

        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, availableCards.Count);
            GameObject chosenCard = availableCards[randIndex];
            selectedCards.Add(chosenCard);
            print($"Picked card: {chosenCard.name}");
            availableCards.RemoveAt(randIndex);

            //moving cards to be selected
            RectTransform cardRect = chosenCard.GetComponent<RectTransform>();
            cardRect.SetParent(cardSlots[i].parent);
            cardRect.anchoredPosition = cardSlots[i].anchoredPosition;

            Button button = chosenCard.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => DiscardPolicyCard(chosenCard));
            }

            print($"Picked card: {chosenCard.name} Slot {i}");
        }
    }

    public void DiscardPolicyCard(GameObject cardToDiscard)
    {
        discardedCard = cardToDiscard;
        print($"Card discarded: {cardToDiscard.name}");

        cardsToSendToChancelor = new List<GameObject>(selectedCards);
        cardsToSendToChancelor.Remove(cardToDiscard);

        cardToDiscard.SetActive(false);

        print($"cards to send to chancellor: {cardsToSendToChancelor[0].name}, {cardsToSendToChancelor[1].name}");
    }
}
