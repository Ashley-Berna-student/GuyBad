using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PolicyTracker : MonoBehaviour
{
    //good card locations:
    public GameObject good1;
    public GameObject good2;
    public GameObject good3;
    public GameObject good4;
    public GameObject good5;

    //bad card locations:
    public GameObject bad1;
    public GameObject bad2;
    public GameObject bad3;
    public GameObject bad4;
    public GameObject bad5;
    public GameObject bad6;

    //using this stuff for testing
    private bool choseACard = false;
    public GameObject testingChosenCard;

    //number of cards that are good or bad:
    public int amountOfGoodCards = 0;
    public int amountOfBadCards = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            choseACard = true;
        }
        if (choseACard)
        {
            KeepTrackOfPolicys();
        }
    }

    public void KeepTrackOfPolicys()
    {
        if (testingChosenCard.CompareTag("BadCard"))
        {
            amountOfBadCards++;

            if (amountOfBadCards == 1)
            {
                bad1.SetActive(true);
                choseACard = false;
            }
            if (amountOfBadCards == 2)
            {
                bad2.SetActive(true);
                choseACard = false;
            }
            if (amountOfBadCards == 3)
            {
                bad3.SetActive(true);
                choseACard = false;
            }
            if (amountOfBadCards == 4)
            {
                bad4.SetActive(true);
                choseACard = false;
            }
            if (amountOfBadCards == 5)
            {
                bad5.SetActive(true);
                choseACard = false;
            }
            if (amountOfBadCards == 6)
            {
                bad6.SetActive(true);
                choseACard = false;
                print("bad guys win");
            }
        }
        if (testingChosenCard.CompareTag("GoodCard"))
        {
            amountOfGoodCards++;

            if (amountOfGoodCards == 1)
            {
                good1.SetActive(true);
                choseACard = false;
            }
            if (amountOfGoodCards == 2)
            {
                good2.SetActive(true);
                choseACard = false;
            }
            if (amountOfGoodCards == 3)
            {
                good3.SetActive(true);
                choseACard = false;
            }
            if (amountOfGoodCards == 4)
            {
                good4.SetActive(true);
                choseACard = false;
            }
            if (amountOfGoodCards == 5)
            {
                good5.SetActive(true);
                choseACard = false;
                print("good guys win");
            }
        }
    }
}
