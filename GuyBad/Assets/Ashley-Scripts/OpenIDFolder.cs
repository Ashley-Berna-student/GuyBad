using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenIDFolder : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public GameObject folder;
    public RectTransform partyCard;
    public RectTransform idCard;
    public float moveAmountPartyCard = 150;
    public float moveAmountIdCard = 300;
    public float moveSpeed = 0.2f;
    public AudioClip paperRustle;

    private Vector2 originalPartyPos;
    private Vector2 originalIDPos;
    private Coroutine moveCoroutine;

    private void Start()
    {
        if (partyCard != null)
        {
            originalPartyPos = partyCard.anchoredPosition;
        }

        if (idCard != null)
        {
            originalIDPos = idCard.anchoredPosition;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == folder)
        {

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                PlaySound();
            }

            moveCoroutine = StartCoroutine(MoveCards(originalPartyPos.x - moveAmountPartyCard, originalIDPos.x - moveAmountIdCard));
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        if (eventData.pointerEnter == folder)
        {

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                PlaySound();
            }

            moveCoroutine = StartCoroutine(MoveCards(originalPartyPos.x, originalIDPos.x));
        }
    }

    private IEnumerator MoveCards(float targetPartyX, float targetIDX)
    {
        float elapsedTime = 0f;
        Vector2 startPartyPos = partyCard.anchoredPosition;
        Vector2 startIDPos = idCard.anchoredPosition;

        while (elapsedTime < moveSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveSpeed;

            partyCard.anchoredPosition = new Vector2(Mathf.Lerp(startPartyPos.x, targetPartyX, t), startPartyPos.y);
            idCard.anchoredPosition = new Vector2(Mathf.Lerp(startIDPos.x, targetIDX, t), startIDPos.y);

            yield return null;
        }

        partyCard.anchoredPosition = new Vector2(targetPartyX, startPartyPos.y);
        idCard.anchoredPosition = new Vector2(targetIDX, startIDPos.y);
    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(paperRustle, transform.position);
    }
}
