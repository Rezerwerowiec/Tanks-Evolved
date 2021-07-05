using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ADMIN : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine("SetUp");

    }

    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
        //GAME_CONTROLLER.GunsLevels = GAME_CONTROLLER.Guns.Length-1;
        GAME_CONTROLLER.GunsLevels = 2;
        GAME_CONTROLLER.HullLevels = GAME_CONTROLLER.Hulls.Length-1;
        //GAME_CONTROLLER.TowerLevels = GAME_CONTROLLER.Towers.Length;
        GAME_CONTROLLER.ExperiencePoints = 100000;
        GAME_CONTROLLER.SavingSystem = false;
    }
}
