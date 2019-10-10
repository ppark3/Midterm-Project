﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;

    public GameObject musicManager;

    public bool caught;

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.cutscenePlaying)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.J) && this.gameObject.transform.position.z >= -15) // moving up
            {
                this.transform.rotation = up.rotation;
                this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.J) && this.gameObject.transform.position.x <= 50) // moving left
            {
                this.transform.rotation = left.rotation;
                this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.J) && this.gameObject.transform.position.x >= -50) // moving right
            {
                this.transform.rotation = right.rotation;
                this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.J) && this.gameObject.transform.position.z <= 15) // moving down
            {
                this.transform.rotation = down.rotation;
                this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
            }
            if (Input.GetKeyDown(KeyCode.J) && !caught)
            {
                musicManager.gameObject.GetComponent<MusicManager>().goodDancing = true;
                GameManager.dancing = true;
                GameManager.startIncrease = true;
            }
            if (Input.GetKeyUp(KeyCode.J) || GameManager.cutscenePlaying)
            {
                musicManager.gameObject.GetComponent<MusicManager>().goodDancing = false;
                musicManager.gameObject.GetComponent<MusicManager>().badDancing = false;
                GameManager.dancing = false;
                GameManager.startDecrease = true;
                GameManager.caught = false;
                GameManager.startSpeedDecrease = false;
                caught = false;
            }
        }
        else
        {
            musicManager.gameObject.GetComponent<MusicManager>().goodDancing = false;
            musicManager.gameObject.GetComponent<MusicManager>().badDancing = false;
            GameManager.dancing = false;
            GameManager.startDecrease = true;
            GameManager.caught = false;
            GameManager.startSpeedDecrease = false;
            caught = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Vision")
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                caught = true;
                musicManager.gameObject.GetComponent<MusicManager>().badDancing = true;
                musicManager.gameObject.GetComponent<MusicManager>().goodDancing = false;
                musicManager.gameObject.GetComponent<MusicManager>().goodPlaying = false;
                GameManager.caught = true;
                GameManager.startSpeedDecrease = true;
            }
            if (Input.GetKey(KeyCode.J) && !caught)
            {
                caught = true;
                musicManager.gameObject.GetComponent<MusicManager>().badDancing = true;
                musicManager.gameObject.GetComponent<MusicManager>().goodDancing = false;
                musicManager.gameObject.GetComponent<MusicManager>().goodPlaying = false;
                GameManager.caught = true;
                GameManager.startSpeedDecrease = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vision")
        {
            caught = false;
            musicManager.gameObject.GetComponent<MusicManager>().badDancing = false;
            GameManager.caught = false;
            GameManager.startSpeedDecrease = false;
        }
    }
}
