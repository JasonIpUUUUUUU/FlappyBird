using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool lost, spawning;
    public GameObject obstaclePrefab;
    public List<GameObject> activeObstacles;
    public float scrollSpeed, timeDelay, spawnX, minY, maxY;

    public void removeObject(GameObject obstacle)
    {
        activeObstacles.Remove(obstacle);
    }

    public IEnumerator spawnObstacle()
    {
        GameObject newObstacle = Instantiate(obstaclePrefab);
        newObstacle.transform.position = new Vector3(spawnX, Random.Range(minY, maxY));
        newObstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(-scrollSpeed, 0);
        newObstacle.GetComponent<Obstacles>().manager = this;
        activeObstacles.Add(newObstacle);
        yield return new WaitForSeconds(timeDelay);
        if (!lost)
        {
            spawning = false;
            StartCoroutine(spawnObstacle());
        }
    }

    public void LoseGame()
    {
        lost = true;
        foreach(GameObject obstacle in activeObstacles)
        {
            obstacle.GetComponent<Obstacles>().loseGame();
        }
    }
}
