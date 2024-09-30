using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoint : MonoBehaviour
{
    [field: SerializeField] public Vector3 Pos { get; private set; }

    public Waypoint()
    {
        Pos = Vector3.zero;
    }

    public void SetPos(Vector3 newPos)
    {
        Pos = newPos;
    }
}
