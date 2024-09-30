using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector, SerializeField]
    public List<Waypoint> path;

    public List<Waypoint> GetPath()
    {
        if (path == null)
            path = new List<Waypoint>();

        return path;
    }

    public void CreateAddPoint()
    {
        Waypoint go = new Waypoint();
        path.Add(go);
    }
}
