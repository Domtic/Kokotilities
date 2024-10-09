using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Waypoint : MonoBehaviour
    {
        [SerializeField]
        private Waypoint m_previousWayPoint;
        [SerializeField]
        private Waypoint m_nextWayPoint;

        [SerializeField, Range(0, 5)]
        private float m_width = 1f;

        public List<Waypoint> m_branches = new List<Waypoint>();

        [Range(0, 1f)]
        public float branchRatio = 0.5f;
        public Waypoint PreviousWayPoint { get => m_previousWayPoint; set => m_previousWayPoint = value; }
        public Waypoint NextWayPoint { get => m_nextWayPoint; set => m_nextWayPoint = value; }
        public float Width { get => m_width; set => m_width = value; }

        public Vector3 GetPosition()
        {
            Vector3 minBound = transform.position + transform.right * Width / 2f;
            Vector3 maxBound = transform.position - transform.right * Width / 2f;

            return Vector3.Lerp(minBound, maxBound, Random.Range(0, 1f));
        }


    }
