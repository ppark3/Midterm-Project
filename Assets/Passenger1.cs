using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger1 : MonoBehaviour
{
    public GameObject player;
    public Vector3 actualTarget;

    public bool decideToLook;
    public bool commitToLook;
    public bool returning;
    
    public float rotateSpeed;
    public float returnRotateSpeed;
    public Vector3 neutralLook;
    public Transform passengerLocation;

    public float waitBeforeLook;
    public float waitBeforeReturn;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 100f;
        returnRotateSpeed = 80f;
        neutralLook = passengerLocation.position + transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.cutscenePlaying)
        {
            if (GameManager.dancing)
            {
                decideToLook = true;
                actualTarget = player.gameObject.transform.position;
                if (returning)
                {
                    commitToLook = true;
                }
            }
            else
            {
                decideToLook = false;
            }
            //this.transform.LookAt(target);
            if ((decideToLook || commitToLook))
            {
                distance = Vector3.Distance(transform.position, actualTarget);
                if (!commitToLook)
                {
                    waitBeforeLook += Time.deltaTime;
                }
                if (waitBeforeLook >= (distance / 10f))
                {
                    waitBeforeLook = 0;
                    commitToLook = true;
                }
                if (commitToLook)
                {
                    Quaternion q = Quaternion.LookRotation(actualTarget - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
                    if (Quaternion.Angle(transform.rotation, q) <= 1)
                    {
                        waitBeforeReturn += Time.deltaTime;
                        if (waitBeforeReturn >= 2f)
                        {
                            commitToLook = false;
                            returning = true;
                        }
                    }
                }
            }
            else
            {
                waitBeforeReturn = 0;
                Quaternion q = Quaternion.LookRotation(neutralLook - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, returnRotateSpeed * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, q) <= 1f)
                {
                    returning = false;
                }
            }
        }
    }
}
