using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MainCharAim : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera vcam;
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform followPos;
    [SerializeField] public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] public LayerMask aimMask;
    Ray ray;
    public RaycastHit hit;
    public bool aimZoom;
    float baseFieldofView;
    [SerializeField] int zoomValue, zoomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        aimZoom = false;
        baseFieldofView = vcam.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            aimZoom = true;
            if (vcam.m_Lens.FieldOfView > baseFieldofView - zoomValue)
                vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView - (Time.deltaTime * zoomSpeed);
        }
        else aimZoom = false;

        if (aimZoom==false && vcam.m_Lens.FieldOfView < baseFieldofView)
            vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView + (Time.deltaTime * zoomSpeed);
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        if (Bullet.cl!=null) Debug.Log("Player Bullet Hit = " + Bullet.cl.gameObject.name);
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimMask))
        {
            Debug.Log("Player Raycast Hit = " + hit.transform.name);
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            //Transform soldierTrans = Bullet.cl.gameObject.transform;
            //SoldierEnemy soldierScript = Bullet.cl.gameObject.GetComponent<SoldierEnemy>();
            //if (soldierTrans.position.y >= -15 && soldierScript.currHealth <= 0)
            //    soldierTrans.position.Set(soldierTrans.position.x, soldierTrans.position.y - 1, soldierTrans.position.z);
            //Debug.Log("enemy go down" + " name = " + Bullet.cl.gameObject.name);
        }
    }

    private void LateUpdate()
    {
        if (PistolControl.recoil == true || SniperControl.recoil == true)
        {
            followPos.localEulerAngles = new Vector3(yAxis.Value-5, followPos.localEulerAngles.y, followPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value+5, transform.eulerAngles.z);
            PistolControl.recoil = false;
            SniperControl.recoil = false;
        }
        else
        {
            followPos.localEulerAngles = new Vector3(yAxis.Value, followPos.localEulerAngles.y, followPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
        }
    }
}
