using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController: MonoBehaviour {
    public ScoreController scoreController;
    public PlayerController playerController;
    public AIController aiController;

    public static MainController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
