using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float SmoothMoveTime = 0;
    public float WalkSpeed = 10f;
   

    [Header("Jump & Gravity")]
    public float grav = -9f;
    public float jumpforce = 2f;

    [Header("Head Bob")]
    [SerializeField] public float WalkBobSpeed = 14f;
    [SerializeField] public float WalkBobAmount = 0.05f;
    public float defaultYpos;

    [Header("Pill Timers")]
    public float SlickPillTimer;
    public float FloatPillTimer;
    public bool PillReadySlick;  
    public bool PillReadyFloat;
    public bool hasSlickPill;
    public bool hasFloatPill;

    [Header("References")]
    public Camera PlayerCam;

   
    private CharacterController controller;
    private Vector3 MoveVector;
    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVelocity;
    private Vector3 playervelocity;
    public bool isgrounded;
    private float timer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultYpos = PlayerCam.transform.localPosition.y;
    }

    void Update()
    {
        UpdateGrounded();
        CharMovement();
        HeadBob();
        

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            JumpPlayer();
        }
        if (FloatPillTimer == 0) 
        {
            grav = -9f;
            jumpforce = 2f;
        }
        if (SlickPillTimer == 0) 
        {
            SmoothMoveTime = 0;
            WalkSpeed = 10;
        }
    }

    public void UpdateGrounded()
    {
       
        Ray ray = new Ray(transform.position, Vector3.down);
        isgrounded = Physics.Raycast(ray, 1.1f);

        if (isgrounded && playervelocity.y < 0)
        {
            playervelocity.y = -2f; 
        }
    }

    public void CharMovement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input.magnitude > 1f)
            input.Normalize();

        MoveVector = transform.TransformDirection(input);
        

        CurrentMoveVelocity = Vector3.SmoothDamp(CurrentMoveVelocity, MoveVector * 10, ref MoveDampVelocity, SmoothMoveTime);

       
        playervelocity.y += grav * Time.deltaTime;

        
        Vector3 totalMovement = CurrentMoveVelocity;
        totalMovement.y = playervelocity.y;

        controller.Move(totalMovement * Time.deltaTime);
    }

    public void JumpPlayer()
    {
        playervelocity.y = Mathf.Sqrt(jumpforce * -2f * grav);
    }

    public void HeadBob()
    {
        if (Mathf.Abs(MoveVector.x) > 0.1f || Mathf.Abs(MoveVector.z) > 0.1f)
        {
            timer += Time.deltaTime * WalkBobSpeed;
            PlayerCam.transform.localPosition = new Vector3(
                PlayerCam.transform.localPosition.x,
                defaultYpos + Mathf.Sin(timer) * WalkBobAmount,
                PlayerCam.transform.localPosition.z
            );
        }
        else
        {
            timer = 0f;
            PlayerCam.transform.localPosition = new Vector3(
                PlayerCam.transform.localPosition.x,
                Mathf.Lerp(PlayerCam.transform.localPosition.y, defaultYpos, Time.deltaTime * WalkBobSpeed),
                PlayerCam.transform.localPosition.z
            );
        }
    }

    public void SlickPill()
    {
        if (SlickPillTimer == 0)
        { 
            PillReadySlick = true;
        }

        if (PillReadySlick)
        {
            hasSlickPill = true;
            WalkSpeed = 30;
            SlickPillTimer = 30f;
            SmoothMoveTime = 4f;
            PillReadySlick = false;
        }

        if (SlickPillTimer > 0)
        {
            SlickPillTimer -= Time.deltaTime;
        }
    }

    public void FloatPill()
    {
        
        if (FloatPillTimer == 0)
        {
           
            PillReadyFloat = true;
        }
        if (PillReadyFloat) 
        {
            hasFloatPill = true;
            grav = -2f;
            jumpforce = 8;
            PillReadyFloat = false;
            FloatPillTimer = 30f;
        }
        if (FloatPillTimer > 0)
        {
            FloatPillTimer -= Time.deltaTime;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SlickPill")) 
        {
            SlickPill();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("FloatPill"))
        {
            Destroy(other.gameObject);
            FloatPill();
        }
    }
}


