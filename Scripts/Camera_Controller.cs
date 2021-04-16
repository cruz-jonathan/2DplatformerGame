using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform target;
    public float yOffset; 

    public float minHeight, maxHeight;

    public Transform farBackground;
    public Transform middleBackground;

    //private float lastXPos;
    private Vector2 lastPos;

    public bool stopFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow)

        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight) + yOffset, transform.position.z);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);


        farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;

            lastPos = transform.position;
    }
}
