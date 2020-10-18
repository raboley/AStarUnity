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

    void Start() {
        PathRequestManager.Requestpath(transform.position, target.position, OnPathFound);
        rb = GetComponent<Rigidbody>();

        // Moves the GameObject using it's transform.
        rb.isKinematic = false;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex ++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            yield return null;
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
