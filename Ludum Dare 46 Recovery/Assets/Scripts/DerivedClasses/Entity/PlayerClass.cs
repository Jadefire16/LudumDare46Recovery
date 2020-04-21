using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : Entity
{
    public static PlayerClass instance;

    private float jumpForce = 6f;
    public Image[] fire;
    public GameObject model;
    public Sprite fireLit, fireUnlit;
    public int fireVal;
    bool canMove = true;
    bool canJump = true;
   [SerializeField] Vector3 inputVec = Vector3.zero;
    AudioManager audioManager;
    bool isGrounded;

    public Transform lastCheckPoint;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void Start()
    {
        base.Start();
        canJump = true;
        audioManager = AudioManager.instance;
    }

    private void Update() // quick movement 
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f, groundLayer, QueryTriggerInteraction.Ignore);
        bool water = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitWater, 1f, waterLayer, QueryTriggerInteraction.Ignore);
        if (water)
        {
            if (lastCheckPoint)
                transform.position = lastCheckPoint.position;
            else
                transform.position = data.position;
            TakeDamage(1);
        }

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        if (inputVec.magnitude >= 1f)
        {
            inputVec.Normalize();
        }
        inputVec = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * inputVec;

        if (Input.GetButton("Jump"))
        {
            if (canJump && isGrounded)
                Jump();
        }
        
       
        fireVal = maxHealth;
        for (int i = 0; i < fire.Length; i++)
        {
            if (i < this.GetHealth())
            {
                fire[i].sprite = fireLit;
            }
            else
            {
                fire[i].sprite = fireUnlit;
            }

            if (i < fireVal)
            {
                fire[i].enabled = true;
            }
            else
            {
                fire[i].enabled = false;
            }

        }
        if (this.GetHealth() <= 0)
        {
            fire[0].sprite = fireUnlit;
        }
        
        HandleSFX();
    }

    public override void TakeDamage(int val)
    {
        base.TakeDamage(val);
        audioManager.Play("Take_Damage", true, 7, 0.2f, true, 0.5f, 0);
    }

    private void HandleSFX()
    {
        if (Health <= 3) { audioManager.Play("Player_Move_Injured", true,7,0.2f,true,0.5f,0); }else{ audioManager.Stop("Player_Move_Injured", true); }
        if(Health == 10) { audioManager.Play("Max_Lives", true, 7, 0.2f, true, 0.5f, 0); } else { audioManager.Stop("Max_Lives", true); }
    }

    private void FixedUpdate() // call movement every fixed update
    {
        if (canMove)
        {
            Move();
        }
    }
   
    protected override void KillEntity()
    {
        canMove = false;
        canJump = false;
        audioManager.Play("Player_Death", true);
    }
    protected override void Attack() // pretty straight forward, make the player lose a life and attack
    {
        Health--;
        //add shooting
        StartCoroutine(Wait(2));
    }

    void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
        audioManager.ForcePlay("Player_Jump", true);
        StartCoroutine(Wait(1.5f));
    }

    protected override void Move() // Moves player using rb on z and z axis (not y)
    {
        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10f, groundLayer, QueryTriggerInteraction.Ignore);

        Vector3 velocity = -rb.velocity;
        velocity.y = 0;

        rb.AddForce(velocity * 0.2f, ForceMode.VelocityChange);

        rb.AddForce(inputVec * speed, ForceMode.VelocityChange);

        if(isGrounded)transform.up = Vector3.Lerp(transform.up, hit.normal, 10 * Time.fixedDeltaTime);
        Debug.DrawRay(Vector3.one, hit.normal * 5, Color.red);
        Debug.Log("Called");
    }


    private void Interact(Burnable burnable)
    {
        burnable.UseObject(this);
    }

    IEnumerator Wait(float sec)
    {
        canJump = false;
        yield return new WaitForSeconds(sec);
        canJump = true;
    }

}
