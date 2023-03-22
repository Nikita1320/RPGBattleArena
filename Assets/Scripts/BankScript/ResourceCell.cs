using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCell : MonoBehaviour
{
    private Resource resource;
    [SerializeField] private Image iconResource;
    [SerializeField] private Text ammountText;
    [SerializeField] private Text descriptionText;
    public void Init(Resource resource)
    {
        this.resource = resource;
        iconResource.sprite = resource.ResourceSprite;
        ammountText.text = resource.Ammount.ToString();
        if (descriptionText != null)
        {
            descriptionText.text = resource.DescriptionResource;
        }

        resource.changeAmmountEvent += DisplayAmmount;
    }
    private void DisplayAmmount()
    {
        ammountText.text = resource.Ammount.ToString();
    }
    private void OnDestroy()
    {
        resource.changeAmmountEvent -= DisplayAmmount;
    }
}
