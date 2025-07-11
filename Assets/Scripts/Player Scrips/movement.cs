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
    private float slickLifetime;
    private float floatLifetime;

    public bool slickEffectStarted = false;
    public bool floatEffectStarted = false;
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
    private CheckPointManager checkPointManager;

    [Header("User Interface")]
    public GameObject slickIcon;
    public GameObject floatIcon;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (checkPointManager == null)
        {
            checkPointManager = FindAnyObjectByType<CheckPointManager>();
        }
        defaultYpos = PlayerCam.transform.localPosition.y;
        slickLifetime = SlickPillTimer;
        floatLifetime = FloatPillTimer;

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

        SlickPill();
        FloatPill();

        //print(SlickPillTimer);  
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
        if (hasSlickPill && !slickEffectStarted && PillReadySlick)
        {
            WalkSpeed = 30;
            SmoothMoveTime = 4f;
            //SlickPillTimer = 3f;
            slickEffectStarted = true;
           
        }

        if (slickEffectStarted)
        {
            SlickPillTimer -= Time.deltaTime;

            if (SlickPillTimer <= 0f)
            {
               
                WalkSpeed = 10;
                SmoothMoveTime = 0f;
                hasSlickPill = false;
                slickEffectStarted = false;
                SlickPillTimer = slickLifetime;
                PillReadySlick = false;
            }
        }
    }

    public void FloatPill()
    {
        
       
        if (hasFloatPill && !floatEffectStarted && PillReadyFloat) 
        {
           
            grav = -2f;
            jumpforce = 8f;
            //FloatPillTimer = 3f;
            floatEffectStarted = true;
        }

        if (floatEffectStarted)
        {
            FloatPillTimer -= Time.deltaTime;

            if (FloatPillTimer <= 0)
            {
                grav = -9f;
                jumpforce = 2f;
                hasFloatPill = false;
                floatEffectStarted = false;
                FloatPillTimer = floatLifetime;
                PillReadyFloat = false;
            }
        }

       


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SlickPill")) 
        {
            hasSlickPill = true;
            PillReadySlick = true;
            SlickPill();
            Destroy(other.gameObject);
            slickIcon.SetActive(true);
        }
        if (other.gameObject.CompareTag("FloatPill"))
        {
            hasFloatPill = true;
            PillReadyFloat = true;
            FloatPill();
            Destroy(other.gameObject);
            floatIcon.SetActive(true);
        }
        if (other.gameObject.CompareTag("Spikes")) 
        {
            checkPointManager.Respawn();
        }
        if (other.gameObject.CompareTag("Crusher"))
        {
            checkPointManager.Respawn();
        }
    }
}


