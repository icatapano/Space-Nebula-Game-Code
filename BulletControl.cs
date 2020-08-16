using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletControl : MonoBehaviour
{
    AudioSource explosion, large, small, bossSound;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("laser").GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "level")
        {
            explosion = GameObject.FindGameObjectWithTag("alienShip").GetComponent<AudioSource>();
            large = GameObject.FindGameObjectWithTag("largeAsteroid").GetComponent<AudioSource>();
            small = GameObject.FindGameObjectWithTag("smallAsteroid").GetComponent<AudioSource>();
        }
        if (currentScene.name == "boss")
        {
            bossSound = GameObject.FindGameObjectWithTag("boss").GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 laserPos = transform.position;
        Vector3 screenRight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().rightSide;

        // If bullet goes above top of screen, destroy bullet
        if (laserPos.x > screenRight.x)
        {
            Destroy(gameObject);
        }
    }

    // Detect bullet collision with foe or friend objects
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.tag == "alienShip")
        {
            PlayerControl.score += GameObject.FindGameObjectWithTag("alienShip").GetComponent<CritterControl>().scoreValue;
            explosion.Play();
            Destroy(gameObject);
        }
        if (colInfo.collider.tag == "largeAsteroid")
        {
            PlayerControl.score += GameObject.FindGameObjectWithTag("largeAsteroid").GetComponent<CritterControl>().scoreValue;
            large.Play();
            Destroy(gameObject);
        }
        if (colInfo.collider.tag == "smallAsteroid")
        {
            PlayerControl.score += GameObject.FindGameObjectWithTag("smallAsteroid").GetComponent<CritterControl>().scoreValue;
            small.Play();
            Destroy(gameObject);
        }
        if (colInfo.collider.tag == "boss")
        {
            PlayerControl.score += GameObject.FindGameObjectWithTag("boss").GetComponent<BossControl>().scoreValue;
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("boss").GetComponent<BossControl>().health -= 10;
            bossSound.Play();
        }
    }
}
