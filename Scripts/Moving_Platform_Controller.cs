using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Platform Jobs
//Go to next point
// once reaches next point, assign new point and go to that one


public class Moving_Platform_Controller : MonoBehaviour
{
    public Transform[] points;
    public float speed;

    public Transform platform;
    public int currentPoint = 0;
    private GameObject nextPoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(platform.position, points[currentPoint].position) == 0)
        {
            currentPoint++;
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
