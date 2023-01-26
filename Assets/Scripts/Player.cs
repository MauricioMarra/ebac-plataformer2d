using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        Jump();

        Move();
    }

    private void Move()
    {
        //Friction
        if (myRigidBody.velocity.x > 0.0f)
            //myRigidBody.velocity = new Vector2(myRigidBody.velocity.x - frictionSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, frictionSpeed), myRigidBody.velocity.y);
        else if (myRigidBody.velocity.x < 0.0f)
            //myRigidBody.velocity = new Vector2(myRigidBody.velocity.x + frictionSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, frictionSpeed), myRigidBody.velocity.y);


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.velocity = myRigidBody.velocity + Vector2.left * Time.deltaTime * speed;
            myRigidBody.velocity = new Vector2(-speed, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.velocity = myRigidBody.velocity + Vector2.right * Time.deltaTime * speed;
            myRigidBody.velocity = new Vector2(speed, myRigidBody.velocity.y);
        }
    }

    private void Jump()
    {
        //Gravity
        //if (myRigidBody.velocity.y > 0)
        //    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y - jumpSpeed);


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
