
using UnityEngine;

public class Ball : MonoBehaviour {

    // Config Parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 14f;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] float randomFactor = 0.2f;

    bool launched = false;

    // State
    Vector2 paddleToBallVector;
    Vector2 lastVelocity = Vector2.zero;

    // Cached component refs
    AudioSource audioSource;
    Rigidbody2D rigidBody2d;
    Vector2 initialVelocity;

	// Use this for initialization
	void Start ()
    {
        paddleToBallVector =  transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidBody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!launched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }     
        else
        {
            ClampVelocity();
        }
    }

    private void ClampVelocity()
    {
        if (lastVelocity == Vector2.zero)
        {
            lastVelocity = rigidBody2d.velocity;
        }
        else
        {
            rigidBody2d.velocity = lastVelocity;
        }

        if(rigidBody2d.velocity.magnitude != initialVelocity.magnitude)
        {
            float difference = initialVelocity.magnitude - rigidBody2d.velocity.magnitude;
            float individualChange = Mathf.Sqrt(Mathf.Abs(difference)) / 2;
            int modifier = difference > 0 ? 1 : -1; 
            Vector2 updateVector = new Vector2(
                rigidBody2d.velocity.x > 0 ? individualChange * modifier : -individualChange * modifier,
                rigidBody2d.velocity.y > 0 ? individualChange * modifier : -individualChange * modifier);
            rigidBody2d.velocity += updateVector;
            lastVelocity = rigidBody2d.velocity;
        }

    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            initialVelocity = new Vector2(xPush, yPush);
            rigidBody2d.velocity = initialVelocity;
            launched = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        Vector2 ballPos = paddlePos + paddleToBallVector;
        transform.position = ballPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (launched)
        {
            lastVelocity = Vector2.zero;
            Vector2 velocityTweak = new Vector2(
                Random.Range(-randomFactor, randomFactor),
                Random.Range(-randomFactor, randomFactor));
            rigidBody2d.velocity += velocityTweak;
            PlaySound();
        }
    }

    private void PlaySound()
    {
        int soundIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[soundIndex]);
    }
}
