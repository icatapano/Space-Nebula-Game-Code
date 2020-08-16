using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CritterControl : MonoBehaviour {

    static Camera mainCam = null;
    public int scoreValue = 0;
    public bool respawn = true;
    AudioSource sound;

    // Use to respawn critters
    public void Respawn()
    {
        // Find the camera from the object tagged as Player.
        if (!mainCam)
            mainCam = GameObject.FindWithTag("Player").GetComponent<PlayerControl>().mainCam;

        // Randomize the initial position based on the screen size above the top of the screen
        float x = Screen.width + Random.Range(1, 100); 
        float y = Random.Range(8, Screen.height - 8); 

        // Reset velocity to a random value
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1f * Random.Range(4, 7), 0f);

        // then covert it to world coordinates and assign it to the critter.
        Vector3 pos = mainCam.ScreenToWorldPoint(new Vector3(x, y, 0f));
        pos.z = transform.position.z;
        transform.position = pos;
    }

    // collision detection function
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.tag == "Player")
        {
            sound.Play();
            SceneManager.LoadScene("gameLost");
        }
        if (colInfo.collider.tag == "end") // Check for collisions with the ground and respawn
        {
            Respawn();
        }
        if (colInfo.collider.tag == "laser") // Check for collisions with bullet and respawn
        {
            Respawn();
            Destroy(colInfo.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        Respawn();

        sound = GameObject.FindGameObjectWithTag("alienShip").GetComponent<AudioSource>();
    }
}