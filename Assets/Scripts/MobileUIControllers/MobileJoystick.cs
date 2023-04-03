using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform controllerPanel;
    [SerializeField] private RectTransform stick;
    [SerializeField] private float maxDistance;
    private void Start()
    {
        maxDistance = controllerPanel.rect.width / 2;
    }

    public Vector2 GetAxis()
    {
        float x = stick.anchoredPosition.x / maxDistance;
        float y = stick.anchoredPosition.y / maxDistance;
        return new Vector2(x, y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
