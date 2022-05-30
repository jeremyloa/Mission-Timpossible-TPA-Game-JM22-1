using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMove : MonoBehaviour
{

    public Transform targetPlayer;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPlayer);
        if(Vector3.Distance(transform.position, targetPlayer.position) >= 10f)
            chase();   


    }

    public void chase()
    {
        transform.position += transform.forward * 20 * Time.deltaTime;
        Debug.Log("enemy move " + transform.position);
    }
}
