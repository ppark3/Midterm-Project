using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light myLight;
    public bool changeColors;
    public float colorSpeed;
    public Color mainColor;
    public Color color1;
    public Color color2;

    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColors)
        {
            float t = (Mathf.Sin(Time.time - startTime * colorSpeed));
            myLight.color = Color.Lerp(color1, color2, t);
        }
        else
        {
            myLight.color = mainColor;
        }
    }
}
