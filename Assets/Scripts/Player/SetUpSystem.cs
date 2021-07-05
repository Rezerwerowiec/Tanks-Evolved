using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpSystem : MonoBehaviour
{
    public GameObject Hull = null, Tower = null, Gun = null, Tracks = null;
    public Transform HullTr = null, TowerTr = null, GunTr = null, TankPlayer = null;

    // Start is called before the first frame update
    void Start()
    {

        Hull = GAME_CONTROLLER.Hulls[GAME_CONTROLLER.CurHull];
        Gun = GAME_CONTROLLER.Guns[GAME_CONTROLLER.CurGun];
        Instantiate(Hull, HullTr);
        Instantiate(Tower, TowerTr);
        Instantiate(Gun, GunTr);
        Instantiate(Tracks, TankPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
