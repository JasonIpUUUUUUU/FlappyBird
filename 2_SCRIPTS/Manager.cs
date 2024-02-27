using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool lost, spawning;

    //A prefab is essentially a pre-made game object that you can repeatedly clone. Another good example of where you would use one is with bullets and projectiles
    public GameObject obstaclePrefab;

    //This keeps track of all the projectiles currently on screen, this is useful as to stop all obstacles from moving once the game ends
    public List<GameObject> activeObstacles;

    public float scrollSpeed, timeDelay, spawnX, minY, maxY;
    

    public void removeObject(GameObject obstacle)
    {
        activeObstacles.Remove(obstacle);
    }

    //An IEnumerator is a couroutine (a block of code) that runs when you call it and has the ability to keep track of time, allowing for functions like WaitForSeconds(1)
    public IEnumerator spawnObstacle()
    {
        //creates a new obstacle gameObject, then passes in values which weren't pre-set.
        GameObject newObstacle = Instantiate(obstaclePrefab);
        newObstacle.transform.position = new Vector3(spawnX, Random.Range(minY, maxY));
        newObstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(-scrollSpeed, 0);
        newObstacle.GetComponent<Obstacles>().manager = this;
        activeObstacles.Add(newObstacle);
        yield return new WaitForSeconds(timeDelay);
        //only repeats the coroutine if you havent lost yet.
        if (!lost)
        {
            spawning = false;
            StartCoroutine(spawnObstacle());
        }
    }

    //iterates through all obstacles on screen and makes them stop
    public void LoseGame()
    {
        lost = true;
        foreach(GameObject obstacle in activeObstacles)
        {
            obstacle.GetComponent<Obstacles>().loseGame();
        }
    }
}
