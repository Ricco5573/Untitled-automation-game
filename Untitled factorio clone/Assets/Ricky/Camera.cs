using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Camera mainCam;
   
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();  
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
