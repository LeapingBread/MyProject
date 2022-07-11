using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float jumpHight;
    
    [Header("CheckJump")]
    [SerializeField] Transform checkTransfrom;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField]bool isOnGround;
    [SerializeField]bool isRun;
    [SerializeField]bool isJump;
    [SerializeField] GameObject jumpFX;
    [SerializeField] GameObject landFX;
    [SerializeField] GameObject runFX;
    [Header("PlantBomb")]
    [SerializeField] GameObject bombPerfeb;
    [SerializeField] float plantCD;
    float startTime;
    [SerializeField] Transform bombParent;
    [Header("SoundFX")]
    [SerializeField] SoundName jumpSoundfx;
    [SerializeField] SoundName landSoundfx;
    [SerializeField] SoundName plantBombfx;
    FixedJoystick joystick;
    bool inputDisable;
    bool canJump;


    public bool IsJump { get { return isJump; } }
    public bool IsOnGround { get { return isOnGround; } }
    public bool IsRun { get { return isRun; } }

    Rigidbody2D rigidbody2D;
    PlayerHealth playerHealth;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        joystick = FindObjectOfType<FixedJoystick>();
    }
    private void Start()
    {
        startTime = 3;
    }
    private void OnEnable()
    {
        EventSystem.GameStateEvent += OnGameStateEvent;
    }
    private void OnDisable()
    {
        EventSystem.GameStateEvent -= OnGameStateEvent;
    }
    void OnGameStateEvent(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Pause:
                inputDisable = true;
                break;
            case GameState.Start:
                inputDisable = false;
                break;
        }
    }

    void Update()
    {
        if (playerHealth.IsDead)
        {
            runFX.SetActive(false);
            return;
        }
      // if (Input.GetKeyDown(KeyCode.Space))
        // canJump = true;
        CanPlanBobm();
       // if (Input.GetKey(KeyCode.J))
          //  PlantBomb();
    }
    bool CanPlanBobm()
    {
        startTime += Time.deltaTime;
        if (startTime >= plantCD)
            return true;
        return false;
    }
    public void PlantBomb()// button event
    {
        if(CanPlanBobm() && !inputDisable)
        {
            Instantiate(bombPerfeb, transform.position, Quaternion.identity, bombParent);
            startTime = 0;
            if(plantBombfx != SoundName.None)
            {
                EventSystem.CallPlaySoundEvent(plantBombfx);
            }
        }
    }
    private void FixedUpdate()
    {
        if(playerHealth.IsDead)
        {
            rigidbody2D.velocity = Vector2.zero;
            return;
        }
        CheckOnGround();
        if (!inputDisable)
        {
            PlayerMove();
            PlayerJump();


        }
    }
   
    void PlayerMove()
    {
        // var inputMoveValue = Input.GetAxisRaw("Horizontal"); //keyboard
       var inputMoveValue = joystick.Horizontal;
        rigidbody2D.velocity = new Vector2(inputMoveValue * speed * Time.deltaTime, rigidbody2D.velocity.y);
        if (inputMoveValue != 0)
        {
            isRun = true;
            if (inputMoveValue > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            if (inputMoveValue < 0)
                transform.eulerAngles = new Vector3(0, 180, 0);
            runFX.SetActive(true);
        }
        else
        {
            isRun = false;
            runFX.SetActive(false);
        }
    }
    public void JumpButtonEvnet()//buttong event
    {
        canJump = true;
    }
    void PlayerJump()
    {
        if (isOnGround&& canJump)
        {
            isJump = true;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHight * Time.deltaTime);
            if(jumpSoundfx != SoundName.None)
            {
                EventSystem.CallPlaySoundEvent(jumpSoundfx);
            }
        }
        canJump = false;
    }
    void CheckOnGround()
    {
        isOnGround = Physics2D.OverlapCircle(checkTransfrom.position, checkRadius, groundLayer);
        if(isOnGround)
        {
            isJump = false;
            rigidbody2D.gravityScale = 1;
        }
        else
        {
            rigidbody2D.gravityScale = 4;
        }
    }
    public void JumpFXEnable()
    {
        jumpFX.SetActive(true);
        jumpFX.transform.position = this.transform.position + new Vector3(0.1f, -0.6f, 0);
    }
    public void LandFXEnable()
    {
        landFX.SetActive(true);
        landFX.transform.position = this.transform.position + new Vector3(0.1f, -0.8f, 0);
        if(landSoundfx != SoundName.None)
        {
            EventSystem.CallPlaySoundEvent(landSoundfx);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkTransfrom.position, checkRadius);
    }
    public void WalkingSoundFX() //Animation event
    {
        EventSystem.CallPlaySoundEvent(SoundName.PlayerFootStep);
    }

    
}
