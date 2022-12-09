using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [Header("Internally Defined")]
    [SerializeField]
    protected Rigidbody rbody;

    public bool InputMoveThisFrame { get; private set; }

    public bool InputJumpThisFrame { get; private set; }

    private Vector3 _poslastframe;
    public bool Moved { get { if (_poslastframe != rbody.position) { return false; } else { return true; } } }

    public bool OnGround { get; private set; }

    public bool Falling { get; private set; }


    public int Jumps { get; private set; } = 1;

    private bool CanJump;

    public bool JumpLocked;

    private int JumpDelay = -1;

    public int TimeOffGround;

    // public bool StartFalling { get; private set; }


    [Header("Settings")]
    [SerializeField]
    [Min(0)]
    public float Speed = 1f;

    [SerializeField]
    [Range(1, 5)]
    protected int MaxSpeedScale = 3;

    [SerializeField]
    public float JumpPower = 3f;

    [SerializeField]
    [Min(1)]
    protected int MaxJumps = 1;

    [SerializeField]
    [Min(0)]
    public float FallPower = 20f;

    [SerializeField]
    protected int AirDrag = 2;

    [SerializeField]
    protected int GroundDrag = 8;

    [SerializeField]
    protected int CoyoteTimer = 5;

    [SerializeField]
    [Min(0)]
    public int MaxJumpDelay = 8;

    [SerializeField]
    protected Vector3 BaseGravity = Physics.gravity;

    [Header("Animation Settings")]
    [SerializeField]
    public GameObject JumpPrefab;
    [SerializeField]
    [Range(-1, 1)]
    protected float JumpEffectOffset = -0.15f;


    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!OnGround && rbody.velocity.y < 0f) { Falling = true; }
        else { Falling = false; }

        if (Falling && Physics.gravity == BaseGravity) { Physics.gravity += new Vector3(0, -FallPower, 0); }
        else if (!Falling && Physics.gravity != BaseGravity) { Physics.gravity = BaseGravity; }

        if (OnGround) { rbody.drag = GroundDrag; }
        else { rbody.drag = AirDrag; }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) {
            InputMoveThisFrame = Move_Left();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            InputMoveThisFrame = Move_Right();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            InputJumpThisFrame = Jump();
        }

        if (!CanJump) { TimeOffGround += 1; }
        else { TimeOffGround = 0; }

        if (JumpDelay >= 0 && JumpDelay < MaxJumpDelay) { JumpDelay += 1; }
        else { JumpDelay = -1; }

        rbody.velocity = new Vector3(Mathf.Clamp(rbody.velocity.x, -Speed * MaxSpeedScale, Speed * MaxSpeedScale), rbody.velocity.y);
        if (rbody.velocity.x < 0.001f && rbody.velocity.x > -0.001f) { rbody.velocity = new Vector3(0, rbody.velocity.y); }
    }

    private void OnTriggerExit(Collider other)
    {
        OnGround = false;
        CanJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "World" && !OnGround)
        {
            OnGround = true;
            CanJump = true;
            rbody.drag = GroundDrag;
            Jumps = MaxJumps;
        }
    }

    protected bool Move_Left()
    {
        rbody.velocity += new Vector3 (-1 * Speed, 0);
        return true;
    }

    protected bool Move_Right()
    {
        rbody.velocity += new Vector3(1 * Speed, 0);
        return true;
    }

    protected bool Jump()
    {
        if ((OnGround && CanJump && !JumpLocked && JumpDelay == -1) || (TimeOffGround <= CoyoteTimer && !JumpLocked && Jumps > 0 && JumpDelay == -1) || (!OnGround && !CanJump && Jumps > 0 && Jumps < MaxJumps && JumpDelay == -1))
        {
            rbody.drag = AirDrag;
            rbody.velocity = new Vector3(rbody.velocity.x, 1 * JumpPower);
            Instantiate(JumpPrefab, new Vector3(transform.position.x, transform.position.y - 0.15f), transform.rotation);
            CanJump = false;
            Jumps -= 1;
            JumpDelay += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
}
