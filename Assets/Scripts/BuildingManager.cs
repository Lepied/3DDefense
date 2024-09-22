using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject previewPrefab;
    public GameObject buildingPrefab;
    public LayerMask groundMask;
    private GameObject currentPreview;
    private bool isBuilding = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildingMode();
        }

        if (isBuilding)
        {
            UpdatePreviewPostition();

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }
        }
    }

    void ToggleBuildingMode() 
    {
        isBuilding = !isBuilding;
        if(isBuilding)
        {
            currentPreview = Instantiate(previewPrefab);
        }
        else
        {
            Destroy(currentPreview);
            
        }
    }

    void UpdatePreviewPostition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            currentPreview.transform.position = hit.point;
        }
    }

    void PlaceBuilding()
    {
        Instantiate(buildingPrefab, 
            currentPreview.transform.position, Quaternion.identity);
    }
}
