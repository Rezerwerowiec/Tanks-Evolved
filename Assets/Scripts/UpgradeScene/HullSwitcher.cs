using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HullSwitcher : MonoBehaviour
{

    [SerializeField]
    private Text textField = null;

    private void Start()
    {
        Info();
    }
    public void OnSwitcherClick(bool inRight)
    {
        if (inRight)
        {
            if (GAME_CONTROLLER.CurHull < GAME_CONTROLLER.HullLevels)
                GAME_CONTROLLER.CurHull++;

        }
        else if (GAME_CONTROLLER.CurHull > 0)
        {
            GAME_CONTROLLER.CurHull--;
        }
        Debug.Log(GAME_CONTROLLER.CurHull);

        Info();
    }
    
    private void Info()
    {
        GetComponent<Image>().sprite = GAME_CONTROLLER.Hulls[GAME_CONTROLLER.CurHull].GetComponent<SpriteRenderer>().sprite;



        textField.text = "Hull: " + GAME_CONTROLLER.Hulls[GAME_CONTROLLER.CurHull].GetComponent<HullStats>().Name +
                         "\nHit points: " + GAME_CONTROLLER.Hulls[GAME_CONTROLLER.CurHull].GetComponent<HullStats>().HullHP +
                         "\nArmour: " + GAME_CONTROLLER.Hulls[GAME_CONTROLLER.CurHull].GetComponent<HullStats>().HullAmour +
                         "\nHorse Power: " + GAME_CONTROLLER.Hulls[GAME_CONTROLLER.CurHull].GetComponent<HullStats>().EnginePower;
    }
}
