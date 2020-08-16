using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public GameObject laserRef;
    public KeyCode moveUp, moveDown, moveLeft, moveRight, fireAction; 
    public float speedX, speedY;
    public bool linearMovement = true;
    public Camera mainCam;
    public static int score = 0;
    private Rigidbody2D rbody;
    public Vector3 rightSide, leftSide;

    AudioSource laserSound;

    void Fire()
    {
        laserSound.Play();
        
        GameObject laserClone = Instantiate(laserRef);
        laserClone.GetComponent<Rigidbody2D>().transform.position = new Vector3(transform.position.x + 1.2f, transform.position.y - 0.01f, -1f);
        laserClone.GetComponent<Rigidbody2D>().velocity = new Vector2(12f, 0f);
    }

    // Initialization function
    void Start()
    {
        // store the rigid body in an attribute for easier access.
        rbody = GetComponent<Rigidbody2D>();

        rightSide = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));
        leftSide = mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));

        laserSound = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // move the player in the 4 directions based on the keys we set up for it
        if (Input.GetKey(moveUp))
        {
            if (linearMovement)                                 // simple constant velocity
                rbody.velocity = new Vector2(0f, speedY);
            else                                                // if we were going to use a force instead
                rbody.AddForce(new Vector2(0f, speedY));
        }
        else if (Input.GetKey(moveDown))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(0f, -speedY);
            else
                rbody.AddForce(new Vector2(0f, -speedY));
        }
        else if (Input.GetKey(moveLeft))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(-speedX, 0f);
            else
                rbody.AddForce(new Vector2(-speedX, 0f));
        }
        else if (Input.GetKey(moveRight))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(speedX, 0f);
            else
                rbody.AddForce(new Vector2(speedX, 0f));
        }
        else
        { 
            // no input, reset the speed
            rbody.velocity = new Vector2(0f, 0f);
        }
        AdjustPosition();

        // Check if fire button is pushed and fire projectile
        if (Input.GetKeyUp(fireAction))
        {
            Fire();
        }      
    }

    // function to make sure the player doesn't go off the screen
    void AdjustPosition()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);
        Vector3 topScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector3 bottomScreen = mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 rightScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));
        Vector3 leftScreen = mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));

        // vertical adjustment
        if (screenPos.y > Screen.height)
            transform.position = new Vector3(transform.position.x, bottomScreen.y, transform.position.z); 
        else if (screenPos.y < 0)
            transform.position = new Vector3(transform.position.x, topScreen.y, transform.position.z); 
        // When ship exits one side of screen, ship enters other side of screen
        else if (screenPos.x > Screen.width)
            transform.position = new Vector3(rightScreen.x, transform.position.y, transform.position.z);
        else if (screenPos.x < 0)
            transform.position = new Vector3(leftScreen.x, transform.position.y, transform.position.z);
    }
}