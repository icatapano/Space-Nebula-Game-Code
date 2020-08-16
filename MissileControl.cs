using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissileControl : MonoBehaviour
{
    public bool missileFire = true;
    public int scoreValue;
    AudioSource sound, explode;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("missile").GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("boss").GetComponent<Collider2D>());
        sound = GameObject.FindGameObjectWithTag("missile").GetComponent<AudioSource>();
        MissileFire();
    }

    // Re-fire missles as they go off screen or are destroyed
    public void MissileFire()
    {
        GetComponent<Rigidbody2D>().transform.position = new Vector3(6f, Random.Range(-4, 4), -1f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * Random.Range(5, 10), 0f);
        sound.Play();
    }
    
    // Detect bullet collision with foe or friend objects
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.tag == "Player")
        {
            SceneManager.LoadScene("gameLost");
        }
        if (colInfo.collider.tag == "end")
        {
            MissileFire();
        }
        if (colInfo.collider.tag == "laser")
        {
            PlayerControl.score += GameObject.FindGameObjectWithTag("missile").GetComponent<MissileControl>().scoreValue;
            Destroy(colInfo.gameObject);
            MissileFire();
        }
    }
}
