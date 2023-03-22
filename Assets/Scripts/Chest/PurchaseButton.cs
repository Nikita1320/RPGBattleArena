using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PurchaseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image imageResource;
    [SerializeField] private Text priceText;
    [SerializeField] private Button button;
    private Resource resource;
    private int price;
    public Action onPointerEnter;
    public Action onPointerExit;
    public Button Button => button;

    public void Init(Resource resource, int price)
    {
        this.resource = resource;
        this.price = price;
        imageResource.sprite = resource.ResourceSprite;
        priceText.text = price.ToString();

        Checking«ossibilityPurchase();
        resource.changeAmmountEvent += Checking«ossibilityPurchase;
    }

    private void Checking«ossibilityPurchase()
    {
        button.interactable = (resource.Ammount >= price);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke();
    }
    private void OnDestroy()
    {
        resource.changeAmmountEvent -= Checking«ossibilityPurchase;
    }
}
