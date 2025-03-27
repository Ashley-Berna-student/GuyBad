using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWords : MonoBehaviour
{
    public float scrollSpeed = 40f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
