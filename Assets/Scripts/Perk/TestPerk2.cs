using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPerk2 : ItemPerk
{
    [SerializeField] private float[] values = { 1.5f, 3, 4.5f, 6, 7.5f};
    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override float[] GetValues()
    {
        return values;
    }
}
