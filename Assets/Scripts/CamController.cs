using System;
using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private GridCardController gridCardController;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            
            Ray ray = SpatialBridge.cameraService.ScreenPointToRay(mousePos);
            
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Debug.Log($"Hit {hit.collider.gameObject.name} at {hit.point}");
                if (hit.collider.gameObject.tag == "Card")
                {
                    if (gridCardController)
                    {
                        gridCardController.CardSelected(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
