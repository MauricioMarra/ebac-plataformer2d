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

    private float _currentSpeed;
    private float _runningSpeedFactor = 3;

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
            _currentSpeed = speed * _runningSpeedFactor;
        else
            _currentSpeed = speed;

        //Friction
        if (myRigidBody.velocity.x > 0.0f)
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, frictionSpeed), myRigidBody.velocity.y);
        else if (myRigidBody.velocity.x < 0.0f)
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, frictionSpeed), myRigidBody.velocity.y);


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.velocity = myRigidBody.velocity + Vector2.left * Time.deltaTime * speed;
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.velocity = myRigidBody.velocity + Vector2.right * Time.deltaTime * speed;
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //myRigidBody.velocity += Vector2.up * Time.deltaTime * jumpForce;
            myRigidBody.velocity = Vector2.up * jumpForce;
        }
    }

    public void Respawn()
    {
        this.transform.position = restartPoint.transform.position;
    }
}
