﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger1 : MonoBehaviour
{
    public GameObject player;
    public Vector3 actualTarget;
    public GameObject can;
    public Transform canTarget;

    public bool decideToLook;
    public bool commitToLook;
    public bool returning;

    public bool decideToLookAtCan;
    
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
            // IF THE PLAYER IS DANCING, LOOK AT PLAYER BUT WAIT FIRST
            // IF THE PLAYER DANCES WHILE RETURNING, COMMIT TO LOOK (LOOK WITHOUT WAITING)
            if (GameManager.dancing && !player.gameObject.GetComponent<PlayerBehavior>().canThrown)
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

            // IF DECIDED TO LOOK, DO THIS STUFF
            if (decideToLook || commitToLook)
            {
                distance = Vector3.Distance(transform.position, actualTarget);
                if (!commitToLook)
                {
                    waitBeforeLook += Time.deltaTime;
                }
                if (waitBeforeLook >= (distance / 9f))
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
            // RETURN TO REGULAR LOOKING POSITION
            else if (!player.gameObject.GetComponent<PlayerBehavior>().canThrown)
            {
                waitBeforeReturn = 0;
                Quaternion q = Quaternion.LookRotation(neutralLook - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, returnRotateSpeed * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, q) <= 1f)
                {
                    returning = false;
                    waitBeforeLook = 0f;
                }
            }

            if (player.gameObject.GetComponent<PlayerBehavior>().canThrown)
            {
                if (!decideToLookAtCan)
                {
                    waitBeforeLook += Time.deltaTime;
                }
                if (waitBeforeLook >= 0.5f)
                {
                    waitBeforeLook = 0;
                    decideToLookAtCan = true;
                }

                if (decideToLookAtCan)
                {
                    canTarget = can.gameObject.transform;
                    Quaternion q = Quaternion.LookRotation((canTarget.position + new Vector3(0, 2.8f, 0)) - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
                    if (Quaternion.Angle(transform.rotation, q) <= 1)
                    {
                        waitBeforeReturn += Time.deltaTime;
                        if (waitBeforeReturn >= 4f)
                        {
                            player.gameObject.GetComponent<PlayerBehavior>().canThrown = false;
                            decideToLookAtCan = false;
                            waitBeforeLook = 0f;
                        }
                    }
                }
            }
        }
    }
}