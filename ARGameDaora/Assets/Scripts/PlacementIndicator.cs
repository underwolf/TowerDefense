﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    public ARPlaneManager planeManager;
    private GameObject visual;
    bool canPlace=true;

    private void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        visual.SetActive(false);
    }

    private void Update()
    {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;

                if (!visual.activeInHierarchy)
                {
                    visual.SetActive(true);
                }
            }
    }

    public void hidePlacement()
    {
        visual.SetActive(false);
        planeManager.enabled = false;
        foreach(var planes in planeManager.trackables)
        {
            planes.gameObject.SetActive(false);
        }
    }
}
