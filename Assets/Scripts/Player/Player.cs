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
    private bool isInAir = false;

    [Header("Animations")]
    public Animator playerAnimator;
    public string animatorKeyForRunning = "isRunning";
    public string animatorKeyForJumpingVelocity = "jumpingVelocity";
    public string _animatorTriggerForTouchedGround = "touchedGround";

    private float _currentSpeed;
    private float _runningSpeedFactor = 3;
    private float _scaleDuration = 0.5f;
    private bool isJumping = false;
    private string floorTag = "Floor";

    private bool myVar = true;

    private void Start()
    {
        _currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myVar);

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
            this.playerAnimator.SetBool(animatorKeyForRunning, !isJumping && !isInAir);
            this.transform.DOScaleX(-1, _scaleDuration);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.velocity = myRigidBody.velocity + Vector2.right * Time.deltaTime * speed;
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            this.playerAnimator.SetBool(animatorKeyForRunning, !isJumping && !isInAir);
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

        if (isJumping || isInAir)
        {
            if (myRigidBody.velocity.y > 0)
                playerAnimator.SetFloat(animatorKeyForJumpingVelocity, myRigidBody.velocity.y);
            else if (myRigidBody.velocity.y < 0)
                playerAnimator.SetFloat(animatorKeyForJumpingVelocity, myRigidBody.velocity.y);
        }
    }

    public void Respawn()
    {
        this.transform.position = restartPoint.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isInAir)
        {
            if (collision.gameObject.CompareTag(floorTag))
                playerAnimator.SetTrigger(_animatorTriggerForTouchedGround);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(floorTag))
            isInAir = true;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(floorTag))
        {
            isInAir = false;
            isJumping = false;
            playerAnimator.SetFloat(animatorKeyForJumpingVelocity, 0.0f);
        }
    }
}
