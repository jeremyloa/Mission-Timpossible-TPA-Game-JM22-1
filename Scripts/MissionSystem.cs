using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSystem : MonoBehaviour
{
    [SerializeField] GameObject BotLeftDialog;
    [SerializeField] TMPro.TMP_Text BotLeftDialogText;
    [SerializeField] GameObject BotCentInteract;
    [SerializeField] TMPro.TMP_Text BotCentInteractText;
    [SerializeField] TMPro.TMP_Text TopLeftMissionText;
    MainCharScript MainChar;
    MainCharAim aim;
    [SerializeField] LayerMask pistolMask, pistolAmmoMask, sniperMask, sniperAmmoMask;
    Ray ray; RaycastHit hit;
    PistolAmmo pistolAmmo;
    SniperAmmo sniperAmmo;
    SniperEquip sniperEquip;
    [SerializeField] GameObject TunnelDoorLeft, TunnelDoorRight;
    bool RotateTunnelDoor;
    // Start is called before the first frame update
    void Start()
    {
        sniperEquip = GetComponent<SniperEquip>();
        aim = GetComponentInParent<MainCharAim>();
        MainChar = GetComponent<MainCharScript>();
        pistolAmmo = GetComponent<PistolAmmo>();
        sniperAmmo = GetComponent<SniperAmmo>();
        BotLeftDialog.SetActive(false);
        BotCentInteract.SetActive(false);
        RotateTunnelDoor = false;
    }

    void Update()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);
        BotCentMain();
        TopLeftMain();
        BotLeftMain();

        if (RotateTunnelDoor == true)
        {
            if (TunnelDoorLeft.transform.rotation.eulerAngles.y>180)
            TunnelDoorLeft.transform.Rotate
                (new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - (Time.deltaTime * 2), transform.rotation.eulerAngles.z));
            if (TunnelDoorRight.transform.rotation.eulerAngles.y < 360)
                TunnelDoorRight.transform.Rotate
                    (new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (Time.deltaTime * 2), transform.rotation.eulerAngles.z));
            if (TunnelDoorLeft.transform.rotation.eulerAngles.y == 180 && TunnelDoorRight.transform.rotation.eulerAngles.y == 360) RotateTunnelDoor = false;
        }
    }

    //BotCentInteract
    void BotCentMain()
    {
        //interact with asuna
        if (MainChar.checkNearAsuna == true)
        {
            BotCentInteractText.SetText("Press F to interact with Asuna.");
            BotCentInteract.SetActive(true);
            return;
        }

        //pickup pistol ammo
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, pistolAmmoMask))
        {
            BotCentInteractText.SetText("Press F to pick up PISTOL AMMO.");
            BotCentInteract.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                pistolAmmo.extraAmmo = pistolAmmo.extraAmmo + pistolAmmo.baseAmmo;
                hit.transform.gameObject.SetActive(false);
            }
            return;
        }

        //pickup rifle ammo
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, sniperAmmoMask))
        {
            BotCentInteractText.SetText("Press F to pick up RIFLE AMMO.");
            BotCentInteract.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                sniperAmmo.extraAmmo = sniperAmmo.extraAmmo + sniperAmmo.baseAmmo;
                hit.transform.gameObject.SetActive(false);
            }
            return;
        }

        //pickup pistol
        if (Physics.Raycast(ray, out hit, 10, pistolMask) || aim.hit.transform.name.Equals("PistolTAKE"))
        {
            BotCentInteractText.SetText("Press F to pick up PISTOL.");
            BotCentInteract.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                PistolEquip.sendEquip = true;
            }
            return;
        }

        //pickup rifle
        if ((Physics.Raycast(ray, out hit, 10, sniperMask) || aim.hit.transform.name.Equals("SniperTAKE")) && MainChar.pistolAvail == true)
        {
            BotCentInteractText.SetText("Press F to pick up RIFLE.");
            BotCentInteract.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                PistolEquip.sendEquip = true;
                MainChar.sniperAvail = true;
                sniperEquip.SniperTable.SetActive(false);
                sniperEquip.Sniper.SetActive(true);
            }
            return;
        }

        
        BotCentInteract.SetActive(false);
        return;
    }

    //BotLeftDialog
    void BotLeftMain()
    {
        //mission one
        if (MainCharScript.missionOne == -1)
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("Talk to Asuna");
            BotLeftDialog.SetActive(true);
            MainCharScript.missionOne = 0;
        }
        else if (MainCharScript.missionOne == 0 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            MainCharScript.missionOne = 1;
            //Time.timeScale = 0;
            //BotLeftDialogText.SetText("You haven't finish your mission.");
            //BotLeftDialog.SetActive(true);
        }
        //mission two
        else if (MainCharScript.missionTwo == -1 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("Go pick up a pistol and shart shooting!");
            BotLeftDialog.SetActive(true);
            MainCharScript.missionTwo = 0;
        }
        else if (MainCharScript.missionTwo == 0 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("You haven't finish your mission. Go pick up a pistol and shart shooting!");
            BotLeftDialog.SetActive(true);
        }
        //mission three
        else if (MainCharScript.missionThree == -1 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("Come back when you have mastered the art of shooting.");
            BotLeftDialog.SetActive(true);
            MainCharScript.missionThree = 0;
        }
        else if (MainCharScript.missionThree == 0 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("You haven't finish your mission. Come back when you have mastered the art of shooting.");
            BotLeftDialog.SetActive(true);
        }
        //mission four
        else if (MainCharScript.missionFour == -1 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("Good. Now, shoot 50 bullets using the rifle.");
            BotLeftDialog.SetActive(true);
            MainCharScript.missionFour = 0;
        }
        else if (MainCharScript.missionFour == 0 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("You haven't finish your mission. Shoot 50 bullets using the rifle.");
            BotLeftDialog.SetActive(true);
        }
        //mission five
        else if (MainCharScript.missionFive == -1 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("Seems like you're ready for war. Now go through the passage and eliminate the enemies!");
            BotLeftDialog.SetActive(true);
            RotateTunnelDoor = true;
            MainCharScript.missionFive = 0;
        }
        else if (MainCharScript.missionFive == 0 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("You haven't finish your mission. Eliminate 16 enemies!");
            BotLeftDialog.SetActive(true);
        }
        //mission six
        else if (MainCharScript.missionSix == -1 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("Great job! Now we only need to eliminate the boss!");
            BotLeftDialog.SetActive(true);
            //rotate teleport door
            MainCharScript.missionSix = 0;
        }
        else if (MainCharScript.missionSix == 0 && MainChar.checkNearAsuna == true && Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 0;
            BotLeftDialogText.SetText("You haven't finish your mission. Eliminate the boss!");
            BotLeftDialog.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && BotLeftDialog.activeSelf == true)
        {
            BotLeftDialog.SetActive(false);
            Time.timeScale = 1;
        }
    }

    //TopLeftMission
    void TopLeftMain()
    {
        if (MainCharScript.missionOne < 1)
        {
            TopLeftMissionText.SetText("Find 'Asuna' and Talk to Her!");
            TopLeftMissionText.color = Color.yellow;
        }
        else if (MainCharScript.missionTwo == 0)
        {
            TopLeftMissionText.SetText("Pick Up The Pistol");
            TopLeftMissionText.color = Color.yellow;
        }
        else if (MainCharScript.missionThree == 0)
        {
            TopLeftMissionText.SetText("Shoot 10 Rounds at the shooting target! (" + MainCharScript.missionThreeScore + "/10)");
            Debug.Log("Mission Three Score = " + MainCharScript.missionThreeScore);
            TopLeftMissionText.color = Color.yellow;
            if (MainCharScript.missionThreeScore == 10)
            {
                TopLeftMissionText.SetText("Shoot 10 Rounds at the shooting target! (10/10) [DONE]");
                TopLeftMissionText.color = Color.green;
                MainCharScript.missionThree = 1;
            }
        }
        else if (MainCharScript.missionFour == 0)
        {
            TopLeftMissionText.SetText("Shoot 50 Bullets with the rifle! (" + MainCharScript.missionFourScore + "/50)");
            TopLeftMissionText.color = Color.yellow;
            if (MainCharScript.missionFourScore == 50 || MainCharScript.missionFour == 1)
            {
                TopLeftMissionText.SetText("Shoot 50 Bullets with the rifle! (50/50) [DONE]");
                TopLeftMissionText.color = Color.green;
                MainCharScript.missionFour = 1;
            }
        }
        else if (MainCharScript.missionFive == 0)
        {
            TopLeftMissionText.SetText("Eliminate the soldiers that are attacking the village! (" + MainCharScript.missionFiveScore + "/16)");
            TopLeftMissionText.color = Color.yellow;
            if (MainCharScript.missionFiveScore == 16 || MainCharScript.missionFive == 1)
            {
                TopLeftMissionText.SetText("Eliminate the soldiers that are attacking the village! (16/16) [DONE]");
                TopLeftMissionText.color = Color.green;
                MainCharScript.missionFive = 1;
            }
        }
        else if (MainCharScript.missionSix == 0)
        {
            TopLeftMissionText.SetText("Head to the secret teleport room and defeat the boss");
            TopLeftMissionText.color = Color.yellow;
        }

    }
}
