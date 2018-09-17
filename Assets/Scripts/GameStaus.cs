using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStaus : MonoBehaviour {
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1.0f;
    [SerializeField] int pointsPerBlock = 10;

    [SerializeField] int currentScore = 0;
    
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = gameSpeed;
	}

    public void AddToScore()
    {
        currentScore += pointsPerBlock;
    }

}
