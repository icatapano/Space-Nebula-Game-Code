using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenControl : MonoBehaviour
{
    public GameObject infoTextRef;
    private int counter = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            switch (counter)
            {
                case 1:
                    infoTextRef.GetComponent<TextMesh>().text = "Use the arrow keys to move your ship, and spacebar to fire your lasers";
                    counter++;
                    break;
                case 2:
                    infoTextRef.GetComponent<TextMesh>().text = "Destroy the alien ships, and asteroids to earn points.";
                    counter++;
                    break;
                case 3:
                    infoTextRef.GetComponent<TextMesh>().text = "Earn enough point to advance to the next level.";
                    counter++;
                    break;
                case 4:
                    infoTextRef.GetComponent<TextMesh>().text = "Caution! A much more dangerous opponent awaits. Press any key to start!";
                    counter++;
                    break;
                case 5:
                    SceneManager.LoadScene("level");
                    counter++;
                    break;
            }
        }
    }
}
