using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private Text lives;
    private Text fruit;
    private PinkMan pinkMan;

    [SerializeField]
    protected GameObject inGameMenu;

    private bool menuPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Text components
        lives = GameObject.Find("Lives_Text").GetComponent<Text>();
        fruit = GameObject.Find("Fruit").GetComponent<Text>();

        // Get a reference to the PinkMan component
        pinkMan = GameObject.Find("PinkMan").GetComponent<PinkMan>();

        // Make in-game menu invisible
        inGameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text field to display the number of lives
        lives.text = $"x{pinkMan.Lives.ToString()}";

        // Update the text field to display the number of fruit
        fruit.text = $"Items: {pinkMan.ItemsCount.ToString()}";

        // On escape key press
        OnEscPress();
    }

    void OnEscPress()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPaused)
                // Resume
                Resume();
            else
                // Pause   
                PauseMenu();
        }
    }

    void PauseMenu()
    {
        // Make in-game menu visible
        inGameMenu.SetActive(true);

        // Pause game
        Time.timeScale = 0;

        menuPaused = true;
    }

    public void Resume()
    {
        // Make in-Game menu invisible
        inGameMenu.SetActive(false);

        // Resume game
        Time.timeScale = 1;

        menuPaused = false;
    }

    public void ReturnToTitle()
    {
        // Load the title screen
        SceneManager.LoadScene("TitleScreen");
    }
}
