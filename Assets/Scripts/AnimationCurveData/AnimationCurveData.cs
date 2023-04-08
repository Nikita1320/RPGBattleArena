using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newAnimationCurve", menuName = "AnimationCurve")]
public class AnimationCurveData : ScriptableObject
{
    [Tooltip("CurveWithTwoKey")]
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float maxTime;
    [SerializeField] private float minTime;
    [SerializeField] private float maxValue;
    [SerializeField] private float minValue;

    public AnimationCurve GetCurve => animationCurve;
    private void ChangeCurveByValue()
    {
        Keyframe[] newKey = new Keyframe[]
        {
            new Keyframe(minTime,minValue),
            new Keyframe(maxTime,maxValue)
        };
        float[,] tangent = new float[,]
        {
            {animationCurve.keys[0].inTangent, animationCurve.keys[0].outTangent },
            {animationCurve.keys[1].inTangent, animationCurve.keys[1].outTangent },
        };
        newKey[0].inTangent = tangent[0, 0];
        newKey[0].outTangent = tangent[0, 1];
        newKey[1].inTangent = tangent[1, 0];
        newKey[1].outTangent = tangent[1, 1];
        newKey[0].outWeight = animationCurve.keys[0].outWeight;
        newKey[1].inWeight = animationCurve.keys[1].inWeight;
        newKey[1].outWeight = animationCurve.keys[1].outWeight;
        newKey[0].inWeight = animationCurve.keys[0].inWeight;
        newKey[0].weightedMode = animationCurve.keys[0].weightedMode;
        newKey[1].weightedMode = animationCurve.keys[1].weightedMode;

        animationCurve.keys = newKey;
    }
    private void OnValidate()
    {
        Debug.Log("OnValidateCurveSO");
        ChangeCurveByValue();
    }
}
