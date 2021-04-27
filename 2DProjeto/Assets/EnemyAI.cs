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

   public  Seeker seeker;
   public  Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("updatePath", 0f, .5f);

       // seeker.StartPath(rb.position, target.position, OnPathComplete);
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
            currentWaypoint++;

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-2f, 2f, 2f);

        }
        else if (force.x <= 0.01f)
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
