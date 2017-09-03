using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    public int scoreMy = 0, scoreBot = 0;
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = string.Format("Score: {0}:{1}", scoreMy, scoreBot);
	}
}
