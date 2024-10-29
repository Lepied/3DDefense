using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager : NetworkBehaviour
{
    public GameObject previewPrefab;
    public GameObject buildingPrefab;
    private GameObject currentPreview;

    public LayerMask groundMask;
    public LayerMask NCZMask; // No Construction Zone
    
    private bool isBuilding = false;
    public bool canPlace = true;



    public Color color = new Color(1, 0, 0, 0.5f); // ������ + ���İ� 0.5
    public Color originalColor;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildingMode();
        }

        if (isBuilding)
        {
            UpdatePreviewPosition();

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceBuilding(currentPreview.transform.position);
            }
        }
        if (currentPreview == null)
        {
            return;
        }
    }

    void ToggleBuildingMode() 
    {
        isBuilding = !isBuilding;
        if(isBuilding)
        {
            currentPreview = Instantiate(previewPrefab);
            PreviewBuilding previewBuilding = currentPreview.GetComponentInChildren<Collider>().gameObject.AddComponent<PreviewBuilding>();
            previewBuilding.buildingManager = this; // BuildingManager�� �Ҵ�


            originalColor = currentPreview.GetComponentInChildren<Renderer>().material.color;
        }
        else
        {
            Destroy(currentPreview);
            
        }
    }

    void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        // ���� Ground�� �浹�ߴ��� üũ
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            currentPreview.transform.position = hit.point;   
            
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void PlaceBuilding(Vector3 position)
    {
        GameObject building = Instantiate(buildingPrefab,position,Quaternion.identity);
        Spawn(building);
    }
}
