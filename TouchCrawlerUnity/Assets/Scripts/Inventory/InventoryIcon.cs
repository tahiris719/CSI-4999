﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Item item;
    RectTransform rectTransform;
    private Vector2 lastMousePosition;
    private Vector3 finalWorldPosition;
    private Quaternion finalWorldRotation;
    public Vector3 basePosition;
    GameObject parent;
    GameObject itemObject;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        basePosition = rectTransform.localPosition;

        parent = gameObject.transform.parent.gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rectTransform.position + new Vector3(diff.x, diff.y, transform.position.z);
        Vector3 oldPosition = rectTransform.position;
        rectTransform.position = newPosition;

        finalWorldPosition = Camera.main.ScreenToWorldPoint(rectTransform.position);

        if (!IsRectTransformInsideSreen(rect))
        {
            rectTransform.position = oldPosition;
        }

        lastMousePosition = currentMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.localPosition = basePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        itemObject = parent.transform.GetChild(1).gameObject;

        GameObject test = Instantiate(itemObject, new Vector3(finalWorldPosition.x, finalWorldPosition.y, 0), new Quaternion(0, 0, 0, 0));

        if (results.Count == 0)
        {
            Assets.Scripts.Events.EventSystem.Broadcast(Assets.Scripts.Events.EventSystem.EventChannel.inventory, Assets.Scripts.Events.EventSystem.EventSubChannel.item, new DropItemEvent(item, parent));
        }
    }

    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        return true;
    }
}