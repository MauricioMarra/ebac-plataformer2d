using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Transform restartPoint;

    [Header("Movement")]
    //public float speed;
    //public float frictionSpeed;
    //public float runningSpeedFactor = 1;
    public ParticleSystem runVfx = null;

    [Header("Jump")]
    //public float jumpForce;
    //public float jumpSpeed;
    private bool isInAir = false;

    [Header("Animations")]
    public Animator playerAnimator;
    public string animatorKeyForRunning = "isRunning";
    public string animatorKeyForJumpingVelocity = "jumpingVelocity";
    public string _animatorTriggerForTouchedGround = "touchedGround";
    public string animatorKeyForDeath = "death";

    public SOPlayerConfig soPlayerConfig;

    [Header("Audio Config")]
    public AudioHelper audioHelper;

    private float _currentSpeed;
    private float _scaleDuration = 0.5f;
    private bool isJumping = false;
    private string floorTag = "Floor";
    private HealthBase _health;

    private bool myVar = true;

    public float hSliderValue { get; private set; }

    private void Awake()
    {
        _health = GetComponent<HealthBase>();
        if (_health != null)
            _health.OnKill += playerKill;
    }

    private void Start()
    {
        _currentSpeed = soPlayerConfig.speed;
    }

    // Update is called once per frame
    void Update()
    {
        var state = GameManager.instance.GetCurrentState();

        //if (!(GameManager.instance.GetCurrentState() is StateDeath))
        if(state is not StateDeath && state is not StateEndGame)
        {
            RunVfx();
            Jump();
            Move();
        }
    }

    private void Move()
    {
        //Player is running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = soPlayerConfig.speed * soPlayerConfig.runningSpeedFactor;
            playerAnimator.speed = 1.5f;
        }
        else
        {
            _currentSpeed = soPlayerConfig.speed;
            playerAnimator.speed = 1.0f;
        }

        //Friction
        if (myRigidBody.velocity.x > 0.0f)
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, soPlayerConfig.frictionSpeed), myRigidBody.velocity.y);
        else if (myRigidBody.velocity.x < 0.0f)
            myRigidBody.velocity = new Vector2(Mathf.MoveTowards(myRigidBody.velocity.x, 0, soPlayerConfig.frictionSpeed), myRigidBody.velocity.y);


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            this.playerAnimator.SetBool(animatorKeyForRunning, !isJumping && !isInAir);
            this.transform.DOScaleX(-1, _scaleDuration);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
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
            if (audioHelper != null)
            {
                audioHelper.audioClip = soPlayerConfig.jumpClip;
                audioHelper.PlaySingle();
            }

            isJumping = true;
            myRigidBody.velocity = Vector2.up * soPlayerConfig.jumpForce;
            VFXManager.instance.PlayVfx(VfxType.Jump, this.transform.position);
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

    void playerKill()
    {
        GameManager.instance.SwitchState(States.Death);
        playerAnimator.speed = 1.0f;
        playerAnimator.SetBool(animatorKeyForDeath, true);

        myRigidBody.simulated = false;
    }

    public void RunVfx() 
    {
        //if (isJumping)
        //    runVfx?.Stop();
        //else
        //    runVfx?.Play();

        if (isInAir)
        {
            if (runVfx != null)
            {
                var ps = runVfx.main;
                ps.simulationSpeed = 0f;
            }
        }
        else
        {
            var ps = runVfx.main;
            ps.simulationSpeed = 1f;
        }
    }
}
