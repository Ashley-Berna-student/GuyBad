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
        else
        {
            print("invalid selection");
        }
    }
}
