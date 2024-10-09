using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class PerformanceTestController : MonoBehaviour
{
    public int NumberOfAgents;
    public bool useCustomUpdates;

    public GameObject ConventionalUpdate;
    public GameObject CustomUpdate;

    public Transform ConventionalTransform;
    public Transform CustomTransform;

    [Button]
    public void InstantiateUpdates()
    {
        if (useCustomUpdates)
        {
            for (int x = 0; x < NumberOfAgents; x++)
            {
                GameObject.Instantiate(CustomUpdate, CustomTransform);
            }

        }
        else
        {
            for (int x = 0; x < NumberOfAgents; x++)
            {
                GameObject.Instantiate(ConventionalUpdate, ConventionalTransform);
            }
        }
    }

    [Button]
    public void PauseCustomTicks()
    {
        foreach (Transform t in CustomTransform)
        {
            t.GetComponent<KokoUpdate>().Pause();
        }

    }


    [Button]
    public void StopCustomTicks()
    {
        foreach (Transform t in CustomTransform)
        {
            t.GetComponent<KokoUpdate>().Stop();
        }

    }

    [Button]
    public void ResumeCustomTicks()
    {
        foreach (Transform t in CustomTransform)
        {
            t.GetComponent<KokoUpdate>().Resume();
        }
    }


}

