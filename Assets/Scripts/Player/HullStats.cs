using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullStats : MonoBehaviour
{
    public string Name;
    [Range(1, 300)]
    public int HullAmour, HullWeight;
    [Range(100, 3000)]
    public int HullHP, EnginePower;
    [Range(10, 100)]
    public int Speed;
}
