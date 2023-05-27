using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackwaypoints : MonoBehaviour
{
    public Color lineColor;
    [Range(0, 1)] public float SphereRadius;
    public List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        Transform[] path = GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();
        for(int i = 1; i < path.Length; i++)
        {
            nodes.Add(path[i]);
        }
        for( int i = 0; i < nodes.Count; i++)
        {
            Vector3 curentWaypoint = nodes[i].position;
            Vector3 previusWaypoint = Vector3.zero;

            if (i != 0) previusWaypoint = nodes[i - 1].position;
            else if (i == 0) previusWaypoint = nodes[nodes.Count - 1].position;

            Gizmos.DrawLine(previusWaypoint, curentWaypoint);
            Gizmos.DrawSphere(curentWaypoint, SphereRadius);
        }
    }
}
