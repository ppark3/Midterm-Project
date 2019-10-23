using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwaySound : MonoBehaviour
{
    public static SubwaySound _SubwaySounds;

    private void Awake()
    {
        if (_SubwaySounds == null)
        {
            _SubwaySounds = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
