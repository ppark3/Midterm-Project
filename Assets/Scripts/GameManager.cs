using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool lose;
    public bool win;

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

    // VARIABLES NEEDED FOR CUTSCENE
    public Camera camera1; //camera 1 is used for gameplay
    public Camera camera2; //camera 2 is used for cutscenes
    public static bool cutscenePlaying;
    public static bool cutscene1;
    public GameObject door1;
    public GameObject door2;
    public Transform door1Location;
    public Transform door2Location;
    public Vector3 door1OriginalLocation;
    public Vector3 door2OriginalLocation;
    public float waitBeforeStart;


    public GameObject winText;

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
                totalTimerTime = 20f * (1 - slider.value);
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
            winText.gameObject.SetActive(true);
            win = true;
        }
        // ***************************************************************

        // ********************* CONTROLLING CUTSCENES *********************
        if (cutscenePlaying)
        {
            camera1.enabled = false;
            camera2.enabled = true;
            if (!cutscene1) //FIRST CUTSCENE
            {
                waitBeforeStart += Time.deltaTime;
                door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1Location.position, 0.1f);
                door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2Location.position, 0.1f);
            }
            else
            {

            }
        }
    }
}
