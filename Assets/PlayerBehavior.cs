using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) // moving up
        {
            this.transform.Translate(new Vector3(0, 0, -moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A)) // moving left
        {
            this.transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.D)) // moving right
        {
            this.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S)) // moving down
        {
            this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }
    }
}
