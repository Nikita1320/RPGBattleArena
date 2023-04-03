using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileController : MonoBehaviour
{
    [SerializeField] private RectTransform joystick;
    [SerializeField] private Button[] abilityButon;
    [SerializeField] private Button[] attackButon;

    public Vector2 GetAxis()
    {
        return Vector2.zero;
    }
}
