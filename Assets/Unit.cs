using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public Transform target;
    float speed = 20f;
    Vector3[] path;
    int targetIndex;
    public Rigidbody rb;
    public Pathfinding pathfinding;
    public Grid grid;
    public int stuckCount;
    private bool needPath;

    void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
        // gridClass = GetComponent<Grid>();
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        // Moves the GameObject using it's transform.
        rb.isKinematic = false;
        needPath = true;

            if (needPath)
            {
                PathRequestManager.Requestpath(transform.position, target.position, OnPathFound);
                needPath = false;
            }
        
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful, Grid grid)
    {
        print("Finding Path");
        if (pathSuccessful)
        {
            print("Path Found!");
            path = newPath;
            IEnumerator coroutine = FollowPath(grid);
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator FollowPath(Grid grid) {
        Vector3 currentWaypoint = path[0];
        List<Vector3> travelled = new List<Vector3>();
        stuckCount = 0;

        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex ++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            travelled.Add(transform.position);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            
            
            
            // Need something to tell if we are stuck.


            // Check if it is basically the same as the position of now
            // Maybe get an array of positions we have been at, and check the distance from 0 to 5 and see if it is not moving much
            if (travelled.Count > 20)
            {
                int distance = Pathfinding.GetDistance(travelled[travelled.Count - 4], travelled[travelled.Count -1]);
                // print("distance from last point to this is:" + distance);

                if (distance == 0)
                {

                    print("I am stuck!");
                    stuckCount++;
                    if (stuckCount > 5)
                    {
                        // Get the direction we are heading
                        // Vector2 directionNew = new Vector2(path[targetIndex - 1].gridX - path[targetIndex].gridX, path[targetIndex - 1].gridY - path[targetIndex].gridY);
                        // Set the block in front of us as unwalkable
                        
                        print("Update node position: " + transform.position + " to be unwalkable");
                        

                        Node node = grid.AddUnwalkableNode(path[targetIndex + 1]);
                        print("Should be unwalkable now!: " + grid.grid[node.gridX, node.gridY].walkable);
                        path = null;
                        needPath = true;
                        for (int i = 0; i < 5; i++)
                        {
                            transform.position += Vector3.back * Time.deltaTime;
                        }

                        yield break;
                        // gridClass.UpdateNode(transform.position, false);
                    }
                }
                else
                {
                    stuckCount = 0;
                }
            }


            // let it try for a couple of times or seconds

            // mark it as unmovable and re-calculate path.

            yield return null;
        }
    }

    void Update()
    {
 
        if (needPath)
        {
            print("Need to get a path!");
            PathRequestManager.Requestpath(transform.position, target.position, OnPathFound);
            needPath = false;
        }
    }

    void FixedUpdate()
    {
        // // Moves the GameObject to the left of the origin.
        // if (transform.position.x > 3.0f)
        // {
        //     transform.position = new Vector3(-3.0f, 0.0f, 0.0f);
        // }
        //
        // rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime);
    }

    public void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i ++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                } else {
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }
}
