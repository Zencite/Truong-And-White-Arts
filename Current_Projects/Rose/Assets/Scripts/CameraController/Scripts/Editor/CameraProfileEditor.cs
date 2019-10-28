// CameraProfileEditor.cs - By Jimbob Games 2019.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;


namespace Bizniz.Profile
{
    [CustomEditor(typeof(CameraProfile))]
    public class CameraProfileEditor : Editor
    {
        GUIStyle boxStyle;
        GUIStyle boxStyle2;

        CameraProfile myTarget;
        SerializedObject serializedObj;
        Texture LogoTexture;


        //
        SerializedProperty MoveMode;
        SerializedProperty Move_NearScreenEdge;
        SerializedProperty ScreenBorder;
        SerializedProperty Use_SpeedBoost;
        SerializedProperty Use_ForwardFacing;



        SerializedProperty ForwardBackwardInputName;
        SerializedProperty LeftRightInputName;
        SerializedProperty MouseScrollWheelInputName;
        SerializedProperty ZoomInKey;
        SerializedProperty ZoomOutKey;
        SerializedProperty RotateLeft;
        SerializedProperty RotateRight;
        SerializedProperty DragKey;
        SerializedProperty SpeedBoostKey;


        SerializedProperty Move_Speed;
        SerializedProperty Move_SpeedMulti;
        SerializedProperty Zoom_Speed;
        SerializedProperty Zoom_SpeedMulti;
        SerializedProperty Rotate_Speed;
        SerializedProperty Rotate_SpeedMulti;
        SerializedProperty BoostMove_SpeedMulti;


        SerializedProperty CamHeight;
        SerializedProperty CamDownMax;
        SerializedProperty CamUpMax;
        SerializedProperty CamLeftMax;
        SerializedProperty CamRightMax;


        SerializedProperty CamZoomMin;
        SerializedProperty CamZoomMax;
        SerializedProperty OrthNearPlane;
        SerializedProperty OrthFarPlane;


        SerializedProperty CamFOVMin;
        SerializedProperty CamFOVMax;
        SerializedProperty PersNearPlane;
        SerializedProperty PersFarPlane;


        void OnEnable()
        {
            myTarget = (CameraProfile)target;
            serializedObj = new SerializedObject(myTarget);
            LogoTexture = Resources.Load("Art/CameraProfile_Logo") as Texture;

            //   

            MoveMode = serializedObj.FindProperty("MoveMode");
            Move_NearScreenEdge = serializedObj.FindProperty("Move_NearScreenEdge");
            ScreenBorder = serializedObj.FindProperty("ScreenBorder");
            Use_SpeedBoost = serializedObj.FindProperty("Use_SpeedBoost");
            Use_ForwardFacing = serializedObj.FindProperty("Use_ForwardFacing");


            ForwardBackwardInputName = serializedObj.FindProperty("ForwardBackwardInputName");
            LeftRightInputName = serializedObj.FindProperty("LeftRightInputName");
            MouseScrollWheelInputName = serializedObj.FindProperty("MouseScrollWheelInputName");
            ZoomInKey = serializedObj.FindProperty("ZoomInKey");
            ZoomOutKey = serializedObj.FindProperty("ZoomOutKey");
            RotateLeft = serializedObj.FindProperty("RotateLeft");
            RotateRight = serializedObj.FindProperty("RotateRight");
            DragKey = serializedObj.FindProperty("DragKey");
            SpeedBoostKey = serializedObj.FindProperty("SpeedBoostKey");


            Move_Speed = serializedObj.FindProperty("Move_Speed");
            Move_SpeedMulti = serializedObj.FindProperty("Move_SpeedMulti");
            Zoom_Speed = serializedObj.FindProperty("Zoom_Speed");
            Zoom_SpeedMulti = serializedObj.FindProperty("Zoom_SpeedMulti");
            Rotate_Speed = serializedObj.FindProperty("Rotate_Speed");
            Rotate_SpeedMulti = serializedObj.FindProperty("Rotate_SpeedMulti");
            BoostMove_SpeedMulti = serializedObj.FindProperty("BoostMove_SpeedMulti");


            CamHeight = serializedObj.FindProperty("Height");
            CamDownMax = serializedObj.FindProperty("DownMax");
            CamUpMax = serializedObj.FindProperty("UpMax");
            CamLeftMax = serializedObj.FindProperty("LeftMax");
            CamRightMax = serializedObj.FindProperty("RightMax");


            CamZoomMin = serializedObj.FindProperty("ZoomMin");
            CamZoomMax = serializedObj.FindProperty("ZoomMax");
            OrthNearPlane = serializedObj.FindProperty("OrthNearPlane");
            OrthFarPlane = serializedObj.FindProperty("OrthFarPlane");


            CamFOVMin = serializedObj.FindProperty("FOVMin");
            CamFOVMax = serializedObj.FindProperty("FOVMax");
            PersNearPlane = serializedObj.FindProperty("PersNearPlane");
            PersFarPlane = serializedObj.FindProperty("PersFarPlane");

        }


        public override void OnInspectorGUI()
        {
            myTarget = (CameraProfile)target;

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
                EditorGUILayout.LabelField("CAMERA PROFILE!", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            // 
            GUILayout.BeginVertical("", boxStyle2);
            EditorGUILayout.EndVertical();

            // 



            //            

            myTarget.currentTab = GUILayout.Toolbar(myTarget.currentTab, myTarget.TabStrings, EditorStyles.toolbarButton);
            switch (myTarget.currentTab)
            {


                case 0: //MAIN
                    {
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(15);

                        //

                        if (myTarget.ShowHelp)
                        {
                            EditorGUILayout.HelpBox
                            ("MOVE MODE  -:The Movement mode to be used. "
                            + "\n \n \n" +
                            "USE FORWARD FACING  -:If True, Camera will move where its facing, If False, it will use North, South, East, West. "
                            + "\n \n \n" +
                            "USE SPEEDBOOST  -:If True, while holding the Speed Boost Key the camera will move faster. "
                            + "\n \n \n" +
                            "MOVE NEAR SCREEN EDGE  -:If True, the camera will move when the Mouse gets close to the screen edge. "
                            + "\n \n \n" +
                            "SCREEN BORDER  -:The size of the border to be used with Move Near Screen Edge. "
                            , MessageType.Info, true);
                            GUILayout.Space(5);
                        }
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(MoveMode, true);
                        EditorGUILayout.PropertyField(Use_ForwardFacing, true);
                        EditorGUILayout.PropertyField(Use_SpeedBoost, true);
                        EditorGUILayout.PropertyField(Move_NearScreenEdge, true);
                        if (myTarget.Move_NearScreenEdge)
                        {
                            EditorGUILayout.PropertyField(ScreenBorder, true);
                        }
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();

                        GUILayout.Space(15);

                        if (myTarget.ShowHelp)
                        {
                            EditorGUILayout.HelpBox
                            ("MOVE SPEED  -:The speed this camera can move without speed boost. "
                            + "\n \n \n" +
                            "MOVE SPEED MULTIPLIER  -:A Multiplier to add to the Speed. "
                            + "\n \n \n" +
                            "ZOOM SPEED  -:The speed of the Zooming in and out. "
                            + "\n \n \n" +
                            "ZOOM SPEED MULTIPLIER  -:A Multiplier to add to the Speed. "
                            + "\n \n \n" +
                            "ROTATE SPEED  -:The speed the camera rotates around. "
                            + "\n \n \n" +
                            "ROTATE SPEED MULTIPLIER  -:A Multiplier to add to the Speed. "
                            + "\n \n \n" +
                            "BOOST MOVE SPEED MULTIPLIER  -:If using Use SpeedBoost, A Multiplier to add to the Move speed to gain a boost. "
                            , MessageType.Info, true);
                            GUILayout.Space(5);
                        }
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(Move_Speed, true);
                        EditorGUILayout.PropertyField(Move_SpeedMulti, true);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(Zoom_Speed, true);
                        EditorGUILayout.PropertyField(Zoom_SpeedMulti, true);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(Rotate_Speed, true);
                        EditorGUILayout.PropertyField(Rotate_SpeedMulti, true);

                        if (myTarget.Use_SpeedBoost)
                        {
                            GUILayout.Space(5);
                            EditorGUILayout.PropertyField(BoostMove_SpeedMulti, true);
                        }
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();

                        GUILayout.Space(15);

                        if (myTarget.ShowHelp)
                        {
                            EditorGUILayout.HelpBox
                            ("CAM HEIGHT  -:The Y Postiton the Camera will use. "
                            , MessageType.Info, true);
                            GUILayout.Space(5);
                        }
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(CamHeight, true);
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();

                        GUILayout.Space(5);

                        if (myTarget.ShowHelp)
                        {
                            EditorGUILayout.HelpBox
                            ("CAM _ MAX  -:In each direction you can set a maximum distance the camera can move to. "
                            + "\n \n \n" +
                            "SET UP MAX  -:While playing, you can move the camera to the position your setting, and this will take the Camera's current position and set the Cam_Max value above. "
                            + "\n \n \n" +
                            "RESET  -:This will set the Cam_Max value to the maximum value, for making setting the Camera Max values easier. -PRO TIP! Disable Move Near Screen Edge while settings Max Values!. "
                            , MessageType.Info, true);
                            GUILayout.Space(5);
                        }
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(CamUpMax, true);
                        GUILayout.BeginHorizontal("", boxStyle);
                        if (GUILayout.Button("SET UP MAX!", EditorStyles.miniButton))
                            myTarget.SetLimit_UpMax();
                        GUILayout.Space(5);
                        if (GUILayout.Button("RESET!", EditorStyles.miniButton))
                            myTarget.ResetLimit_UpMax();
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(10);

                        EditorGUILayout.PropertyField(CamDownMax, true);
                        GUILayout.BeginHorizontal("", boxStyle);
                        if (GUILayout.Button("SET DOWN MAX!", EditorStyles.miniButton))
                            myTarget.SetLimit_DownMax();
                        GUILayout.Space(5);
                        if (GUILayout.Button("RESET!", EditorStyles.miniButton))
                            myTarget.ResetLimit_DownMax();
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(10);

                        EditorGUILayout.PropertyField(CamLeftMax, true);
                        GUILayout.BeginHorizontal("", boxStyle);
                        if (GUILayout.Button("SET LEFT MAX!", EditorStyles.miniButton))
                            myTarget.SetLimit_LeftMax();
                        GUILayout.Space(5);
                        if (GUILayout.Button("RESET!", EditorStyles.miniButton))
                            myTarget.ResetLimit_LeftMax();
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(10);

                        EditorGUILayout.PropertyField(CamRightMax, true);
                        GUILayout.BeginHorizontal("", boxStyle);
                        if (GUILayout.Button("SET RIGHT MAX!", EditorStyles.miniButton))
                            myTarget.SetLimit_RightMax();
                        GUILayout.Space(5);
                        if (GUILayout.Button("RESET!", EditorStyles.miniButton))
                            myTarget.ResetLimit_RightMax();
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(10);

                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();



                        //

                        GUILayout.Space(15);
                        EditorGUILayout.EndVertical();

                    }
                    break;

                case 1: //ORTH
                    {
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(15);

                        //


                        if (myTarget.ShowHelp)
                        {
                            EditorGUILayout.HelpBox
                            ("CAM ZOOM MIN  -:Camera Size Min. "
                            + "\n \n \n" +
                            "CAM ZOOM MAX  -:Camera Size Max. "
                            + "\n \n \n" +
                            "ORTH NEAR PLANE  -:Camera Near Plane Offset. "
                            + "\n \n \n" +
                            "ORTH FAR PLANE  -:Camera Far Plane Offset. "
                            , MessageType.Info, true);
                            GUILayout.Space(5);
                        }
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(CamZoomMin, true);
                        EditorGUILayout.PropertyField(CamZoomMax, true);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(OrthNearPlane, true);
                        EditorGUILayout.PropertyField(OrthFarPlane, true);
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();



                        //

                        GUILayout.Space(15);
                        EditorGUILayout.EndVertical();

                    }
                    break;

                case 2: //PERS
                    {
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(15);

                        //


                        if (myTarget.ShowHelp)
                        {
                            EditorGUILayout.HelpBox
                            ("CAM FOV MIN  -:Camera FOV Min. "
                            + "\n \n \n" +
                            "CAM FOV MAX  -:Camera FOV Max. "
                            + "\n \n \n" +
                            "PERS NEAR PLANE  -:Camera Near Plane Offset. "
                            + "\n \n \n" +
                            "PERS FAR PLANE  -:Camera Far Plane Offset. "
                            , MessageType.Info, true);
                            GUILayout.Space(5);
                        }
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(CamFOVMin, true);
                        EditorGUILayout.PropertyField(CamFOVMax, true);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(PersNearPlane, true);
                        EditorGUILayout.PropertyField(PersFarPlane, true);
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();



                        //

                        GUILayout.Space(15);
                        EditorGUILayout.EndVertical();

                    }
                    break;

                case 3: //INPUTS
                    {
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(15);

                        //


                        if (myTarget.ShowHelp)
                        {
                            EditorGUILayout.HelpBox
                            ("FORWARD BACKWARD INPUT NAME  -:Name of the Input to use. "
                            + "\n \n \n" +
                            "LEFT RIGHT INPUT NAME  -:Name of the Input to use. "
                            + "\n \n \n" +
                            "MOUSE SCROLLWHEEL INPUT NAME  -:Name of the Input to use. "
                            + "\n \n \n" +
                            "ZOOM IN KEY  -:Input Key to be used. "
                            + "\n \n \n" +
                            "ZOOM OUT KEY  -:Input Key to be used. "
                            + "\n \n \n" +
                            "ROTATE LEFT  -:Input Key to be used. "
                            + "\n \n \n" +
                            "ROTATE RIGHT  -:Input Key to be used. "
                            + "\n \n \n" +
                            "DRAG KEY  -:Input Key to be used. "
                            + "\n \n \n" +
                            "SPEED BOOST KEY  -:Input Key to be used. "
                            , MessageType.Info, true);
                            GUILayout.Space(5);
                        }
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Space(5);
                        EditorGUILayout.PropertyField(ForwardBackwardInputName, true);
                        EditorGUILayout.PropertyField(LeftRightInputName, true);
                        EditorGUILayout.PropertyField(MouseScrollWheelInputName, true);
                        EditorGUILayout.PropertyField(ZoomInKey, true);
                        EditorGUILayout.PropertyField(ZoomOutKey, true);
                        EditorGUILayout.PropertyField(RotateLeft, true);
                        EditorGUILayout.PropertyField(RotateRight, true);
                        EditorGUILayout.PropertyField(DragKey, true);
                        if (myTarget.Use_SpeedBoost)
                            EditorGUILayout.PropertyField(SpeedBoostKey, true);
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        EditorGUILayout.EndVertical();



                        //

                        GUILayout.Space(15);
                        EditorGUILayout.EndVertical();

                    }
                    break;
            }



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
