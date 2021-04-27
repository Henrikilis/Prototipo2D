using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200;
    public float nxtwaypointdistance = 0.1f;

    Path path;
    int currentWaypoint = 0;
    bool reachendofpath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("updatePath", 0f, .5f);
    }

    void updatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);

    }
    
    void FixedUpdate()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachendofpath = true;
            return;
        } else
        {
            reachendofpath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nxtwaypointdistance)
        {
            currentWaypoint++;
        }
           

        if (rb.velocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(-2f, 2f, 2f);

        }
        else if (rb.velocity.x <= 0.1f)
        {
            transform.localScale = new Vector3(2f, 2f, 2f);

        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;

        }


    }

}
