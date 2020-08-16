using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossControl : MonoBehaviour
{
    public GameObject missileRef, bossHPRef, scoreTextRef;
    public GameObject[] missileArray;
    public int scoreValue;
    public int health;

    // Detect bullet collision with foe or friend objects
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.tag == "Player")
        {
            SceneManager.LoadScene("gameLost");
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        // populate missle array
        for (int i = 0; i < missileArray.Length; i++)
        {
            missileArray[i] = Instantiate(missileRef) as GameObject;
        }
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("missile").GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("boss").GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        bossHPRef.GetComponent<TextMesh>().text = "Boss Health: " + health;
        scoreTextRef.GetComponent<TextMesh>().text = "Score: " + PlayerControl.score;

        // Check if boss is destroyed and game is won
        if (health < 1)
        {
            SceneManager.LoadScene("gameWon");
        }
    }
}
