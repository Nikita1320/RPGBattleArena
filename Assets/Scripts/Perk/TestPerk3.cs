using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPerk3 : ItemPerk
{
    [SerializeField] private float[] values = { 1, 2, 3, 4, 5 };
    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override float[] GetValues()
    {
        return values;
    }
}
