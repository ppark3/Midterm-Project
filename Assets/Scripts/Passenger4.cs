using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger4 : MonoBehaviour
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
    public float waitBeforeCanReturn;
    public float distance;

    // WALKING PASSENGER EXCLUSIVE VARIABLES
    public float walkingSpeed;
    public Transform leftLocation;
    public Transform rightLocation;
    public bool facingRight;
    public bool keepWalking;
    public float waitingTimeToRotate;

    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 120f;
        returnRotateSpeed = 120f;
        neutralLook = passengerLocation.position + -transform.right;
        keepWalking = true;
        facingRight = true;
        walkingSpeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.cutscenePlaying || GameManager.win)
        {
            // IF THE PLAYER IS DANCING, LOOK AT PLAYER BUT WAIT FIRST
            // IF THE PLAYER DANCES WHILE RETURNING, COMMIT TO LOOK (LOOK WITHOUT WAITING)
            if ((GameManager.dancing || player.gameObject.GetComponent<PlayerBehavior>().cheating)
                 && !player.gameObject.GetComponent<PlayerBehavior>().canThrown)
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
                    keepWalking = false;
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
            else if ((!player.gameObject.GetComponent<PlayerBehavior>().canThrown || player.gameObject.GetComponent<PlayerBehavior>().pickedUp) 
                        && !keepWalking)
            {
                waitBeforeReturn = 0;
                waitBeforeCanReturn = 0f;
                Quaternion q = Quaternion.LookRotation(neutralLook - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, returnRotateSpeed * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, q) <= 1f)
                {
                    returning = false;
                    waitBeforeLook = 0f;
                    keepWalking = true;
                }
            }

            if (keepWalking && !returning)
            {
                if (facingRight)
                {
                    neutralLook = rightLocation.position;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, rightLocation.position, walkingSpeed);
                    if (Vector3.Distance(transform.position, rightLocation.position) <= 0.05f)
                    {
                        Quaternion q = Quaternion.LookRotation(leftLocation.position - transform.position);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, returnRotateSpeed * Time.deltaTime);
                        if (Quaternion.Angle(transform.rotation, q) <= 1f)
                        {
                            facingRight = false;
                            Debug.Log("This is: " + leftLocation.position);
                            Debug.Log(leftLocation.position + transform.right);
                        }
                    }
                }
                else
                {
                    neutralLook = leftLocation.position;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, leftLocation.position, walkingSpeed);
                    if (Vector3.Distance(transform.position, leftLocation.position) <= 0.05f)
                    {
                        Quaternion q = Quaternion.LookRotation(rightLocation.position - transform.position);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, returnRotateSpeed * Time.deltaTime);
                        if (Quaternion.Angle(transform.rotation, q) <= 1f)
                        {
                            facingRight = true;
                            Debug.Log("This is: " + rightLocation.position);
                            Debug.Log(rightLocation.position + transform.right);
                        }
                    }
                }
            }

            if (player.gameObject.GetComponent<PlayerBehavior>().canThrown /*&& !player.gameObject.GetComponent<PlayerBehavior>().pickedUp*/ && !commitToLook)
            {
                keepWalking = false;

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
                        waitBeforeCanReturn += Time.deltaTime;
                        if (waitBeforeCanReturn >= 4f)
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

    /*public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Vision" && other.gameObject.transform.parent.name != this.name &&
            Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.parent.transform.position) <
                Vector3.Distance(player.gameObject.transform.position, other.gameObject.transform.parent.transform.position))
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding4 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Vision")
        {
            player.gameObject.GetComponent<PlayerBehavior>().hiding4 = false;
        }
    }*/
}
