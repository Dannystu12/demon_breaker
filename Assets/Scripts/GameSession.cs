using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1.0f;
    [SerializeField] int pointsPerBlock = 10;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplayEnabled = false;

    [SerializeField] static int currentScore = 0;


    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        { 
            Destroy(gameObject);
        }
        else
        {
            currentScore = 0;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        DisplayScore();
    }

    // Update is called once per frame
    void Update () {
        Time.timeScale = gameSpeed;
	}


    public void AddToScore()
    {
        gameSpeed += 0.002f;
        currentScore += pointsPerBlock;
        DisplayScore();
    }

    private void DisplayScore()
    {
        scoreText.text = string.Format("{0:0000}", currentScore);
    }

    public void DestroySelf()
    { 
        Destroy(gameObject);
    }

    public bool IsAutoplayEnabled()
    {
        return autoplayEnabled;
    }
}
