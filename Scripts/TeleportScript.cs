using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TeleportScript : MonoBehaviour
{
    [SerializeField] MainCharAim aim;
    [SerializeField] int TotalSoldier = 8;
    public static int TeleportScore;
    public static float TeleportTimer;

    public static bool EnterPreTeleport;

    [SerializeField] GameObject show1, show2, showTimer;
    [SerializeField] TMPro.TMP_Text showTimertext;
    bool show1Check, show2Check;
    float Show1Timer, Show2Timer;

    public static bool ReadyTeleport;
    [SerializeField] GameObject MainChar;
    [SerializeField] GameObject CubeTele;

    [SerializeField] GameObject TeleDoorLeft, TeleDoorRight;
    // Start is called before the first frame update
    void Start()
    {
        TeleportScore = 0;
        TeleportTimer = 60;
        EnterPreTeleport = show1Check = show2Check = ReadyTeleport = false;
        Show1Timer = Show2Timer = 5;
        show1.SetActive(false);
        show2.SetActive(false);
        showTimer.SetActive(false);
        TeleDoorLeft.SetActive(true);
        TeleDoorRight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCharScript.missionFive==1) 
            if (aim.hit.transform.name.Equals("EnterPreTeleport")) EnterPreTeleport = true;
        if (EnterPreTeleport==true)
        {
            if (show1Check == false)
            {
                show1.SetActive(true);
                if (Show1Timer > 0) Show1Timer -= Time.deltaTime;
                else show1Check = true;
            }
            else
            {
                show1.SetActive(false);
                if (TeleportTimer > 0)
                {
                    TeleportTimer -= Time.deltaTime;
                    Debug.Log("teleport timer = " + TeleportTimer);
                    showTimer.SetActive(true);
                    showTimertext.SetText("00:" + Mathf.FloorToInt(TeleportTimer));
                    Debug.Log("teleport player shoot enemy = " + TeleportScore);
                }
                else
                {
                    showTimer.SetActive(false);
                    if (TeleportScore < TotalSoldier) MainCharScript.currHealth=0;
                    else
                    {
                        if (show2Check == false)
                        {
                            show2.SetActive(true);
                            if (Show2Timer > 0) Show2Timer -= Time.deltaTime;
                            else show2Check = true;
                        }
                        else
                        {
                            show2.SetActive(false);
                            ReadyTeleport = true;
                        }
                    }
                }
            }
        }

        if (aim.hit.transform.name.StartsWith("TeleDoor"))
        {
            TeleDoorLeft.SetActive(false);
            TeleDoorRight.SetActive(false);
            //change to boss sound 
        }
    }

    public void goTeleport()
    {
        Debug.Log("player teleport");
        MainChar.transform.position = CubeTele.transform.position;
    }
}
