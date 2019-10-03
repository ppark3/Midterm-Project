using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool lose;
    public bool win;

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

    public GameObject winText;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        value = 0.5f;
        totalTimerTime = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < totalTimerTime && !dancing && !caught)
        {
            if (startDecrease)
            {
                value = slider.value;
                startDecrease = false;
                t = 0f;
                totalTimerTime = 40f * slider.value;
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
                totalTimerTime = 10f * (1 - slider.value);
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
            winText.gameObject.SetActive(true);
            win = true;
        }
    }
}
