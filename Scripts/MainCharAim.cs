using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MainCharAim : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform followPos;
    [SerializeField] public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;
    Ray ray;
    public RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimMask))
        {
            Debug.Log(hit.transform.name);
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        followPos.localEulerAngles = new Vector3(yAxis.Value, followPos.localEulerAngles.y, followPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
