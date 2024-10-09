using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WayPointsManager : SingletonAsComponent<WayPointsManager>
    {
       
        public static WayPointsManager Instance { get { return (WayPointsManager)_Instance; } }

        [SerializeField]
        private List<Waypoint> availableWayPoints;

        public Waypoint GetRandomWayPoint()
        {
            return availableWayPoints[Random.Range(0, availableWayPoints.Count)];
        }

        private void OnValidate()
        {
            availableWayPoints.Clear();
            foreach (Transform t in this.transform)
            {
                availableWayPoints.Add(t.GetComponent<Waypoint>());
            }
        }
    }
