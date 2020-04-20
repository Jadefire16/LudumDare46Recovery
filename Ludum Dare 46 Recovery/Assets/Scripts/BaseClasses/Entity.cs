using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour, IDamageable
{
    [Header("DefaultData")]
    public EntityData data;
    [Space]
    public LayerMask groundLayer, waterLayer;
    [Space]
    private int health = 3;
    protected int maxHealth = 10;
    protected float speed = 0.7f;
    protected Rigidbody rb;

    private bool isAlive = true;
    public bool IsAlive { get => isAlive; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void LateUpdate()
    {
        WithinBoundsCheck();
    }
    protected virtual void KillEntity() // this yeets the Entity component, conviniently if you do this the entity stops moving
    {
        Debug.Log("Deleted " + data.name);
        Destroy(this.gameObject);
    } // To use base class implementations in an overidden class you must use (in this functions case) base.KillEntity();

    protected virtual void Attack() { } // Used for enemies or players when they should attack
    protected virtual void Move() { } // Use this function to move enemies
    public virtual void TakeDamage(int val) => Health -= val; // Take Damage can be called by using Entity.TakeDamage(amount); even on derived classes
    protected int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                isAlive = false;
                KillEntity();
            }
            else if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    } // use this and not the actual health variable

    void WithinBoundsCheck()
    {
        if (transform.position.y < GameManager.killLimitY) { Destroy(this.gameObject); }
    }

    public int GetHealth() { return Health; }
    public int GetMaxHealth() { return maxHealth; }
    public float GetSpeed() { return speed; }
    public Vector3 GetPosition() { return transform.position; }
    public Quaternion GetRotation() { return transform.rotation; }

    public void InitialSetup(int health, int maxHealth, float speed, Vector3 pos, Quaternion rot)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.speed = speed;
        this.transform.position = pos;
        this.transform.rotation = rot;
    }

    public void FirstTimeLoad(EntityData data)
    {
        this.health = data.health;
        this.maxHealth = data.maxHealth;
        this.speed = data.speed;
        this.transform.position = data.position;
        this.transform.rotation = data.rotation;

        GameManager.instance.SavePlayer(GameManager.instance.saveKey);
    }



}


