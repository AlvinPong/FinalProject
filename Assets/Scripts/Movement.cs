using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 InitialDirection = Vector2.right;
    public Vector2 MovingDirection;
    public bool CheckRight = false;
    public bool CheckLeft = false;
    public float CheckDistance = 1f;
    //public exposed variables for tweaking
    public float Acceleration = 10f;
    public float JumpForce = 50f;
    public float DamageForce = 10f;
    public LayerMask GroundLayerMask;
    public float MaxSlopeAngle = 90f;

    public PhysicsMaterial2D Default;
    public PhysicsMaterial2D FullFriction;
    public PhysicsMaterial2D NoFriction;

    public Cooldown CoyoteTime;

    public bool IsGrounded = true;
    public float GroundCheckRadius = 1f;

    public int JumpCount = 1;
    public int MaxJumpCount = 1;

    public bool IsRunning
    {
        get 
        { 
            return _isRunning; 
        }  
    }
    public bool FlipAnim
    {
        get { return _flipAnim; }
        set { _flipAnim = value; }
    }
    public bool IsJumpingAnim
    {
        get { return _isJumpingAnim; }
    }
    public bool IsFallingAnim
    {
        get { return _isFallingAnim; }
    }
    public bool IsDamaged
    {
        get { return _isDamaged; }
    }
    //protected variables
    protected Rigidbody2D _rigidBody;
    protected Vector2 _inputDirection;

    //protected bool _isGrounded = true;
    protected RaycastHit2D _groundHit;
    protected RaycastHit2D _slopeHit;

    protected float _slopeAngle = 0f;
    protected Vector2 _slopeHitNormal = Vector2.zero;

    protected bool _onSlope = false;
    protected bool _canWalkOnSlope = false;
    protected float _lastSlopeAngle = 0f;

    protected bool _isRunning = false;
    protected bool _flipAnim = false;
    protected bool _isJumpingAnim = false;
    protected bool _isFallingAnim = false;
    protected bool _isJumping = false;
    protected bool _isDamaged = false;
    protected bool _canJump = true;
    protected bool _canDoubleJump = true;

    private Health _health;
    private bool _disableInput = false;

    

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        MovingDirection = InitialDirection; 
        _health = GetComponent<Health>();
        if ( _health != null )
        {
            _health.OnHit += Hit;
            _health.OnHitReset += ResetMove;
        }
        
    }
    private void OnDisable()
    {
        if (_health != null )
        {
            _health.OnHit -= Hit;
            _health.OnHitReset -= ResetMove;
        }
    }
    private void ResetMove()
    {
       _disableInput = false;
    }
    private void Hit(GameObject source)
    {
        float pushHorizontal = 0f;
        if (source != null)
        {
            if(source.transform.position.x < transform.position.x)
            {
                pushHorizontal = DamageForce;
            }
            else
            {
                pushHorizontal = -DamageForce;
            }
        }
        _rigidBody.velocity = Vector2.zero;    
        _rigidBody.velocity = new Vector2(pushHorizontal, DamageForce);

        _disableInput = true;
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (Input.GetKeyDown(KeyCode.F))
        {
            CoyoteTime.StartCooldown();
        }
    }
    private void FixedUpdate()
    {
        CheckGround();
        CheckSlope();
        CheckSides();
        HandleMovement();
        HandleFlip();
    }
    protected virtual void HandleInput()
    {

    }
    protected virtual void HandleMovement()
    {
        
        if (_disableInput)
        {
            //_isDamaged = true;
            _isRunning = false;
            _isJumpingAnim = false;
            _isFallingAnim = false;
            return;
        }
        //_isDamaged = false;
        if (_rigidBody == null)
        {
            return;
        }
        _rigidBody.velocity = new Vector2(_inputDirection.x * Acceleration, _rigidBody.velocity.y);
        if (_rigidBody.velocity.x == 0 || !IsGrounded)
        {
            _isRunning = false;
        }
        else
        {
            _isRunning = true;
        }


        if (_rigidBody.velocity.y <= 0 || IsGrounded == true)
        {
            _isJumpingAnim = false;
        }
        else if (_rigidBody.velocity.y > 0)
        {
            _isJumpingAnim = true;
        }
        if (_rigidBody.velocity.y >= 0 || IsGrounded == true)
        {
            _isFallingAnim = false;
        }
        else if (_rigidBody.velocity.y < 0)
        {
            _isFallingAnim = true;
        }
        //if (!IsGrounded)
        //{
        //    return;
        //}
        //JumpCount = MaxJumpCount;
    }
    protected virtual void HandleFlip()
    {
        if (_inputDirection.x == 0) { return; }
        if (_inputDirection.x > 0) 
        {
            FlipAnim = false;
        }
        else if (_inputDirection.x < 0)
        {
            FlipAnim = true;
        }
    }
    protected virtual void DoJump()
    {
        if (_disableInput)
            return;
        if (_rigidBody == null) return;

        //if (!_isGrounded) return;
        //Debug.Log("Jumping");
        if (!_canJump && !_canDoubleJump)
            return;

        if (CoyoteTime.CurrentProgress == Cooldown.Progress.Finished)
            return;

        _canJump = false;
        _isJumping = true;

        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
        CoyoteTime.StopCooldown();
        if (_canDoubleJump == true)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce * 0.75f);
            _canDoubleJump = false;
            JumpCount--;
        }
        //if (JumpCount != 0){
        //if (IsGrounded)
        //{
        //    _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
        //}
        //if (!IsGrounded) 
        //{
        //    _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce/1.5f);
        //}
        //JumpCount--;
        //}
    }
    protected void CheckSlope()
    {
        _slopeHit = Physics2D.Raycast(transform.position, Vector2.down, 2f, GroundLayerMask);
        if (_slopeHit)
        {
            _slopeHitNormal = _slopeHit.normal;
            _slopeAngle = Vector2.Angle(Vector2.up, _slopeHitNormal);

            if (_slopeAngle != _lastSlopeAngle)
            {
                _onSlope = true;
            }
            if (_slopeAngle < 1)
            {
                _onSlope = false;
            }
            _lastSlopeAngle = _slopeAngle;
            
            //Debug.Log(_canWalkOnSlope);
            //Debug.DrawRay(_slopeHit.point, _slopeHit.normal, Color.cyan, 1f);
            //Debug.Log(Vector2.Angle(Vector2.up,_slopeHit.normal));
        }
        if (_slopeAngle > MaxSlopeAngle)
        {
            _canWalkOnSlope = false;
        }
        else
        {
            _canWalkOnSlope = true;
        }
        if (_onSlope && _canWalkOnSlope && _inputDirection.x == 0)
        {
            _rigidBody.sharedMaterial = FullFriction;
        }
        else
        {
            _rigidBody.sharedMaterial = Default;
        }
        if (_inputDirection.x != 0)
        {
            _rigidBody.sharedMaterial = NoFriction;
        }

    }
    protected void CheckGround()
    {
        //_groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, GroundLayerMask);
        //if (_groundHit)
        //{
        //    _isGrounded = true;
        //}
        //else
        //{
        //    _isGrounded = false;
        //}

        IsGrounded = Physics2D.OverlapCircle(transform.position, GroundCheckRadius, GroundLayerMask);

        if (_rigidBody.velocity.y <= 0f)
        {
            _isJumping = false;
        }
        if (IsGrounded && !_isJumping)
        {
            _canJump = true;

            if (CoyoteTime.CurrentProgress != Cooldown.Progress.Ready)
                CoyoteTime.StopCooldown();

            //DoJump();
        }
        if (IsGrounded == true)
        {
            _canDoubleJump = false;
            JumpCount = MaxJumpCount;
        }
        if (IsGrounded == false && JumpCount == 1)
        {
            _canDoubleJump = true;
        }
        if (!IsGrounded && _isJumping && CoyoteTime.CurrentProgress == Cooldown.Progress.Ready)
            CoyoteTime.StartCooldown();
    }

    protected void CheckSides()
    {
        CheckRight = Physics2D.Raycast(transform.position, Vector2.right,CheckDistance,GroundLayerMask);
        CheckLeft = Physics2D.Raycast(transform.position, Vector2.left,CheckDistance,GroundLayerMask);
        if (CheckRight == true)
        {
            MovingDirection = Vector2.left;
        }
        if (CheckLeft == true)
        {
            MovingDirection = Vector2.right;
        }
    }
}
