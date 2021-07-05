using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    public string Name;
    [Range(20, 1000)]
    public int Damage, Penetration;
    [Range(0.5f, 30f)]
    public float ReloadTime;
    
}
