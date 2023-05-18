using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Reference to the main camera
    private Transform cameraTransform;
    // The last camera X position 
    private float lastCamPosX;

    // Parallax effect speed
    [SerializeField]
    float pSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCamPosX = Camera.main.transform.position.x;
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        // The current camera X position 
        float curCamPosX = cameraTransform.position.x;
        // The difference between the current camera X postion and the last one 
        float deltaMov = curCamPosX - lastCamPosX;
        // Move the sprite to the opposite direction of the character by a factor of the parallax effect speed (pSpeed)
        transform.position = new Vector3(transform.position.x + deltaMov * pSpeed * -0.1f,
            transform.position.y, transform.position.z);
        // Update the last camera X position 
        lastCamPosX = cameraTransform.position.x;
    }
}
