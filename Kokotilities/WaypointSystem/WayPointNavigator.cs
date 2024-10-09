using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImprovedTimers;

public class WayPointNavigator : MonoBehaviour
{

    [SerializeField]
    private Waypoint currentWaypoint;
   
    int direction;
    public void InitNavigation()
    {
        direction = Mathf.RoundToInt(Random.Range(0F, 1F));
        GetNewWayPoint(true);
        GetComponent<Rigidbody>().position = currentWaypoint.GetPosition();
        
        transform.position = currentWaypoint.GetPosition();

        /*controller.Initialize();
        controller.OnNPCDead += EliminateBehavior;
        ticker = new UpdateTicker();
        ticker.OnTick += CustomUpdate;
        ticker.Start();*/
    }

    private void UpdateWaypoint()
    {
        bool shouldBranch = false;
        if (currentWaypoint.m_branches != null && currentWaypoint.m_branches.Count > 0)
        {
            shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
        }

        if (shouldBranch)
            currentWaypoint = currentWaypoint.m_branches[Random.Range(0, currentWaypoint.m_branches.Count - 1)];
        else
        {
            if (direction == 0)
                if (currentWaypoint.NextWayPoint != null)
                    currentWaypoint = currentWaypoint.NextWayPoint;
                else
                {
                    currentWaypoint = currentWaypoint.PreviousWayPoint;
                    direction = 1;
                }

            else
            {
                if (currentWaypoint.PreviousWayPoint != null)
                    currentWaypoint = currentWaypoint.PreviousWayPoint;
                else
                {
                    currentWaypoint = currentWaypoint.NextWayPoint;
                    direction = 0;
                }
            }

        }

       // controller.SetDestination(currentWaypoint.GetPosition());
    }

    /* private Vector3 CheckIfCanSpawn()
     {
         if(Physics.BoxCast(currentWaypoint.GetPosition(), Vector3.one, Vector3.zero, ))
         {

         }
     }*/
    private void EliminateBehavior()
    {
       /* ticker.OnTick -= CustomUpdate;
        ticker.Dispose();*/
    }

    private void OnDestroy()
    {/*
        if (ticker != null)
        {
            ticker.OnTick -= CustomUpdate;
            ticker.Dispose();
        }*/
    }


    void GetNewWayPoint(bool printDebug)
    {
        currentWaypoint = WayPointsManager.Instance.GetRandomWayPoint();
        //controller.SetDestination(currentWaypoint.GetPosition());

        if (printDebug)
            Debug.Log("This object had to get new waypoint" + transform.name + "at position: " + transform.position);

    }


}


