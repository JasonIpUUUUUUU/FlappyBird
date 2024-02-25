using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool started, dead;
    public float jumpForce;
    public Vector2 spawnPos;
    public Manager manager;

    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
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
}
