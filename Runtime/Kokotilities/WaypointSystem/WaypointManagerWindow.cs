using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TPSBR
{
    public class WaypointManagerWindow : EditorWindow
    {
        [MenuItem("Tools/Waypoint editor")]
        public static void Open()
        {
            GetWindow<WaypointManagerWindow>();
        }

        public Transform waypointRoot;
        [Range(0, 2)]
        public float GizmosSize = 0.4f;
        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
            if(waypointRoot ==null)
            {
                EditorGUILayout.HelpBox("Root transform must be selected, please assign a root transform", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                DrawButtons();
                EditorGUILayout.EndVertical();
            }

            obj.ApplyModifiedProperties();

        }

        void DrawButtons()
        {
            if(GUILayout.Button("Create waypoint"))
            {
                CreateWaypoint();
            }
            if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
            {
                if (GUILayout.Button("Add Branch waypoint"))
                {
                    CreateBranch();
                }


                if (GUILayout.Button("Create waypoint Before"))
                {
                    CreateWaypointBefore();
                }
                if (GUILayout.Button("Create waypoint After"))
                {
                    CreateWaypointAfter();
                }
                if (GUILayout.Button("RemoveWaypoint"))
                {
                    RemoveWaypoint();
                }
            }
        }

        private void CreateBranch()
        {
            Waypoint newWaypoint = GenerateNewWaypoint(true);
            Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();

            branchedFrom.m_branches.Add(newWaypoint);
            newWaypoint.transform.position = branchedFrom.transform.position;
            newWaypoint.transform.forward = branchedFrom.transform.forward;

            Selection.activeGameObject = newWaypoint.gameObject;
        }

        void CreateWaypointBefore()
        {
            Waypoint newWaypoint = GenerateNewWaypoint(false);
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            newWaypoint.transform.position = selectedWaypoint.transform.position;
            newWaypoint.transform.forward = selectedWaypoint.transform.forward;

            if(selectedWaypoint.PreviousWayPoint != null)
            {
                newWaypoint.PreviousWayPoint = selectedWaypoint.PreviousWayPoint;
                selectedWaypoint.PreviousWayPoint.NextWayPoint = newWaypoint;
            }

            newWaypoint.NextWayPoint = selectedWaypoint;
            selectedWaypoint.PreviousWayPoint = newWaypoint;
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
            Selection.activeGameObject = newWaypoint.gameObject;
        }
        void CreateWaypointAfter()
        {
            Waypoint newWaypoint = GenerateNewWaypoint(false);
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            newWaypoint.transform.position = selectedWaypoint.transform.position;
            newWaypoint.transform.forward = selectedWaypoint.transform.forward;

            newWaypoint.PreviousWayPoint = selectedWaypoint;

            if (selectedWaypoint.NextWayPoint != null)
            {
                selectedWaypoint.NextWayPoint.PreviousWayPoint = newWaypoint;
                newWaypoint.NextWayPoint = selectedWaypoint.NextWayPoint;
            }
            selectedWaypoint.NextWayPoint = newWaypoint;
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
            Selection.activeGameObject = newWaypoint.gameObject;
        }

        void RemoveWaypoint()
        {
            Waypoint selectedWayPoint = Selection.activeGameObject.GetComponent<Waypoint>();

            if(selectedWayPoint.NextWayPoint != null)
            {
                selectedWayPoint.NextWayPoint.PreviousWayPoint = selectedWayPoint.PreviousWayPoint;
            }
            if (selectedWayPoint.PreviousWayPoint != null)
            {
                selectedWayPoint.PreviousWayPoint.NextWayPoint = selectedWayPoint.NextWayPoint;
                Selection.activeGameObject = selectedWayPoint.PreviousWayPoint.gameObject;
            }

            DestroyImmediate(selectedWayPoint.gameObject);
        }


        void CreateWaypoint()
        {

            Waypoint waypoint = GenerateNewWaypoint(false);
            if(waypointRoot.childCount > 1)
            {
                waypoint.PreviousWayPoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
                waypoint.PreviousWayPoint.NextWayPoint = waypoint;
                waypoint.transform.position = waypoint.PreviousWayPoint.transform.position;
                waypoint.transform.forward = waypoint.PreviousWayPoint.transform.forward;
            }

            Selection.activeGameObject = waypoint.gameObject;

        }

        Waypoint GenerateNewWaypoint(bool isBranch)
        {
            string name = "";
            if (isBranch)
                name = "Branch";
            else
                name = "WayPoint ";
            GameObject waypointObject = new GameObject(name, typeof(Waypoint));
            waypointObject.transform.SetParent(waypointRoot, false);
            return waypointObject.GetComponent<Waypoint>();
        }
    }
}
