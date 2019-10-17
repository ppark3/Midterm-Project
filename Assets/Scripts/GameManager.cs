using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public static bool win;
    public GameObject winText;

    // VARIABLES NEEDED FOR CUTSCENE
    public Camera camera1; //camera 1 is used for gameplay
    public Camera camera2; //camera 2 is used for cutscenes
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;
    public GameObject invisiWall;
    public Transform door1Location;
    public Transform door2Location;
    public Vector3 door1OriginalLocation;
    public Vector3 door2OriginalLocation;
    public Transform door3Location;
    public Transform door4Location;
    public Vector3 door3OriginalLocation;
    public Vector3 door4OriginalLocation;
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
    public GameObject passenger3;
    public Transform passenger3Position;
    public GameObject passenger4;
    public Transform passenger4Position;
    public GameObject passenger5;
    public Transform passenger5Position;

    public static bool cutscenePlaying;
    public static bool cutscene1;
    public static bool cutscene2;
    public static bool cutscene3;
    public static bool cutscene4;
    public static bool cutscene5;
    public static bool cutscene6;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        value = 0.5f;
        totalTimerTime = 20f;
        cutscenePlaying = true;
        door1OriginalLocation = door1.transform.position;
        door2OriginalLocation = door2.transform.position;
        door3OriginalLocation = door3.transform.position;
        door4OriginalLocation = door4.transform.position;

        // FOR TESTING
        //cutscene2 = true;
        //cutscene3 = true;
        //cutscene4 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lose)
        {
            cutscene1 = false;
            cutscene2 = false;
            cutscene3 = false;
            cutscene4 = false;
            cutscene5 = false;
            cutscene6 = false;
            SceneManager.LoadScene("StartScene");
        }
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
                totalTimerTime = 5f * slider.value;
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
            if (cutscene6)
            {
                winText.gameObject.SetActive(true);
                win = true;
            }
            else if (cutscene5)
            {
                cutscenePlaying = true;
                slider.value = 0.5f;
                totalTimerTime = 20f;
                t = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheating = false;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheatingNum = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().cheating = false;
            }
            else if (cutscene4)
            {
                cutscenePlaying = true;
                slider.value = 0.5f;
                totalTimerTime = 20f;
                t = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheating = false;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheatingNum = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().cheating = false;
            }
            else if (cutscene3)
            {
                cutscenePlaying = true;
                slider.value = 0.5f;
                totalTimerTime = 20f;
                t = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheating = false;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheatingNum = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().cheating = false;
            }
            else if (cutscene2)
            {
                cutscenePlaying = true;
                slider.value = 0.5f;
                totalTimerTime = 20f;
                t = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheating = false;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheatingNum = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().cheating = false;
            }
            else if (cutscene1)
            {
                cutscenePlaying = true;
                slider.value = 0.5f;
                totalTimerTime = 20f;
                t = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheating = false;
                player.gameObject.GetComponent<PlayerBehavior>().toPreventCheatingNum = 0f;
                player.gameObject.GetComponent<PlayerBehavior>().cheating = false;
            }
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
                if (waitBeforeStart >= 5)
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
                    passenger1.gameObject.transform.rotation = Quaternion.RotateTowards(passenger1.gameObject.transform.rotation, right.rotation, 500f * Time.deltaTime);
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
                    player.gameObject.GetComponent<PlayerBehavior>().canThrown = false;
                    passenger1.gameObject.SetActive(true);
                    camera1.enabled = false;
                    camera2.enabled = true;
                }
            }
            else if (!cutscene3) // THIRD CUTSCENE ****************************************
            {
                waitBeforeStart += Time.deltaTime;
                if (waitBeforeStart >= 6)
                {
                    invisiWall.gameObject.SetActive(true);
                    cutscene3 = true;
                    cutscenePlaying = false;
                    waitBeforeStart = 0;
                }
                else if (waitBeforeStart >= 3)
                {
                    camera2.enabled = false;
                    camera1.enabled = true;
                    passenger2.gameObject.transform.rotation = right.rotation;
                    passenger2.gameObject.transform.position = Vector3.MoveTowards(passenger2.gameObject.transform.position, passenger2Position.position, 0.5f);
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1OriginalLocation, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2OriginalLocation, 0.1f);
                }
                else if (waitBeforeStart >= 2)
                {
                    passenger2.gameObject.transform.Translate(new Vector3(0, 0, 0.5f));
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
                    player.gameObject.GetComponent<PlayerBehavior>().canThrown = false;
                    passenger2.gameObject.SetActive(true);
                    camera1.enabled = false;
                    camera2.enabled = true;
                }
            }
            else if (!cutscene4) // FOURTH CUTSCENE ****************************************
            {
                waitBeforeStart += Time.deltaTime;
                if (waitBeforeStart >= 7)
                {
                    invisiWall.gameObject.SetActive(true);
                    cutscene4 = true;
                    cutscenePlaying = false;
                    waitBeforeStart = 0;
                }
                else if (waitBeforeStart >= 3)
                {
                    camera2.enabled = false;
                    camera1.enabled = true;
                    passenger3.gameObject.transform.rotation = right.rotation;
                    passenger3.gameObject.transform.position = Vector3.MoveTowards(passenger3.gameObject.transform.position, passenger3Position.position, 0.5f);
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1OriginalLocation, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2OriginalLocation, 0.1f);
                }
                else if (waitBeforeStart >= 2)
                {
                    passenger3.gameObject.transform.Translate(new Vector3(0, 0, 0.5f));
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
                    player.gameObject.GetComponent<PlayerBehavior>().canThrown = false;
                    passenger3.gameObject.SetActive(true);
                    passenger3.gameObject.GetComponent<Passenger2>().isThirdPassenger = true;
                    camera1.enabled = false;
                    camera2.enabled = true;
                }
            }
            else if (!cutscene5) // FIFTH CUTSCENE ****************************************
            {
                waitBeforeStart += Time.deltaTime;
                if (waitBeforeStart >= 5)
                {
                    invisiWall.gameObject.SetActive(true);
                    cutscene5 = true;
                    cutscenePlaying = false;
                    waitBeforeStart = 0;
                }
                else if (waitBeforeStart >= 3)
                {
                    camera2.enabled = false;
                    camera1.enabled = true;
                    passenger4.gameObject.transform.rotation = right.rotation;
                    passenger4.gameObject.transform.position = Vector3.MoveTowards(passenger4.gameObject.transform.position, passenger4Position.position, 0.5f);
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1OriginalLocation, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2OriginalLocation, 0.1f);
                }
                else if (waitBeforeStart >= 2)
                {
                    passenger4.gameObject.transform.Translate(new Vector3(0, 0, 0.5f));
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
                    player.gameObject.GetComponent<PlayerBehavior>().canThrown = false;
                    passenger4.gameObject.SetActive(true);
                    camera1.enabled = false;
                    camera2.enabled = true;
                }
            }
            else if (!cutscene6) // SIXTH CUTSCENE ****************************************
            {
                waitBeforeStart += Time.deltaTime;
                if (waitBeforeStart >= 5)
                {
                    invisiWall.gameObject.SetActive(true);
                    cutscene6 = true;
                    cutscenePlaying = false;
                    waitBeforeStart = 0;
                }
                else if (waitBeforeStart >= 3)
                {
                    camera2.enabled = false;
                    camera1.enabled = true;
                    passenger5.gameObject.transform.rotation = right.rotation;
                    passenger5.gameObject.transform.position = Vector3.MoveTowards(passenger5.gameObject.transform.position, passenger5Position.position, 0.5f);
                    door1.gameObject.transform.position = Vector3.MoveTowards(door1.gameObject.transform.position, door1OriginalLocation, 0.1f);
                    door2.gameObject.transform.position = Vector3.MoveTowards(door2.gameObject.transform.position, door2OriginalLocation, 0.1f);
                }
                else if (waitBeforeStart >= 2)
                {
                    passenger5.gameObject.transform.Translate(new Vector3(0, 0, 0.5f));
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
                    player.gameObject.GetComponent<PlayerBehavior>().canThrown = false;
                    passenger5.gameObject.SetActive(true);
                    passenger5.gameObject.GetComponent<Passenger1>().isFifthPassenger = true;
                    camera1.enabled = false;
                    camera2.enabled = true;
                }
            }
        }
    }
}
