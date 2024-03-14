using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class MapSizeData : ScriptableObject
{
    [SerializeField]
    private Vector2 Limit_Min;

    [SerializeField]
    Vector2 Limit_Max;


    public Vector2 LimitMin
    {
        get { return Limit_Min; }
    }

    public Vector2 LimitMax
    {
        get { return Limit_Max; }
    }
}
