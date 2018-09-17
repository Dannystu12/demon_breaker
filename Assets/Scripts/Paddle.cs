using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    // Configuration Parameters
    [SerializeField] float screenWidthUnits = 16f;
    [SerializeField] float minX = 2f;
    [SerializeField] float maxX = 14f;

    GameSession gameSession;
    Ball ball;

	// Use this for initialization
	void Start () {
        ball = FindObjectOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();

    }
	
	// Update is called once per frame
	void Update () {
        float mousPosUnits = GetXPos();
        mousPosUnits = Mathf.Clamp(mousPosUnits, minX, maxX);
        Vector2 paddlePos = new Vector2(mousPosUnits, transform.position.y);
        transform.position = paddlePos;
	}

    private float GetXPos()
    {
        if(gameSession.IsAutoplayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthUnits;
        }
    }
}
