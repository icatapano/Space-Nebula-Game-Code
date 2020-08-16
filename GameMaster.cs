using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject aliensRef, largeAstRef, smallAstRef, scoreRef, levelRef;
    public GameObject[] aliens, largeAst, smallAst;
    public static int level = 1;
    private int arraySize;

    // Start appropriate level
    void StartLevel()
    {
        // array size for enemy objects
        arraySize = level + 1;

        // initialize size of arrays based off level
        aliens = new GameObject[arraySize];
        largeAst = new GameObject[arraySize];
        smallAst = new GameObject[arraySize];

        // Populate arrays with copies of prefabs
        for (int i = 0; i < arraySize; i++)
        {
            aliens[i] = Instantiate(aliensRef) as GameObject;
            smallAst[i] = Instantiate(smallAstRef) as GameObject;
            largeAst[i] = Instantiate(largeAstRef) as GameObject;
        }

        // look at maze game for how to do the arrays
        if (level == 1)
        {
            PlayerControl.score = 0;
        }
    }

    // Check if end of level criteria has been reached
    void CheckEndLevel()
    {
        if (level == 1)
        {
            // Check if player score is 0 and the game is won
            if (PlayerControl.score >= 500)
            {
                level++;
                SceneManager.LoadScene("level");
            }
        }
        else if (level == 2)
        {
            // Check if game has been won
            if (PlayerControl.score >= 1000)
            {
                level++;
                SceneManager.LoadScene("level");
            }
        }
        else if (level == 3)
        {
            // Check if game has been won
            if (PlayerControl.score >= 1500)
            {
                SceneManager.LoadScene("boss");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        levelRef.GetComponent<TextMesh>().text = "Level: " + level;
        
        CheckEndLevel();
    }

    void LateUpdate()
    {
        scoreRef.GetComponent<TextMesh>().text = "Score: " + PlayerControl.score;
    }
}
