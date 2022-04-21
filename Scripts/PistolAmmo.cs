using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmo : MonoBehaviour
{
    public int baseAmmo, extraAmmo, currentAmmo;
    [SerializeField] LayerMask pistolAmmoMask;
    GameObject AmmoPistol;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = baseAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);
        //if (Input.GetKeyDown(KeyCode.F) && Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, pistolAmmoMask))
        //{
        //    extraAmmo = extraAmmo + baseAmmo;
        //    AmmoPistol = hit.transform.gameObject;
        //    AmmoPistol.SetActive(false);
        //}
        if (currentAmmo==0) Reload();
    }

    public void Reload() {
        
        if (extraAmmo>=baseAmmo)
        {
            //reload sound & animation
            currentAmmo = currentAmmo + baseAmmo;
            extraAmmo = extraAmmo - baseAmmo;
        }
        //kalau pencet r reload
        
    }
}
