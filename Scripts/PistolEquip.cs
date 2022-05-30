using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PistolEquip : MonoBehaviour
{
    public static bool sendEquip;
    [SerializeField] public GameObject Pistol;
    [SerializeField] public Rig PistolRig;
    [SerializeField] GameObject PistolTable;

    [SerializeField] LayerMask pistolMask;
    Ray ray;
    bool animate, reverseanimate, firstTime, takePistol;
    SniperEquip sniper;
    MainCharScript MainChar;
    [SerializeField] AudioClip shotSFX;
    AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        sendEquip = false;
        src = GetComponent<AudioSource>();
        MainChar = GetComponent<MainCharScript>();
        sniper = GetComponent<SniperEquip>();
        animate = false;
        reverseanimate = false;
        firstTime = false;
        Pistol.SetActive(false);
        PistolRig.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);

        if (sendEquip == true)
        {
            MainChar.pistolAvail = true;
            PistolTable.SetActive(false);
            Pistol.SetActive(true);
            animate = true;
            reverseanimate = true;
            firstTime = true;
            sendEquip = false;
            src.PlayOneShot(shotSFX);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && MainChar.pistolAvail == true)
        {
            Pistol.SetActive(true);
            animate = true;
            reverseanimate = true;
            takePistol = true;
            src.PlayOneShot(shotSFX);
        }

        if (firstTime == true && reverseanimate == true && sniper.SniperRig.weight > 0 && MainChar.sniperOn == true)
        {
            sniper.SniperRig.weight = sniper.SniperRig.weight - (Time.deltaTime * 2);
            if (sniper.SniperRig.weight == 0)
            {
                reverseanimate = false; sniper.Sniper.SetActive(false); MainChar.sniperOn = false;
            }
        }
            
        else if (firstTime == true && animate == true && PistolRig.weight < 1 && MainChar.pistolOn == false)
        {
            PistolRig.weight = PistolRig.weight + (Time.deltaTime * 2);
            if (PistolRig.weight == 1)
            {
                animate = false; MainChar.pistolOn = true; firstTime = false; MainCharScript.missionTwo = 1; 
            }
        }
        
        else if (MainChar.checkNearAsuna == true && PistolRig.weight>0 && MainChar.pistolOn == true)
        {
            PistolRig.weight = PistolRig.weight - (Time.deltaTime * 2);
            if (PistolRig.weight == 0)
            {
                Pistol.SetActive(false); MainChar.pistolOn = false; MainChar.checkNearAsuna = false;
            }
        }

        else if (takePistol == true && reverseanimate == true && sniper.SniperRig.weight > 0 && MainChar.sniperOn == true)
        {
            sniper.SniperRig.weight = sniper.SniperRig.weight - (Time.deltaTime * 2);
            if (sniper.SniperRig.weight == 0)
            {
                reverseanimate = false; sniper.Sniper.SetActive(false); MainChar.sniperOn = false;
            }
        }

        else if (takePistol == true && animate == true && PistolRig.weight < 1 && MainChar.pistolOn == false)
        {
            PistolRig.weight = PistolRig.weight + (Time.deltaTime * 2);
            if (PistolRig.weight == 1)
            {
                animate = false; MainChar.pistolOn = true; firstTime = false;
            }
        }
    }
}
