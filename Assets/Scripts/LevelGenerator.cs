using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] objects;
    GameObject item;

    public int timeTillRespawn = 60;
    float collisionTime = 0;
    bool destroyed = true;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        item = Instantiate(objects[rand], transform.position, Quaternion.Euler(-45, 0 , 0));
        destroyed = false;
    }

    void Update()
    {
        if (!item && !destroyed)
        {
            destroyed = true;
            collisionTime = Time.time;
        }
        if (Time.time - collisionTime >= timeTillRespawn && destroyed)
        {
            Start();
        }
    }
}
