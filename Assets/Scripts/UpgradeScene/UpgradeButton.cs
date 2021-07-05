using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    Text textButton;
    int cost;
    void Start()
    {
        textButton = GetComponentInChildren<Text>();
        if (GAME_CONTROLLER.Guns.Length - 1 == GAME_CONTROLLER.GunsLevels) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (GAME_CONTROLLER.Guns.Length-1 == GAME_CONTROLLER.GunsLevels) Destroy(gameObject);
        textButton.text = "UNLOCK GUN FOR \n" + GAME_CONTROLLER.expCostGun[GAME_CONTROLLER.GunsLevels];
        if (GAME_CONTROLLER.ExperiencePoints < GAME_CONTROLLER.expCostGun[GAME_CONTROLLER.GunsLevels])
        {
            GetComponent<Image>().color = Color.red;
            textButton.color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.green;
            textButton.color = Color.blue;
        }
    }

    public void UpgradeGuns()
    {
        if (GAME_CONTROLLER.ExperiencePoints < GAME_CONTROLLER.expCostGun[GAME_CONTROLLER.GunsLevels])
        {     
            return;
        }
        GAME_CONTROLLER.ExperiencePoints -= GAME_CONTROLLER.expCostGun[GAME_CONTROLLER.GunsLevels];
        GAME_CONTROLLER.GunsLevels++;
        if (GAME_CONTROLLER.Guns.Length - 1 == GAME_CONTROLLER.GunsLevels) Destroy(gameObject);
    }
}
