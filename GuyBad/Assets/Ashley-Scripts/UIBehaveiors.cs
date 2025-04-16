using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIBehaveiors : MonoBehaviour
{
    public UnityEvent buttonClick;
    public AudioClip gaspSound;

    private void Awake()
    {
        if (buttonClick != null)
        {
            buttonClick = new UnityEvent();
        }
    }
    private void OnMouseUp()
    {
        
    }
}
