using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPerk : ItemPerk
{
    [SerializeField] private float[] values = { 3, 5, 7, 9, 11 };
    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override float[] GetValues()
    {
        return values;
    }
}
