using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor {
    private SerializedObject m_object;
    Weapon weapon;
    bool editFormPos;
    public void OnEnable()
    {
        m_object = new SerializedObject(target);
        weapon = (Weapon)target;
    }
    public override void OnInspectorGUI()
    {
        m_object.Update();

        EditorGUILayout.PropertyField(m_object.FindProperty("spawnObj"));
        EditorGUILayout.PropertyField(m_object.FindProperty("shootCooldown"));
        EditorGUILayout.PropertyField(m_object.FindProperty("repeatCooldown"));
        EditorGUILayout.PropertyField(m_object.FindProperty("type"));
        EditorGUILayout.PropertyField(m_object.FindProperty("randomAnglePreObj"));
        EditorGUILayout.PropertyField(m_object.FindProperty("parentShootOrigin"));
        if (!(weapon.type == WeaponType.Single)) EditorGUILayout.PropertyField(m_object.FindProperty("randomAngleGroup"));
        if (weapon.type==WeaponType.Circle)
        {
            EditorGUILayout.LabelField("Circle");
            EditorGUI.indentLevel = 2;
            EditorGUILayout.PropertyField(m_object.FindProperty("circleRight"));
            EditorGUILayout.PropertyField(m_object.FindProperty("circlePatternCount"));
            EditorGUILayout.Slider(m_object.FindProperty("maxAngle"),0,360);
            EditorGUILayout.PropertyField(m_object.FindProperty("patternPerShoot"));
            EditorGUI.indentLevel = 0;
        }
        if (weapon.type == WeaponType.Form)
        {
            EditorGUILayout.LabelField("Form");
            EditorGUI.indentLevel = 2;
            
            EditorGUILayout.PropertyField(m_object.FindProperty("formPositionList"));
            //if (weapon.formPositionList.Count == 0) { EditorGUILayout.HelpBox("No Position in the list",MessageType.Warning); }
            GUI.color = Color.white;
            EditorGUILayout.PropertyField(m_object.FindProperty("formPositionCount"));

            for (int i = 0; i < weapon.formPositionCount; i++)
            {
                if(weapon.formPositionList.Count<=i)weapon.formPositionList.Add(new Vector2(1, 0));
            }
            for (int i = weapon.formPositionCount; i < weapon.formPositionList.Count; i++)
            {
                weapon.formPositionList.RemoveAt(weapon.formPositionCount);
            }
            if(editFormPos)
            {
                if (GUILayout.Button("Stop edit"))
                {
                    editFormPos = false;
                }
                if (GUILayout.Button("Snap Positions"))
                {
                    
                    for (int i = 0; i < weapon.formPositionList.Count; i++)
                    {
                        Vector2 pos = weapon.formPositionList[i];
                        Debug.Log(pos);
                        pos = new Vector2(Mathf.Round(pos.x * 2f) * 0.5f, Mathf.Round(pos.y * 2f) * 0.5f);
                        weapon.formPositionList[i] = pos;
                    }
                }
            }
            else
            {
                if (GUILayout.Button("Edit Position"))
                {
                    editFormPos = true;
                }
            }
            
            EditorGUILayout.PropertyField(m_object.FindProperty("formCircleDir"));
            EditorGUI.indentLevel = 0;

        }

        EditorGUILayout.PropertyField(m_object.FindProperty("clipSize"));
        EditorGUILayout.PropertyField(m_object.FindProperty("ammoPerShoot"));
        EditorGUILayout.PropertyField(m_object.FindProperty("reloadTime"));
        EditorGUILayout.PropertyField(m_object.FindProperty("ammoCount"));
        //Rect bar = GUILayoutUtility.GetRect(10, 10);
        //EditorGUI.ProgressBar(bar, .5f, "Test");
        m_object.ApplyModifiedProperties();
    }
    void OnSceneGUI()
    {
        if(editFormPos)
        {
            for (int i = 0; i < weapon.formPositionList.Count; i++)
            {
                Vector3 result = Handles.PositionHandle(weapon.ShootOrigin.TransformPoint(weapon.formPositionList[i]), Quaternion.identity);
                weapon.formPositionList[i] = weapon.ShootOrigin.InverseTransformPoint(result);
            }
        }
        
    }
}
