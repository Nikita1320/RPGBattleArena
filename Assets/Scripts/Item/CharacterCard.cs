using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Removing();
public delegate void ChangedAmmount();
[System.Serializable]
public class CharacterCard
{
    [SerializeField] private CharacterCardData cardData;
    [SerializeField] private int ammount;
    private Removing removingEvent;
    private ChangedAmmount changedAmmountEvent;

    public Removing RemovingEvent { get { return removingEvent; } set { if (value != null) { removingEvent += value; } } }
    public ChangedAmmount ChangedAmmountEvent { get { return changedAmmountEvent; } set { if (value != null) { changedAmmountEvent += value; } } }
    public CharacterCardData CardData => cardData;
    public int Ammount => ammount;

    public CharacterCard(CharacterCardData cardData)
    {
        this.cardData = cardData;
    }
    public CharacterCard(CharacterCardData cardData, int ammount)
    {
        this.cardData = cardData;
        this.ammount = ammount;
    }
    public void ChangeAmmount(int value)
    {
        ammount += value;
        ChangedAmmountEvent?.Invoke();
        if (ammount == 0)
        {
            removingEvent?.Invoke();
        }
    }
}
