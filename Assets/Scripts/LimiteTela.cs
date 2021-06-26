using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteTela : MonoBehaviour
{
    [Header("LIMITES X")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
     
    [Header("LIMITES Y")]
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    public float XMax
    {
        set => xMax = value;
        get => xMax;
    }
    public float Ymax
    {
        set => yMax = value;
        get => yMax;
    }

    public float XMin
    {
        set => xMin = value;
        get => xMin;
    }
    public float YMin
    {
        set => yMin = value;
        get => yMin;
    }
}
