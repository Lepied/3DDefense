using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuilding : MonoBehaviour
{
    public BuildingManager buildingManager;

    private Renderer previewRenderer;
    private bool isColliding = false; // �浹 ���� ����

    private void Awake()
    {
        previewRenderer = GetComponentInChildren<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !isColliding)
        {
            Debug.Log("�浹����: " + other.gameObject.name); // �浹�� ������Ʈ�� �̸� ���
            isColliding = true;
            previewRenderer.material.color = buildingManager.color;
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && isColliding)
        {
            Debug.Log("�浹��: " + other.gameObject.name); // �浹 ������ ������Ʈ�� �̸� ���
            isColliding = false;
            previewRenderer.material.color = buildingManager.originalColor;
            buildingManager.canPlace = true;
        }
    }
}