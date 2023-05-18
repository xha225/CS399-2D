using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinkMan : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    [SerializeField]
    protected float walkSpeed = 100f; // 100 meters

    protected float runSpeed;
    // Boolean varible to check if the character is on the ground
    bool grounded = false;
    // Boolean varible to check if the character is running
    bool run = false;
    // A counter used to trigger running
    int runCounter = 0;
    // The integer value for the Terrain layer
    int terrainLayer;
    // Default character facing direction
    bool faceRight = true;
    // air jump
    bool enableAirJump = false;
    // Jump force
    [SerializeField]
    protected float jumpForce = 100f;
    // Spike tag
    string SpikeTag = "Spikes";

    // Apple tag
    string AppleTag = "Apple";

    public int ItemsCount
    {
        get;
        set;
    } = 0;

    // Number of character lives, default to 3 
    public int Lives
    {
        get;
        set;
    } = 3;
    //
    private void Awake()
    {
        Application.targetFrameRate = -1;
        terrainLayer = LayerMask.NameToLayer("Terrain");
    }

    // Start is called before the first frame update
    void Start()
    {
        runSpeed = 3 * walkSpeed;
        // Get references in the scene
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Load lives from PlayerPrefs, default lives to 3 if the entry does not exist
        Lives = PlayerPrefs.GetInt("Lives",3);
    }

    // Update is called once per frame
    void Update()
    {
        // Handles both keyboard and joystick
        bool jump = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump");
        
        if (jump)
        {
            if (grounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);   
                grounded = false;
                enableAirJump = true;
            }
            else if(enableAirJump)
            {   
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
                enableAirJump = false;
            }
        }

        // Restart level
        if (Lives == 0)
            RestartLevel();
        
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        // Move horizontally
        float hMove = Input.GetAxis("Horizontal");
        float speed = walkSpeed;
        if (run)
            speed = runSpeed;

        // Update character velocity
        rb.velocity = new Vector2(hMove * speed * Time.deltaTime, rb.velocity.y);

        /* Change character face direction*/
        // Face left
        if (hMove < 0 && faceRight)
        {
            rb.transform.Rotate(0f, 180f, 0, Space.Self);
            faceRight = false;
        }

        // Face right
        if (hMove > 0 && !faceRight)
        {
            rb.transform.Rotate(0f, 180f, 0, Space.Self);
            faceRight = true;

        }

        // Set walk animation
        if (hMove == 0)
            anim.SetBool("walk", false);
        else
            anim.SetBool("walk", true);

        // Switch to run animation
        if (System.Math.Abs(hMove) == 1)
        {
            if (runCounter == 3)
            {
                run = true;
                anim.SetBool("run", true);
            }
            runCounter++;
        }
        else
        {
            run = false;
            anim.SetBool("run", false);
            runCounter = 0;
        }


    }



    // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only)
    // Prevent air jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == terrainLayer)
        {
            grounded = true;
            Debug.Log("OnCollisionEnter2D: " + collision.gameObject.layer);
        }

        if (collision.gameObject.tag == SpikeTag)
        {
            // Push the character to the opposite direction
            if (faceRight)
            {
                rb.AddForce(Vector2.left * 5000);
            }
            else
            {
                rb.AddForce(Vector2.right * 5000);
            }

            // Reduce player life by one
            Lives--;

            // Update player lives
            PlayerPrefs.SetInt("Lives", Lives);
        }

        // Update item count
        if (collision.gameObject.tag == AppleTag)
        {
            ItemsCount++;

            // Remove item
            Destroy(collision.gameObject);

            // Update lives count
            if (ItemsCount == 10)
            {
                Lives++;
                // Update player lives
                PlayerPrefs.SetInt("Lives", Lives);
            }
        }
    }

    // OnCollisionExit2D is called when this collider2D/rigidbody2D has stopped touching another rigidbody2D/collider2D (2D physics only)
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit2D: " + collision.gameObject.name);
    }


    private void RestartLevel()
    {
        // Load the current scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
