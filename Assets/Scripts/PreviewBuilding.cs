using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuilding : MonoBehaviour
{
    public BuildingManager buildingManager;

    private Renderer previewRenderer;
    private bool isColliding = false; // 충돌 상태 추적

    private void Awake()
    {
        previewRenderer = GetComponentInChildren<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !isColliding)
        {
            Debug.Log("충돌시작: " + other.gameObject.name); // 충돌한 오브젝트의 이름 출력
            isColliding = true;
            previewRenderer.material.color = buildingManager.color;
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && isColliding)
        {
            Debug.Log("충돌끝: " + other.gameObject.name); // 충돌 종료한 오브젝트의 이름 출력
            isColliding = false;
            previewRenderer.material.color = buildingManager.originalColor;
            buildingManager.canPlace = true;
        }
    }
}