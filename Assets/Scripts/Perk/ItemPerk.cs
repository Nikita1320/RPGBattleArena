using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPerk : MonoBehaviour
{
    [SerializeField] protected GameObject characterGameobject;
    [SerializeField] protected int levelUpgrade;
    public abstract float[] GetValues();

    public abstract void Activate();
    public void Init(int level)
    {
        levelUpgrade = level;
    }
}
