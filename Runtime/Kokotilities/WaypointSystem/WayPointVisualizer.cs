using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSBR
{
    public class WayPointVisualizer : MonoBehaviour
    {

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}
