using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController: MonoBehaviour {
    public ScoreController scoreController;
    public PlayerController playerController;
    public AIController aiController;
    private Vector3 myStartPos, enemyStartPos;

    public static MainController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        myStartPos = playerController.transform.position;
        enemyStartPos = aiController.transform.position;
    }

    public void Restart()
    {
        playerController.transform.position = myStartPos;
        aiController.transform.position = enemyStartPos;
    }
}
