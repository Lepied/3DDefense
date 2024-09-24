using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    public GameObject previewPrefab;
    public GameObject buildingPrefab;
    private GameObject currentPreview;

    public LayerMask groundMask;
    public LayerMask NCZMask; // No Construction Zone
    
    private bool isBuilding = false;
    public bool canPlace = true;



    public Color color = new Color(1, 0, 0, 0.5f); // 빨간색 + 알파값 0.5
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
                PlaceBuilding();
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
            //currentPreview.AddComponent<PreviewBuilding>().buildingManager = this; // PreviewBuilding 스크립트 추가 및 참조 설정
            //currentPreview의 자식 오브젝트에 PreviewBuilding 추가
            PreviewBuilding previewBuilding = currentPreview.GetComponentInChildren<Collider>().gameObject.AddComponent<PreviewBuilding>();
            previewBuilding.buildingManager = this; // BuildingManager를 할당


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


        // 먼저 Ground에 충돌했는지 체크
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            currentPreview.transform.position = hit.point;   
            
        }
    }

    void PlaceBuilding()
    {
        Instantiate(buildingPrefab, currentPreview.transform.position, Quaternion.identity);
    }
}
