using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobbyUIHandler : MonoBehaviour
{
    public UnityEngine.Camera playerCamera;

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

                if(gameObject.name == "turnTeal")
                {
                    print("YOu are now teal");
                }
            }
            else
            {
                print("raycast didnt hit anything");
            }
        }
    }
}
