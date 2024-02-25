using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public float Xlimit;
    public Manager manager;
    public Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < Xlimit)
        {
            removeObject();
        }
    }

    public void removeObject()
    {
        manager.removeObject(gameObject);
        Destroy(gameObject);
    }

    public void loseGame()
    {
        rb2d.velocity = Vector2.zero;
    }
}
