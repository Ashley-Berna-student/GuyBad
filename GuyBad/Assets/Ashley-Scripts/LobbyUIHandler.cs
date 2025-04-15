using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobbyUIHandler : MonoBehaviour
{
    public UnityEngine.Camera playerCamera;
    public GameObject[] colorObjects;
    public GameObject clapButton;
    public GameObject gaspButton;
    public GameObject laughButton;
    public AudioClip clapSound;
    public AudioClip gaspSound;
    public AudioClip laughSound;
    private bool isClapping = false;
    private bool isGasping = false;
    private bool isLaughing = false;

    void Update()
    {
        if (playerCamera == null)
        {
            print("no camera assigned");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            print("mouse click detected");

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                print("raycast hit: " + hit.collider.gameObject.name);
                SetActiveColor(hit.collider.gameObject.name);
            }
            else
            {
                print("raycast didnt hit anything");
            }
        }
    }

    void SetActiveColor(string colorName)
    {
        GameObject selectedColor = System.Array.Find(colorObjects, obj => obj.name == colorName);

        if (selectedColor != null)
        {
            foreach (GameObject obj in colorObjects)
            {
                obj.SetActive(obj == selectedColor);
            }
        }
        if (colorName == clapButton.name)
        {
            isClapping = true;
            PlaySound();
            isClapping = false;
        }
        if (colorName == gaspButton.name)
        {
            isGasping = true;
            PlaySound();
            isGasping = false;
        }
        if (colorName == laughButton.name)
        {
            isLaughing = true;
            PlaySound();
            isLaughing = false;
        }
        else
        {
            print("invalid selection");
        }
    }

    public void PlaySound()
    {
        if (isClapping)
        {
            AudioSource.PlayClipAtPoint(clapSound, transform.position);
        }
        if (isGasping)
        {
            AudioSource.PlayClipAtPoint(gaspSound, transform.position);
        }
        if (isLaughing)
        {
            AudioSource.PlayClipAtPoint(laughSound, transform.position);
        }
    }
}
