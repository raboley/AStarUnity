using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathRequestManager instance;
    Pathfinding pathfinding;
    bool isProcessingPath;


    void Awake() {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }


    public static void Requestpath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool, Grid> callback)
    {
        PathRequest newrequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newrequest);
        instance.TryProcessNext();
    }

    void TryProcessNext() {
        if (!isProcessingPath && pathRequestQueue.Count > 0) {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success, Grid grid) {
        currentPathRequest.callback(path, success, grid);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool, Grid> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool, Grid> _callback) {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
