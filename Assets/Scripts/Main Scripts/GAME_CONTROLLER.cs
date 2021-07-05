using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME_CONTROLLER : MonoBehaviour
{
    public static GAME_CONTROLLER GC;

    public static bool SavingSystem = false;

    public static GameObject DamageHUD;
    
    public static int GunsLevels, TowerLevels, HullLevels, TracksLevels, ExperiencePoints;

    public static int[] expCostGun;
    public static int CurHull = 0, CurTower = 0, CurGun = 0, CurTracks = 0;
    public static GameObject[] Guns, Hulls, Towers;

    public static int GameScore = 0, GameScoreEarned = 0;
    static SaveData sd = null;
    static SaveController sc = null;
    private void Start()
    {
        if(SavingSystem)
            InitialLoadSavingSystem();
    }

    void InitialLoadSavingSystem()
    {
        InvokeRepeating("Save", 5.0f, 5f);

        sc = GetComponent<SaveController>();
        sd = sc.LoadGame();
        if (sd == null)
        {
            sd = new SaveData
            {
                SGunsLevels = GunsLevels,
                SHullLevels = HullLevels,
                STowerLevels = TowerLevels,
                STracksLevels = TracksLevels,
                SExperiencePoints = ExperiencePoints,
                SCurGun = CurGun,
                SCurHull = CurHull,
                SCurTower = CurTower
            };
            Debug.Log("NEW DATA CREATED");
        }
        else
        {
            GunsLevels = sd.SGunsLevels;
            HullLevels = sd.SHullLevels;
            TowerLevels = sd.STowerLevels;
            TracksLevels = sd.STracksLevels;
            ExperiencePoints = sd.SExperiencePoints;
            CurGun = sd.SCurGun;
            CurHull = sd.SCurHull;
            CurTower = sd.SCurTower;

            Debug.Log("LOADED SAVED DATA");
        }

        sc.SaveGame(sd);
        //CalculateStats();
    }


    private void Awake()
    {
        if (GC != null)
            GameObject.Destroy(GC);
        else
            GC = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            
        }
    }

    public void Save()
    {
        sc.DeleteGame();
        sd.SCurGun = CurGun;
        sd.SCurHull = CurHull;
        sd.SCurTower = CurTower;
        sd.SGunsLevels = GunsLevels;
        sd.SHullLevels = HullLevels;
        sd.STowerLevels = TowerLevels;
        sd.STracksLevels = TracksLevels;
        sd.SExperiencePoints = ExperiencePoints;


        sc.SaveGame(sd);
        Debug.Log("AUTOMATICALLY SAVED DATA...");
    }


    public static int CalculateDamageOnHit(int pen, int dmg, int armour)
    {
        pen = (int)Random.Range(0.8f * pen, 1.2f * pen);
        Debug.Log("GC:CalculateDamageOnHit, Pen: " + pen);

        if(pen >= armour)
        {

            float damage = dmg;
            if(pen-armour > armour)
                 damage = (float)dmg * (float)(((float)pen - (float)armour) / 300 + 1);
            return (int) damage;
        }
        else
        {
            if (Random.Range(armour-pen, (armour-pen)*2+100) > 100) return -1;
            else
            {
                float damage = dmg * Mathf.Pow(pen, 2) / Mathf.Pow(armour, 2);
                return (int) damage;
            }
        }
    }
}


