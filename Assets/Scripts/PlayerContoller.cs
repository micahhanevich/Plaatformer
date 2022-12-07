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
    [Min(0)]
    public float FallPower = 20f;

    [SerializeField]
    protected int AirDrag = 2;
    [SerializeField]
    protected int GroundDrag = 8;

    [SerializeField]
    protected Vector3 BaseGravity = Physics.gravity;


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

        if (OnGround) { rbody.drag = GroundDrag; }
        else { rbody.drag = AirDrag; }

        rbody.velocity = new Vector3(Mathf.Clamp(rbody.velocity.x, -Speed * MaxSpeedScale, Speed * MaxSpeedScale), rbody.velocity.y);
        if (rbody.velocity.x < 0.001f && rbody.velocity.x > -0.001f) { rbody.velocity = new Vector3(0, rbody.velocity.y); }

        Debug.Log(Physics.gravity.y);
    }

    private void OnTriggerExit(Collider other)
    {
        OnGround = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "World" && !OnGround)
        {
            OnGround = true;
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
        if (OnGround)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, 1 * JumpPower);
            return true;
        }
        else
        {
            return false;
        }
    }
}
