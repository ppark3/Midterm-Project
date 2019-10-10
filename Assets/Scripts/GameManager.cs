using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // VARIABLES NEEDED FOR BAR
    public Slider slider;
    public float totalTimerTime;
    public float t;
    public float timerSpeed;
    public float value;
    public static bool dancing;
    public static bool caught;
    public static bool startIncrease;
    public static bool startDecrease;
    public static bool startSpeedDecrease;

    public bool lose;
    public bool win;
    public GameObject winText;



    // VARIABLES NEEDED FOR CUTSCENE
    public Camera camera1; //camera 1 is used for gameplay
    public Camera camera2; //camera 2 is used for cutscenes
    public GameObject door1;
    public GameObject door2;
    public GameObject invisiWall;
    public Transform door1Location;
    public Transform door2Location;
    public Vector3 door1OriginalLocation;
    public Vector3 door2OriginalLocation;
    public float waitBeforeStart;

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public GameObject player;
    public Transform playerPosition;
    public GameObject passenger1;
    public Transform passenger1Position;
    public GameObject passenger2;
    public Transform passenger2Position;

    public static bool cutscenePlaying;
    public static bool cutscene1;
    public static bool cutscene2;
    public static bool cutscene3;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        value = 0.5f;
        totalTimerTime = 20f;
        cutscenePlaying = true;
        door1OriginalLocation = door1.transform.position;
        door2OriginalLocation = door2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // ********************* CONTROLLING THE BAR *********************
        if (t < totalTimerTime && !dancing && !caught && !cutscenePlaying)
        {
            if (startDecrease)
            {
                value = slider.value;
                startDecrease = false;
                t = 0f;
                totalTimerTime = 30f * slider.value;
            }
            slider.value = Mathf.Lerp(value, 0, Mathf.Min(1, t / totalTimerTime));
            t += Time.deltaTime;
        }
        else if (t < totalTimerTime && dancing && !caught)
        {
            if (startIncrease)
            {
                value = slider.value;
                startIncrease = false;
                t = 0f;
                totalTimerTime = 2f * (1 - slider.value);
            }
            slider.value = Mathf.Lerp(value, 1, Mathf.Min(1, t / totalTimerTime));
            t += Time.deltaTime;
        }
        else if (t < totalTimerTime && caught)
        {
            if (startSpeedDecrease)
            {
                value = slider.value;
                startSpeedDecrease = false;
                t = 0f;
                totalTimerTime = 3f * slider.value;
            }
            slider.value = Mathf.Lerp(value, 0, Mathf.Min(1, t / totalTimerTime));
            t += Time.deltaTime;
        }
        if (slider.value <= 0.01)
        {
            lose = true;
        }
        if (slider.value >= 0.99)
        {
            if (cutscene2)
            {
                cutscenePlaying = true;
                slider.value = 0.5f;
                totalTimerTime = 20f;
                t = 0f;
            }
            else if (cutscene1)
            {
                cutscenePlaying = true;
                slider.value = 0.5f;
                totalTimerTime = 20f;
                t = 0f;
            }
            //winText.gameObject.SetActive(true);
            //win = true;
        }
        // ***************************************************************

        // ********************* CONTROLLING CUTSCENES *********************
        if (cutscenePlaying)
        {
            if (!cutscene1) //FIRST CUTSCENE ****************************************
            {
                waitBeforeStart += Time.deltaTime;
                if (waitBeforeStart >= 4.5)
                {
                    invisiWall.gameObject.SetActive(true);
                    cutscene1 = true;
                    cutscenePlaying = false;
                    waitBeforeStart = 0;
                }
                else if (waitBeforeStart >= 3)
                {
                    camera2.enabled = false;
                    camera1.enabled = true;
                    player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, playerPosition.position, 0.5f);
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1OriginalLocation, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2OriginalLocation, 0.1f);
                }
                else if (waitBeforeStart >= 2)
                {
                    player.gameObject.transform.Translate(new Vector3(0,0,0.5f));
                }
                else if (waitBeforeStart >= 1)
                {
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1Location.position, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2Location.position, 0.1f);
                    invisiWall.gameObject.SetActive(false);
                }
                else if (waitBeforeStart < 1)
                {
                    camera1.enabled = false;
                    camera2.enabled = true;
                }
            }
            else if (!cutscene2) //SECOND CUTSCENE ****************************************
            {
                waitBeforeStart += Time.deltaTime;
                if (waitBeforeStart >= 4.5)
                {
                    invisiWall.gameObject.SetActive(true);
                    cutscene2 = true;
                    cutscenePlaying = false;
                    waitBeforeStart = 0;
                }
                else if (waitBeforeStart >= 3)
                {
                    camera2.enabled = false;
                    camera1.enabled = true;
                    passenger1.gameObject.transform.rotation = right.rotation;
                    passenger1.gameObject.transform.position = Vector3.MoveTowards(passenger1.gameObject.transform.position, passenger1Position.position, 0.5f);
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1OriginalLocation, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2OriginalLocation, 0.1f);
                }
                else if (waitBeforeStart >= 2)
                {
                    passenger1.gameObject.transform.Translate(new Vector3(0, 0, 0.5f));
                }
                else if (waitBeforeStart >= 1)
                {
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1Location.position, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2Location.position, 0.1f);
                    invisiWall.gameObject.SetActive(false);
                }
                else if (waitBeforeStart < 1)
                {
                    player.gameObject.transform.position = playerPosition.position;
                    player.gameObject.transform.rotation = down.rotation;
                    passenger1.gameObject.SetActive(true);
                    camera1.enabled = false;
                    camera2.enabled = true;
                }
            }
            else if (!cutscene3)
            {

            }
        }
    }
}
