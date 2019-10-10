using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger2 : MonoBehaviour
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
        
    }
}
