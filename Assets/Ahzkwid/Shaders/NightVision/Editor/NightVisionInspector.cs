#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class NightVisionInspector : ShaderGUI
{
    public enum _RM
    {
        Default, Overlay
    }
    public enum _CM
    {
        Back, None, Front
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {

        MaterialProperty _RenderMode = FindProperty(nameof(_RenderMode), properties);
        MaterialProperty _MainTex = FindProperty(nameof(_MainTex), properties);
        MaterialProperty _RenderRange = FindProperty(nameof(_RenderRange), properties);
        MaterialProperty _Hue = FindProperty(nameof(_Hue), properties);
        MaterialProperty _Brightness = FindProperty(nameof(_Brightness), properties);
        MaterialProperty _BBC = FindProperty(nameof(_BBC), properties);
        MaterialProperty _Outline = FindProperty(nameof(_Outline), properties);
        MaterialProperty _OutlineWidth = FindProperty(nameof(_OutlineWidth), properties);
        MaterialProperty _UDL = FindProperty(nameof(_UDL), properties);
        MaterialProperty _CullingMode = FindProperty(nameof(_CullingMode), properties);



        // render the default gui

        Material targetMat = materialEditor.target as Material;

        // see if redify is set, and show a checkbox
        EditorGUI.BeginChangeCheck();
        {

            //base.OnGUI(materialEditor, properties);
            materialEditor.ShaderProperty(_RenderMode, new GUIContent("Render Mode"));

            EditorGUILayout.Space();
            GUILayout.Label("Main Maps", EditorStyles.boldLabel);
            EditorGUI.indentLevel = 1; //얼마나 오른쪽으로 이동시킬지
            {
                switch ((_RM)_RenderMode.floatValue)
                {
                    case _RM.Default:
                        materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture", "Default Texture"), _MainTex);
                        break;
                    case _RM.Overlay:
                        materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture (Overlay UI)", "Overlay UI Texture"), _MainTex);
                        materialEditor.ShaderProperty(_RenderRange, new GUIContent("Render Range"));
                        materialEditor.ShaderProperty(_UDL, "UI Depth Level (VR Only)");
                        break;
                    default:
                        break;
                }
            }
            EditorGUI.indentLevel = 0;


            EditorGUILayout.Space();
            GUILayout.Label("Color", EditorStyles.boldLabel);
            {
                EditorGUI.indentLevel = 1; //얼마나 오른쪽으로 이동시킬지
                {
                    materialEditor.ShaderProperty(_Hue, "Hue");
                    materialEditor.ShaderProperty(_Brightness, "Brightness");
                    materialEditor.ShaderProperty(_BBC, "Blend Black Color");
                }
                EditorGUI.indentLevel = 0;
            }


            EditorGUILayout.Space();
            GUILayout.Label("OutLine", EditorStyles.boldLabel);
            {
                EditorGUI.indentLevel = 1; //얼마나 오른쪽으로 이동시킬지
                {
                    materialEditor.ShaderProperty(_Outline, "Outline Brightness");
                    materialEditor.ShaderProperty(_OutlineWidth, "OutlineWidth");
                }
                EditorGUI.indentLevel = 0;
            }




            EditorGUILayout.Space();
            EditorGUILayout.Space();
            materialEditor.ShaderProperty(_CullingMode, "Culling Mode");


            //EditorGUILayout.Space();
            //EditorGUILayout.Space();
            materialEditor.RenderQueueField();



            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUILayout.Label("Advanced Oprions", EditorStyles.boldLabel);
            {
                materialEditor.EnableInstancingField();
                materialEditor.DoubleSidedGIField();
            }


        }
        if (EditorGUI.EndChangeCheck())
        {
        }
    }
}

#endif