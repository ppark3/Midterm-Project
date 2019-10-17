using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrigger : MonoBehaviour
{
    public Rigidbody canRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehavior>().canPickUp = true;
        }
        if (other.tag == "Floor")
        {
            canRigidbody.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehavior>().canPickUp = false;
        }

        if (other.tag == "Floor")
        {
            canRigidbody.isKinematic = false;
        }
    }
}
