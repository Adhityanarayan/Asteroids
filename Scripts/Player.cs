using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator animator;

    public Joystick joystick;
    public Bullet bulletPrefab;
    public float thrustSpeed = 1f;

    public float bOffset = 0.01f;
    public float trailSetTime = 3.2f;
    public float respawnDelay = 2f;
    public float respawnInvulnerability = 3f;

    public GameObject trail;

    //04D9FF FF7700
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 moveVector = (Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal);
        if(joystick.Horizontal !=0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
            _rigidbody.velocity = new Vector2(joystick.Horizontal * thrustSpeed, joystick.Vertical * thrustSpeed);
        }
    }

    private void OnEnable()
    {
        // Turn off collisions for a few seconds after spawning to ensure the
        // player has enough time to safely move away from asteroids
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        animator.StopPlayback();
        Invoke(nameof(TurnOnCollisions), respawnInvulnerability);
    }
    private void TurnOnCollisions()
    {
        animator.StartPlayback();
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);

        bullet.Projectile(this.transform.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            this.gameObject.SetActive(false);
            GameManager.instance.PlayerDeadEffect(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 position = collision.transform.position;
        trail.gameObject.SetActive(false);
        if (collision.gameObject.tag == "Boundary_Up" && collision.gameObject.tag == "Boundary_Left")
        {
            position.x = collision.transform.position.x - bOffset;
            position.y = -collision.transform.position.y + bOffset;
            collision.transform.position = position;
        }
        if (collision.gameObject.tag == "Boundary_Up" && collision.gameObject.tag == "Boundary_Right")
        {
            position.x = -collision.transform.position.x + bOffset;
            position.y = -collision.transform.position.y + bOffset;
            collision.transform.position = position;
        }
        if (collision.gameObject.tag == "Boundary_Down" && collision.gameObject.tag == "Boundary_Left")
        {
            position.x = -collision.transform.position.x + bOffset;
            position.y = -collision.transform.position.y + bOffset;
            collision.transform.position = position;
        }
        if (collision.gameObject.tag == "Boundary_Down" && collision.gameObject.tag == "Boundary_Right")
        {
            position.x = collision.transform.position.x - bOffset;
            position.y = -collision.transform.position.y + bOffset;
            collision.transform.position = position;
        }
        
        Invoke("TrailSet", trailSetTime);
    }
    void TrailSet()
    {
        trail.gameObject.SetActive(true);
    }
}
