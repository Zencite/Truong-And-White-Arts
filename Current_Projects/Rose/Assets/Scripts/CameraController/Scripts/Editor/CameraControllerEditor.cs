// CameraControllerEditor.cs - By Jimbob Games 2019.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;


namespace Bizniz
{
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : Editor
    {
        GUIStyle boxStyle;
        GUIStyle boxStyle2;

        CameraController myTarget;
        SerializedObject serializedObj;
        Texture LogoTexture;


        //
        SerializedProperty Camera_Profile;
        SerializedProperty Camera_ToUse;
        SerializedProperty UI_Camera;

        SerializedProperty FollowTarget;
        SerializedProperty TargetOffset;


        void OnEnable()
        {
            myTarget = (CameraController)target;
            serializedObj = new SerializedObject(myTarget);
            LogoTexture = Resources.Load("Art/CameraController_Logo") as Texture;

            //

            Camera_Profile = serializedObj.FindProperty("Camera_Profile");
            Camera_ToUse = serializedObj.FindProperty("Camera_ToUse");
            UI_Camera = serializedObj.FindProperty("UI_Camera");

            FollowTarget = serializedObj.FindProperty("FollowTarget");
            TargetOffset = serializedObj.FindProperty("TargetOffset");
        }

        public override void OnInspectorGUI()
        {
            myTarget = (CameraController)target;

#if UNITY_5_6_OR_NEWER
            serializedObj.UpdateIfRequiredOrScript();
#else
			serializedObj.UpdateIfDirtyOrScript ();
#endif

            //Set up the box style
            if (boxStyle == null)
            {
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
                boxStyle.fontStyle = FontStyle.Bold;
                boxStyle.alignment = TextAnchor.UpperLeft;
            }

            if (boxStyle2 == null)
            {
                boxStyle2 = new GUIStyle(GUI.skin.label);
                boxStyle2.normal.textColor = GUI.skin.label.normal.textColor;
                boxStyle2.fontStyle = FontStyle.Bold;
                boxStyle2.alignment = TextAnchor.UpperLeft;
            }



            // Begin
            GUILayout.BeginVertical("", boxStyle);
            GUILayout.Space(10);



            //
            GUILayout.BeginVertical("", boxStyle2);
            if (LogoTexture != null)
                GUILayout.Label(LogoTexture, EditorStyles.centeredGreyMiniLabel);
            else
                EditorGUILayout.LabelField("CAMERA CONTROLLER!", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            // 
            GUILayout.BeginVertical("", boxStyle2);
            EditorGUILayout.EndVertical();

            //  



            //            

            GUILayout.BeginVertical("", boxStyle);
            GUILayout.Space(15);

            //

            if (myTarget.ShowHelp)
            {
                EditorGUILayout.HelpBox
                ("CAMERA PROFILE  -:The Profile to be used. Settings are taken from the Profile and are Required!. "
                + "\n \n \n" +
                "SET CAMERA TO USE  -:If Camera To Use is empty and there is a Camera with a tag set to Main Camera in the scene, it will find the Main Camera and set the Camera To Use as it. "
                , MessageType.Info, true);
                GUILayout.Space(5);
            }
            GUILayout.BeginVertical("", boxStyle);
            GUILayout.Space(5);
            GUILayout.BeginVertical("", boxStyle);
            GUILayout.Space(5);
            EditorGUILayout.PropertyField(Camera_Profile, true);
            if (myTarget.Camera_Profile == null)
            {
                EditorGUILayout.HelpBox("Profile is NOT SET! To create a new Profile, (select 'Assets / Create / BIZNIZ / Camera')!", MessageType.Error, true);
                GUILayout.Space(15);
            }
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();


            //
            GUILayout.Space(15);
            GUILayout.BeginHorizontal("", boxStyle);
            GUILayout.Space(15);

            if (GUILayout.Button("Set Camera To Use!", EditorStyles.toolbarButton))
                myTarget.Setup_CameraToUse();

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
            //

            if (myTarget.ShowHelp)
            {
                EditorGUILayout.HelpBox
                ("CAMERA TO USE  -:Camera to Control. "
                + "\n \n \n" +
                "UI CAMERA  -:Camera to use for displaying the World Space UI. "
                , MessageType.Info, true);
                GUILayout.Space(5);
            }
            GUILayout.BeginVertical("", boxStyle);
            GUILayout.Space(5);
            GUILayout.BeginVertical("", boxStyle);
            GUILayout.Space(5);
            EditorGUILayout.PropertyField(Camera_ToUse, true);
            EditorGUILayout.PropertyField(UI_Camera, true);
            if (myTarget.Camera_Profile != null)
            {
                if (myTarget.Camera_Profile.MoveMode == Bizniz.Profile.CameraProfile.MovementMode.Target_Follow)
                {
                    GUILayout.Space(5);
                    EditorGUILayout.PropertyField(FollowTarget, true);
                    EditorGUILayout.PropertyField(TargetOffset, true);
                }
            }
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();

            //

            GUILayout.Space(15);
            EditorGUILayout.EndVertical();


            //   
            GUILayout.Space(15);
            GUILayout.BeginHorizontal("", boxStyle);
            GUILayout.Space(15);

            EditorGUILayout.LabelField("Show Help?", EditorStyles.boldLabel);
            myTarget.ShowHelp = EditorGUILayout.Toggle(myTarget.ShowHelp, EditorStyles.toggle);

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
            //

            //            
            serializedObj.ApplyModifiedProperties();

            // END
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(target);




        }
    }
}
