using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SniperEquip : MonoBehaviour
{
    public static bool sendEquip;
    [SerializeField] public GameObject Sniper;
    [SerializeField] public Rig SniperRig;
    [SerializeField] public GameObject SniperTable;

    [SerializeField] LayerMask sniperMask;
    Ray ray;
    bool animate, reverseanimate, firstTime, takeSniper;
    PistolEquip pistol;
    MainCharScript MainChar;
    [SerializeField] AudioClip shotSFX;
    AudioSource src;
    MainCharAim aim;
    // Start is called before the first frame update
    void Start()
    {
        sendEquip = false;
        aim = GetComponentInParent<MainCharAim>();
        src = GetComponent<AudioSource>();
        MainChar = GetComponent<MainCharScript>();
        pistol = GetComponent<PistolEquip>();
        animate = false;
        reverseanimate = false;
        firstTime = false;
        Sniper.SetActive(false);
        SniperRig.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);

        if (sendEquip == true)
        {
            //SniperTable.SetActive(false);
            //Sniper.SetActive(true);
            animate = true;
            reverseanimate = true;
            firstTime = true;
            src.PlayOneShot(shotSFX);
            sendEquip = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && MainChar.sniperAvail == true)
        {
            Sniper.SetActive(true);
            animate = true;
            reverseanimate = true;
            takeSniper = true;
            src.PlayOneShot(shotSFX);
        }

        if (firstTime == true && reverseanimate == true && pistol.PistolRig.weight > 0 && MainChar.pistolOn == true)
        {
            pistol.PistolRig.weight = pistol.PistolRig.weight - (Time.deltaTime * 2);
            if (pistol.PistolRig.weight == 0)
            {
                reverseanimate = false; pistol.Pistol.SetActive(false); MainChar.pistolOn = false; 
            }
        }
        else if (firstTime == true && animate == true && SniperRig.weight < 1 && MainChar.sniperOn == false)
        {
            SniperRig.weight = SniperRig.weight + (Time.deltaTime * 2);
            if (SniperRig.weight == 1)
            {
                animate = false; MainChar.sniperOn = true; firstTime = false; 
            }
        }
        else if (MainChar.checkNearAsuna == true && SniperRig.weight > 0 && MainChar.sniperOn == true)
        {
            SniperRig.weight = SniperRig.weight - (Time.deltaTime * 2);
            if (SniperRig.weight == 0)
            {
                Sniper.SetActive(false); MainChar.sniperOn = false; MainChar.checkNearAsuna = false;
            }
        }
        else if (takeSniper == true && reverseanimate == true && pistol.PistolRig.weight > 0 && MainChar.pistolOn == true)
        {
            pistol.PistolRig.weight = pistol.PistolRig.weight - (Time.deltaTime * 2);
            if (pistol.PistolRig.weight == 0)
            {
                reverseanimate = false; pistol.Pistol.SetActive(false); MainChar.pistolOn = false;
            }
        }
        else if (takeSniper == true && animate == true && SniperRig.weight < 1 && MainChar.sniperOn == false)
        {
            SniperRig.weight = SniperRig.weight + (Time.deltaTime * 2);
            if (SniperRig.weight == 1)
            {
                animate = false; MainChar.sniperOn = true; firstTime = false; takeSniper = false;
            }
        }
    }
}
