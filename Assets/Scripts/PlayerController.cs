using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
public class PlayerController : MonoBehaviour
{

    public float jumpForce = 200;
    public float rotationSpeed = 3;

    public Rigidbody2D rb;
    public Animator animator;

    bool isReady, isDead;

    public AudioClip JumpSound;
    public AudioClip DeathSound; 
    private AudioSource audioSource;


    void Awake()
    {
        GameManager.OnGameStarted += OnGameStarted;
    }
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;

        audioSource = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted; 
    }

    void OnGameStarted()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isReady = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce);
    }

    void Update()
    {
        if(isReady && !isDead)
        {
            float angle;
            float rotSpeed = rotationSpeed;

            if(rb.velocity.y < -2)
            {
                angle = Mathf.Lerp(-90,90,rb.velocity.y);
            }
            else
            {
                angle = 20;
                rotSpeed *= 3;
            }

            Quaternion rotation = Quaternion.Euler(0,0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);

            if (Input.GetMouseButtonDown(0)) {
                PlayJumpSound();
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }

            if(transform.position.y > 8)
            {
                Die();
            }
        }
    }

    private void PlayJumpSound()
    {
        audioSource.clip = JumpSound;
        audioSource.time = 0.2f;
        audioSource.Play();
    }

    private void PlayDeathSound()
    {
        audioSource.clip = DeathSound;
        audioSource.time = 0.2f;
        audioSource.Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pipe"))
        {
            ScoreManager.instance.AddScore();
        }
    }

    void Die()
    {
        if (isDead)
        {
            return;
        }
        PlayDeathSound();
        isDead = true;
        animator.speed = 0;
        transform.DORotate(new Vector3(0, 0, -90), 0.5f);
        GameManager.Instance.GameOver();
    }
}
