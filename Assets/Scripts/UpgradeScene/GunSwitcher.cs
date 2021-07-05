using UnityEngine;
using UnityEngine.UI;

public class GunSwitcher : MonoBehaviour
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
            if (GAME_CONTROLLER.CurGun < GAME_CONTROLLER.GunsLevels)
                GAME_CONTROLLER.CurGun++;


        }
        else if (GAME_CONTROLLER.CurGun > 0)
        {
            GAME_CONTROLLER.CurGun--;
        }
        Debug.Log(GAME_CONTROLLER.CurGun);

        Info();
    }
    private void Info()
    {
        GetComponent<Image>().sprite = GAME_CONTROLLER.Guns[GAME_CONTROLLER.CurGun].GetComponent<SpriteRenderer>().sprite;



        textField.text = "Gun: " + GAME_CONTROLLER.Guns[GAME_CONTROLLER.CurGun].GetComponent<GunStats>().Name +
                         "\nDamage per shoot: " + GAME_CONTROLLER.Guns[GAME_CONTROLLER.CurGun].GetComponent<GunStats>().Damage +
                         "\nReloading time: " + GAME_CONTROLLER.Guns[GAME_CONTROLLER.CurGun].GetComponent<GunStats>().ReloadTime;
    }
}
