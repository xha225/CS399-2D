using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Finishline : MonoBehaviour
{
    string playerName = "PinkMan";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == playerName )
        {
            // Load the next level
            SceneManager.LoadScene("Level2");
        }

    }

}
