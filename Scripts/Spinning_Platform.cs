using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning_Platform : MonoBehaviour
{
    public float rotationSpeed;
    public Transform centerpoint;

    //Platform Positions
    public Transform NPoint;
    public Transform EPoint;
    public Transform SPoint;
    public Transform WPoint;

    //Platforms
    public Transform NPlatform;
    public Transform EPlatform;
    public Transform SPlatform;
    public Transform WPlatform;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotating the main mechanism
        centerpoint.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        //Aligning the platforms
        if (NPlatform)
        {
            NPlatform.position = NPoint.position;
        }

        if (EPlatform)
        {
            EPlatform.position = EPoint.position;
        }

        if (SPlatform)
        {
            SPlatform.position = SPoint.position;
        }

        if (WPlatform)
        {
            WPlatform.position = WPoint.position;
        }

    }
}
