using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathManagerEditor : Editor
{
    [SerializeField] private PathManager _pathManager;

    [SerializeField] private List<Waypoint> _path;
    private List<int> toDelete;

    private Waypoint selectedPoint;
    bool doRepaint = true;

    private void OnSceneGUI()
    {
        _path = _pathManager.GetPath();
        DrawPath(_path);
    }

    private void OnEnable()
    {
        _pathManager = target as PathManager;
        toDelete = new List<int>();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _path = _pathManager.path;

        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Path");

        DrawGUIFOrPoints();

        if (GUILayout.Button("Add point to path"))
            _pathManager.CreateAddPoint();

        EditorGUILayout.EndVertical();
        SceneView.RepaintAll();
    }

    private void DrawGUIFOrPoints()
    {
        if(_path != null && _path.Count > 0)
        {
            for (int i = 0; i < _path.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                Waypoint p = _path[i];
                Vector3 oldPos = p.Pos;
                Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                if (EditorGUI.EndChangeCheck())
                    p.SetPos(newPos);

                if (GUILayout.Button("-", GUILayout.Width(25)))
                    toDelete.Add(i);

                EditorGUILayout.EndHorizontal();
                    
            }
        }
        if (toDelete.Count >0)
        {
            foreach (int i in toDelete)
                _path.RemoveAt(i);

            toDelete.Clear();
        }
    }

    public void DrawPath(List<Waypoint> path)
    {
        if(path != null)
        {
            int current = 0;
            foreach (Waypoint wp in path)
            {
                doRepaint = DrawPoint(wp);
                int next = (current - 1) % path.Count;
                Waypoint wpNext = path[next];

                DrawPathLine(wp, wpNext);
                current++;
            }
            if (doRepaint) Repaint();
        }
    }

    public void DrawPathLine(Waypoint p1, Waypoint p2)
    {
        Color c = Handles.color;
        Handles.color = Color.gray;
        Handles.DrawLine(p1.Pos, p2.Pos);
        Handles.color = c;
    }

    public bool DrawPoint(Waypoint wp)
    {
        bool isChanged = false;

        Vector3 currPos = wp.Pos;
        float handlesSize = HandleUtility.GetHandleSize(currPos);

        if(Handles.Button(currPos, Quaternion.identity, 0.25f * handlesSize, 0.25f * handlesSize, Handles.SphereHandleCap))
        {
            isChanged = true;
            selectedPoint = wp;
        }
        return isChanged;
    }
}
