﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger2 : MonoBehaviour
{
    public bool isThirdPassenger;

    public bool facingLeft;

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
        if (isThirdPassenger)
        {
            neutralLook = passengerLocation.position + transform.forward;
        }
        else
        {
            neutralLook = passengerLocation.position + transform.right;
        }
        Physics.IgnoreCollision(can.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.cutscenePlaying)
        {
            // IF THE PLAYER IS DANCING, LOOK AT PLAYER BUT WAIT FIRST
            // IF THE PLAYER DANCES WHILE RETURNING, COMMIT TO LOOK (LOOK WITHOUT WAITING)
            if ((GameManager.dancing || player.gameObject.GetComponent<PlayerBehavior>().cheating) && 
                    !player.gameObject.GetComponent<PlayerBehavior>().canThrown &&
                    ((player.gameObject.transform.position.x >= this.gameObject.transform.position.x && !isThirdPassenger) ||
                    (player.gameObject.transform.position.z >= this.gameObject.transform.position.z && isThirdPassenger)))
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

            if (player.gameObject.GetComponent<PlayerBehavior>().canThrown /*&& !player.gameObject.GetComponent<PlayerBehavior>().pickedUp*/ && !commitToLook)
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
                    Quaternion q = Quaternion.LookRotation((canTarget.position /*+ new Vector3(0, 2.8f, 0)*/) - transform.position);
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

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name != this.name 
            && other.gameObject.transform.parent.name == "Passenger1" &&
            Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.parent.transform.position) <
                Vector3.Distance(player.gameObject.transform.position, other.gameObject.transform.parent.transform.position))
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding1 = true;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name != this.name
            && other.gameObject.transform.parent.name == "Passenger2" &&
            Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.parent.transform.position) <
                Vector3.Distance(player.gameObject.transform.position, other.gameObject.transform.parent.transform.position))
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding2 = true;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name != this.name
            && other.gameObject.transform.parent.name == "Passenger3" &&
            Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.parent.transform.position) <
                Vector3.Distance(player.gameObject.transform.position, other.gameObject.transform.parent.transform.position))
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding3 = true;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name != this.name
            && other.gameObject.transform.parent.name == "Passenger4" &&
            Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.parent.transform.position) <
                Vector3.Distance(player.gameObject.transform.position, other.gameObject.transform.parent.transform.position))
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding4 = true;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name != this.name
            && other.gameObject.transform.parent.name == "Passenger5" &&
            Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.parent.transform.position) <
                Vector3.Distance(player.gameObject.transform.position, other.gameObject.transform.parent.transform.position))
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding5 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name == "Passenger1")
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding1 = false;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name == "Passenger2")
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding2 = false;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name == "Passenger3")
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding3 = false;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name == "Passenger4")
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding4 = false;
        }
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name == "Passenger5")
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding5 = false;
        }
    }
}
