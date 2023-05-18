using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    protected Transform target;

    [SerializeField]
    float xOffset = 0;

    [SerializeField]
    float yOffset = 5f;
    // Initial location of the character when the scene loads
    float initY;

    [SerializeField]
    float cameraSpeed = 1f;
    [SerializeField]
    float cameraDistance = 5;
    [SerializeField]
    bool enableLookAhead = false;

    bool changedDirection = true;
    // Start is called before the first frame update
    void Start()
    {
        initY = target.position.y + yOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //if (target.rotation.y < 0) // Going to the left
        //{
        //    if (changedDirection)
        //    {
        //        //xOffset *= -1;
        //        xOffset = 0;
        //        changedDirection = false;
        //    }

        //    // Camera position
        //    transform.position = new Vector3(target.position.x - xOffset, initY, transform.position.z);

        //    if (xOffset < cameraDistance)
        //    {
        //        xOffset = Mathf.Lerp(xOffset, cameraDistance, Time.deltaTime * cameraSpeed);
        //    }


        //}
        //else
        //{
        //    if (!changedDirection)
        //    {
        //        xOffset = 0;
        //        changedDirection = true;
        //    }

        // Camera position
        //transform.position = new Vector3(target.position.x + xOffset, initY + yOffset, transform.position.z);
        transform.position = new Vector3(target.position.x + xOffset, target.position.y + yOffset, transform.position.z);

        // if (xOffset < cameraDistance)
        if (enableLookAhead)
            xOffset = Mathf.Lerp(xOffset, cameraDistance, Time.deltaTime * cameraSpeed);
        //}



        Debug.Log(xOffset);
    }

}
