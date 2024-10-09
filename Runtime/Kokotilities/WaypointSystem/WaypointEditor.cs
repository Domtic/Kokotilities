using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace TPSBR
{
    [InitializeOnLoad()]
    public class WaypointEditor
    {
        [DrawGizmo(GizmoType.Pickable | GizmoType.Selected |GizmoType.NonSelected)]
        public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
        {
             if((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.yellow * 0.5f;
            }

            Gizmos.DrawSphere(waypoint.transform.position, 0.3f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.Width / 2), waypoint.transform.position - (waypoint.transform.right * waypoint.Width /2f));

            if(waypoint.PreviousWayPoint != null)
            {
                Gizmos.color = Color.red;
                Vector3 offset = waypoint.transform.right * waypoint.PreviousWayPoint.Width / 2f;
                Vector3 offsetTo = waypoint.PreviousWayPoint.transform.right * 
                    waypoint.PreviousWayPoint.Width / 2f;

                Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.PreviousWayPoint.transform.position + offsetTo);
            }

            if(waypoint.NextWayPoint != null)
            {
                Gizmos.color = Color.green;
                Vector3 offset = waypoint.transform.right * -waypoint.Width / 2; ;
                Vector3 offsetTo = waypoint.NextWayPoint.transform.right * -waypoint.Width / 2;
                Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.NextWayPoint.transform.position + offsetTo);
            }

            if(waypoint.m_branches !=  null)
            {
                foreach (Waypoint branch in waypoint.m_branches)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
                }

            }

        }
    }
}
