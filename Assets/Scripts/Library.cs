using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    public GameObject[] Guns, Hulls, Towers;

    public int[] expCostGun;

    public GameObject DamageHUD;
    private void Start()
    {
        GAME_CONTROLLER.Guns = Guns;
        GAME_CONTROLLER.Hulls = Hulls;
        GAME_CONTROLLER.expCostGun = expCostGun;
        GAME_CONTROLLER.DamageHUD = DamageHUD;
    }
}
