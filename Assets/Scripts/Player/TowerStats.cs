using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [Range(100, 2000)]
    public int TowerHP;
    [Range(1, 300)]
    public int TowerArmour, TowerWeight;
}
