using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SceneManagment is used to load different scenes as the name implies.  It is useful for loading new levels or resetting levels
using UnityEngine.SceneManagement;

//TMPro stands for TextMeshPro and handles text
using TMPro;


public class PlayerController : MonoBehaviour
{
    public bool started, dead;
    public int currentScore;
    public float jumpForce;
    public Vector2 spawnPos;
    public Manager manager;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        rb2d = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        started = false;
        dead = false;
        transform.position = spawnPos;
        rb2d.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dead)
            {
                rb2d.velocity = Vector2.zero;
                if (!started)
                {
                    StartCoroutine(manager.spawnObstacle());
                    rb2d.isKinematic = false;
                    started = true;
                }
                rb2d.AddForce(new Vector2(0, jumpForce));
            }
            else
            {
                SceneManager.LoadScene("FlappyBird");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            dead = true;
            manager.LoseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            currentScore++;
            scoreText.text = "Score: " + currentScore;
        }
    }
}
