using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger1 : MonoBehaviour
{
    public Transform target;
    
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(target);
        if (GameManager.dancing)
        {
            Quaternion q = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
        }
    }
}
