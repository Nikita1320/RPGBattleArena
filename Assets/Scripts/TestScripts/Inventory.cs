using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void AddedEquipment(Equipment equipment);
public delegate void AddedCard(CharacterCard card);

public delegate void RemovedEquipment(Equipment equipment);
public delegate void RemovedCard(CharacterCard card);
public class Inventory : MonoBehaviour
{
    private static Inventory instance;

    public AddedEquipment AddedEquipmentEvent;
    public AddedCard AddedCardtEvent;
    public RemovedEquipment RemovedEquipmentEvent;
    public RemovedCard RemovedCardEvent;

    [SerializeField] private List<Equipment> equipments;
    [SerializeField] private List<CharacterCard> cards;

    public static Inventory Instance => instance;
    public Equipment[] Equipments => equipments.ToArray();
    public CharacterCard[] CharacterCards => cards.ToArray();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        for (int i = 0; i < equipments.Count; i++)
        {
            equipments[i].InitializeWithParametrs();
        }
    }
    public void AddItem(Equipment newEquipment)
    {
        equipments.Add(newEquipment);
        AddedEquipmentEvent?.Invoke(newEquipment);
    }
    public void AddItem(CharacterCard newCharacterCard)
    {
        if (FindCard(newCharacterCard.CardData, out CharacterCard characterCard))
        {
            characterCard.ChangeAmmount(newCharacterCard.Ammount);
        }
        else
        {
            cards.Add(newCharacterCard);
            AddedCardtEvent?.Invoke(newCharacterCard);
        }
    }
    public bool FindCard(CharacterCardData characterCardData, out CharacterCard characterCard)
    {
        characterCard = null;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].CardData == characterCardData)
            {
                characterCard = cards[i];
                return true;
            }
        }
        return false;
    }
    public void RemoveCardItem(CharacterCard characterCard)
    {
        RemovedCardEvent?.Invoke(characterCard);
        cards.Remove(characterCard);
    }
    public void RemoveEquipment(Equipment removeEquipment)
    {
        if (removeEquipment.Owner != null)
        {
            removeEquipment.Owner.RemoveEquipment(removeEquipment);
        }
        RemovedEquipmentEvent?.Invoke(removeEquipment);
        removeEquipment.removingEvent?.Invoke();
        equipments.Remove(removeEquipment);
    }
    public Equipment[] GetEquipmentWithRarity(RareEquipment rareEquipment)
    {
        List<Equipment> equipmentsWithRarity = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].EquipmentData.Rare == rareEquipment)
            {
                equipmentsWithRarity.Add(equipments[i]);
            }
        }
        return equipmentsWithRarity.ToArray();
    }
    public Equipment[] GetEquipmentWithType(TypeEquipment typeEquipment)
    {
        List<Equipment> equipmentsWithType = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].EquipmentData.EquipmentType == typeEquipment)
            {
                equipmentsWithType.Add(equipments[i]);
            }
        }
        return equipmentsWithType.ToArray();
    }
    public Equipment[] GetEquipmentWithType(TypeEquipment typeEquipment, List<Equipment> without)
    {
        List<Equipment> equipmentsWithType = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].EquipmentData.EquipmentType == typeEquipment)
            {
                if (without.Contains(equipments[i]) == false)
                {
                    equipmentsWithType.Add(equipments[i]);
                }
            }
        }
        return equipmentsWithType.ToArray();
    }
    public Equipment[] GetEquipments(List<Equipment> without)
    {
        List<Equipment> equipmentsWithType = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (without.Contains(equipments[i]) == false)
            {
                equipmentsWithType.Add(equipments[i]);
            }
        }
        return equipmentsWithType.ToArray();
    }
    public Equipment[] GetEquipments(TypeEquipment typeEquipment, RareEquipment rareEquipment)
    {
        List<Equipment> equipmentsWithType = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].EquipmentData.EquipmentType == typeEquipment && equipments[i].EquipmentData.Rare == rareEquipment)
            {
                equipmentsWithType.Add(equipments[i]);
            }
        }
        return equipmentsWithType.ToArray();
    }
    public Equipment[] GetEquipments(TypeEquipment typeEquipment, RareEquipment rareEquipment, List<Equipment> without)
    {
        List<Equipment> equipmentsWithType = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if ((equipments[i].EquipmentData.EquipmentType == typeEquipment) && (equipments[i].EquipmentData.Rare == rareEquipment) && (without.Contains(equipments[i]) == false))
            {
                equipmentsWithType.Add(equipments[i]);
            }
        }
        return equipmentsWithType.ToArray();
    }
    public Equipment[] GetEquipments(TypeEquipment typeEquipment, RareEquipment rareEquipment, Equipment without)
    {
        List<Equipment> equipmentsWithType = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if ((equipments[i].EquipmentData.EquipmentType == typeEquipment) && (equipments[i].EquipmentData.Rare == rareEquipment) && (without != equipments[i]))
            {
                equipmentsWithType.Add(equipments[i]);
            }
        }
        return equipmentsWithType.ToArray();
    }
    public Equipment[] GetAllSameEquipment(EquipmentData equipmentData)
    {
        List<Equipment> sameEquipments = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].EquipmentData == equipmentData)
            {
                sameEquipments.Add(equipments[i]);
            }
        }
        return sameEquipments.ToArray();
    }
    public Equipment[] GetAllSameEquipment(EquipmentData equipmentData, List<Equipment> without)
    {
        List<Equipment> sameEquipments = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].EquipmentData == equipmentData && without.Contains(equipments[i]) == false)
            {
                sameEquipments.Add(equipments[i]);
            }
        }
        return sameEquipments.ToArray();
    }
    public Equipment[] GetAllSameEquipment(EquipmentData equipmentData, Equipment without)
    {
        List<Equipment> sameEquipments = new();
        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].EquipmentData == equipmentData && without != equipments[i])
            {
                sameEquipments.Add(equipments[i]);
            }
        }
        return sameEquipments.ToArray();
    }
}
