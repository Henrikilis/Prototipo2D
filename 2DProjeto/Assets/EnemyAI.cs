using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200;
    public float nxtwaypointdistance = 3;

    Path path;
    int currentWaypoint = 0;
    bool reachendofpath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    
    void Update()
    {
        
    }

    void OnPathComplete(Path p)
    {


    }

}
