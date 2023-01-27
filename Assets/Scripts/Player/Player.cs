using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Transform restartPoint;

    [Header("Movement")]
    public float speed;
    public float frictionSpeed;

    [Header("Jump")]
    public float jumpForce;
    public float jumpSpeed;

    [Header("Animations")]
    public Animator playerAnimator;
    public string animatorKeyForRunning = "isRunning";

    private float _currentSpeed;
    private float _runningSpeedFactor = 3;
    private float _scaleDuration = 0.5f;
    private bool isJumping = false;


    private void Start()
    {
        _currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        Move();
    }

    private void Move()
    {
        //Player is running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speed * _runningSpeedFactor;
            playerAnimator.speed = 1.5f;
        }
        else
        {
            _currentSpeed = speed;
            playerAnimator.speed = 1.0f;
        }

        //Friction
        if (myRigidBody.velocity.x > 0.0f)
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, frictionSpeed), myRigidBody.velocity.y);
        else if (myRigidBody.velocity.x < 0.0f)
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, frictionSpeed), myRigidBody.velocity.y);


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.velocity = myRigidBody.velocity + Vector2.left * Time.deltaTime * speed;
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            this.playerAnimator.SetBool(animatorKeyForRunning, true);
            this.transform.DOScaleX(-1, _scaleDuration);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.velocity = myRigidBody.velocity + Vector2.right * Time.deltaTime * speed;
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            this.playerAnimator.SetBool(animatorKeyForRunning, true);
            this.transform.DOScaleX(1, _scaleDuration);
        }
        else
        {
            this.playerAnimator.SetBool(animatorKeyForRunning, false);
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            myRigidBody.velocity = Vector2.up * jumpForce;
        }
    }

    public void Respawn()
    {
        this.transform.position = restartPoint.transform.position;
    }
}
