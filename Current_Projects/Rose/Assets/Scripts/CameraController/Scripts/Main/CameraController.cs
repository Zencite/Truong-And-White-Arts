// CameraController.cs - By Jimbob Games 2019.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bizniz.Profile;


namespace Bizniz
{
    public partial class CameraController : MonoBehaviour
    {
        public static CameraController Instance = null;

        //

        public CameraProfile Camera_Profile;

        //

        public Camera Camera_ToUse;
        public Camera UI_Camera;

        public Transform FollowTarget = null;
        public Vector3 TargetOffset = Vector3.zero;

        //

        // USED WITH EDITOR!
        public bool ShowHelp = false;

        //

        //Private
        protected Vector3 CamOrigin;
        protected Vector3 Difference;

        //

        /// <summary>
        /// Sets Instance!
        /// </summary>
        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        //

        /// <summary>
        /// EDITOR USE!  Set's MainCamera to CameraToUse if empty!
        /// </summary>
        public virtual void Setup_CameraToUse()
        {
            if (Camera_ToUse == null)
            {
                if (Camera.main != null)
                    Camera_ToUse = Camera.main;
                else Debug.LogError("Camera Controller:  -AutoSetup CANNOT find a Camera, please make sure you have a Camera in the Scene tagged with 'MainCamera'!");
            }
        }

        //

        /// <summary>
        /// Calculates Move Speed!
        /// </summary>
        protected float Move_SpeedValue()
        {
            if (!Camera_Profile.Use_SpeedBoost)
                return (Camera_Profile.Move_Speed * 100f) * Camera_Profile.Move_SpeedMulti;
            else
            {
                if (Input.GetKey(Camera_Profile.SpeedBoostKey))
                    return (Camera_Profile.Move_Speed * 100f) * Camera_Profile.Move_SpeedMulti * Camera_Profile.BoostMove_SpeedMulti;
                else return (Camera_Profile.Move_Speed * 100f) * Camera_Profile.Move_SpeedMulti;
            }
        }

        /// <summary>
        /// Calculates Zoom Speed!
        /// </summary>
        protected float Zoom_SpeedValue()
        {
            return (Camera_Profile.Zoom_Speed * 100f) * Camera_Profile.Zoom_SpeedMulti;
        }

        /// <summary>
        /// Calculates Rotate Speed!
        /// </summary>
        protected float Rotate_SpeedValue()
        {
            return (Camera_Profile.Rotate_Speed * 100f) * Camera_Profile.Rotate_SpeedMulti;
        }

        /// <summary>
        /// Calculates Camera Vertical Positions!
        /// </summary>
        protected float CamPosZValue()
        {
            return Camera_ToUse.transform.position.z;
        }

        /// <summary>
        /// Calculates Camera Horizontal Position!
        /// </summary>
        protected float CamPosXValue()
        {
            return Camera_ToUse.transform.position.x;
        }

        /// <summary>
        /// Calculates Camera Height!
        /// </summary>
        protected float CamPosYValue()
        {
            return Camera_Profile.Height;
        }

        /// <summary>
        /// Calculates Camera Position!
        /// </summary>
        protected Vector3 CameraPosition()
        {
            return new Vector3(CamPosXValue(), CamPosYValue(), CamPosZValue());
        }

        /// <summary>
        /// Calculates Mouse Position!
        /// </summary>
        protected Vector3 MousePosition()
        {
            return Camera_ToUse.ScreenToWorldPoint(Input.mousePosition);
        }

        /// <summary>
        /// Used by Dragging to calculate position!
        /// </summary>
        protected Vector3 GetOrigin()
        {
            return new Vector3(MousePosition().x, CamPosYValue(), MousePosition().z);
        }

        /// <summary>
        /// Used by Dragging to calculate position!
        /// </summary>
        protected Vector3 GetDifference()
        {
            return GetOrigin() - CameraPosition();
        }

        /// <summary>
        /// Calculates Screen Width!
        /// </summary>
        protected int GetScreenWidth()
        {
            return Screen.width;
        }
        /// <summary>
        /// Calculates Screen Height!
        /// </summary>
        protected int GetScreenHeight()
        {
            return Screen.height;
        }

        /// <summary>
        /// Calculates Target Follow Position!
        /// </summary>
        protected Vector3 GetTargetFollow()
        {
            Vector3 targetPos = FollowTarget.position + TargetOffset;
            Vector3 smoothPos = Vector3.Lerp(Camera_ToUse.transform.position, targetPos, Move_SpeedValue());

            return smoothPos;
        }
        /// <summary>
        /// Returns Input commands when Profile set to Keyboard Mode/Both!
        /// </summary>
        protected virtual void Inputs_Keyboard()
        {
            if (Camera_Profile.Use_ForwardFacing)
            {
                //USING KEYBOARD TO MOVE!
                //Wait for Inputs and Move the Camera Vertical!
                if (Input.GetAxis(Camera_Profile.ForwardBackwardInputName) > 0f)
                    Camera_ToUse.transform.position += Camera_ToUse.transform.forward * Move_SpeedValue() * Time.deltaTime;
                else if (Input.GetAxis(Camera_Profile.ForwardBackwardInputName) < 0f)
                    Camera_ToUse.transform.position -= Camera_ToUse.transform.forward * Move_SpeedValue() * Time.deltaTime;

                //Wait for Inputs and Move the Camera Horizontal!
                if (Input.GetAxis(Camera_Profile.LeftRightInputName) > 0f)
                    Camera_ToUse.transform.position += Camera_ToUse.transform.right * Move_SpeedValue() * Time.deltaTime;
                else if (Input.GetAxis(Camera_Profile.LeftRightInputName) < 0f)
                    Camera_ToUse.transform.position -= Camera_ToUse.transform.right * Move_SpeedValue() * Time.deltaTime;
            }
            else
            {
                //Wait for Inputs and Move the Camera Vertical!
                if (Input.GetAxis(Camera_Profile.ForwardBackwardInputName) > 0f)
                    Camera_ToUse.transform.position += Vector3.forward * Move_SpeedValue() * Time.deltaTime;
                else if (Input.GetAxis(Camera_Profile.ForwardBackwardInputName) < 0f)
                    Camera_ToUse.transform.position += Vector3.back * Move_SpeedValue() * Time.deltaTime;

                //Wait for Inputs and Move the Camera Horizontal!
                if (Input.GetAxis(Camera_Profile.LeftRightInputName) > 0f)
                    Camera_ToUse.transform.position += Vector3.right * Move_SpeedValue() * Time.deltaTime;
                else if (Input.GetAxis(Camera_Profile.LeftRightInputName) < 0f)
                    Camera_ToUse.transform.position += Vector3.left * Move_SpeedValue() * Time.deltaTime;
            }
        }
        /// <summary>
        /// Returns Input commands when Profile set to ClickDrag Mode/Both!
        /// </summary>
        protected virtual void Inputs_ClickDrag()
        {
            //USING MOUSE CLICK DRAG TO MOVE!
            if (Input.GetKeyDown(Camera_Profile.DragKey))
            {
                CamOrigin = GetOrigin();
            }
            if (Input.GetKey(Camera_Profile.DragKey))
            {
                Difference = GetDifference();
                Camera_ToUse.transform.position = CamOrigin - Difference;
            }
        }

        //

        /// <summary>
        /// Updates all commands!
        /// </summary>
        protected virtual void Update()
        {
            //Don't Continue if No Profile!
            if (!Camera_Profile)
                return;
            //Don't Continue if Time is paused!
            if (Time.timeScale == 0f)
                return;
            //Don't Continue if Camera is Missing!
            if (Camera_ToUse == null)
                return;

            //

            if (Camera_Profile.Move_NearScreenEdge)
            {
                if (!Input.GetKeyDown(Camera_Profile.DragKey))
                    Update_MoveScreenEdge();
            }
            Update_Movement();
            Update_Rotation();
            Update_Zooming();
            Update_Limits();
        }

        //
        /// <summary>
        /// Updates when Mouse is close to screen border!
        /// </summary>
        protected virtual void Update_MoveScreenEdge()
        {
            if (Camera_Profile.Use_ForwardFacing)
            {
                if (Input.mousePosition.x > GetScreenWidth() - Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position += Camera_ToUse.transform.right * Move_SpeedValue() * Time.deltaTime;
                else if (Input.mousePosition.x < 0 + Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position -= Camera_ToUse.transform.right * Move_SpeedValue() * Time.deltaTime;

                if (Input.mousePosition.y > GetScreenHeight() - Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position += Camera_ToUse.transform.forward * Move_SpeedValue() * Time.deltaTime;
                else if (Input.mousePosition.y < 0 + Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position -= Camera_ToUse.transform.forward * Move_SpeedValue() * Time.deltaTime;
            }
            else
            {
                if (Input.mousePosition.x > GetScreenWidth() - Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position += Vector3.right * Move_SpeedValue() * Time.deltaTime;
                else if (Input.mousePosition.x < 0 + Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position += Vector3.left * Move_SpeedValue() * Time.deltaTime;

                if (Input.mousePosition.y > GetScreenHeight() - Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position += Vector3.forward * Move_SpeedValue() * Time.deltaTime;
                else if (Input.mousePosition.y < 0 + Camera_Profile.ScreenBorder)
                    Camera_ToUse.transform.position += Vector3.back * Move_SpeedValue() * Time.deltaTime;
            }
        }

        /// <summary>
        /// Updates Movement!
        /// </summary>
        protected virtual void Update_Movement()
        {
            switch (Camera_Profile.MoveMode)
            {
                case CameraProfile.MovementMode.Keyboard:
                    {
                        Inputs_Keyboard();
                    }
                    break;

                case CameraProfile.MovementMode.ClickDrag:
                    {
                        Inputs_ClickDrag();
                    }
                    break;

                case CameraProfile.MovementMode.Keyboard_and_ClickDrag:
                    {
                        Inputs_Keyboard();

                        Inputs_ClickDrag();
                    }
                    break;

                case CameraProfile.MovementMode.Target_Follow:
                    {
                        if (!FollowTarget)
                        {
                            Debug.LogError("CameraController:  -We have no Target to follow!");
                            return;
                        }

                        Camera_ToUse.transform.position = GetTargetFollow();
                        Camera_ToUse.transform.LookAt(FollowTarget);
                    }
                    break;
            }
        }

        /// <summary>
        /// Updates Rotation!
        /// </summary>
        protected virtual void Update_Rotation()
        {
            if (!Camera_Profile.Can_Rotate)
                return;


            if (Input.GetKey(Camera_Profile.RotateLeft))
            {
                Camera_ToUse.transform.Rotate(Vector3.up * Rotate_SpeedValue() * Time.deltaTime, Space.World);
            }
            else if (Input.GetKey(Camera_Profile.RotateRight))
            {
                Camera_ToUse.transform.Rotate(-Vector3.up * Rotate_SpeedValue() * Time.deltaTime, Space.World);
            }
        }

        /// <summary>
        /// Updates Zooming!
        /// </summary>
        protected virtual void Update_Zooming()
        {
            //Wait for Inputs and Zoom the Camera Up/Down!
            if (Input.GetAxis(Camera_Profile.MouseScrollWheelInputName) > 0f || Input.GetKey(Camera_Profile.ZoomInKey))
            {
                if (Camera_ToUse.orthographic)
                    Camera_ToUse.orthographicSize -= Zoom_SpeedValue() * Time.deltaTime;
                else
                    Camera_ToUse.fieldOfView -= Zoom_SpeedValue() * Time.deltaTime;

                //UI Camera
                if (UI_Camera != null)
                {
                    if (UI_Camera.orthographic)
                        UI_Camera.orthographicSize -= Zoom_SpeedValue() * Time.deltaTime;
                    else
                        UI_Camera.fieldOfView -= Zoom_SpeedValue() * Time.deltaTime;
                }
            }
            else if (Input.GetAxis(Camera_Profile.MouseScrollWheelInputName) < 0f || Input.GetKey(Camera_Profile.ZoomOutKey))
            {
                if (Camera_ToUse.orthographic)
                    Camera_ToUse.orthographicSize += Zoom_SpeedValue() * Time.deltaTime;
                else
                    Camera_ToUse.fieldOfView += Zoom_SpeedValue() * Time.deltaTime;

                //UI Camera
                if (UI_Camera != null)
                {
                    if (UI_Camera.orthographic)
                        UI_Camera.orthographicSize += Zoom_SpeedValue() * Time.deltaTime;
                    else
                        UI_Camera.fieldOfView += Zoom_SpeedValue() * Time.deltaTime;
                }
            }
        }

        /// <summary>
        /// Updates Position Limits!
        /// </summary>
        protected virtual void Update_Limits()
        {
            //Set Min/Max Camera Positions Vertical!
            if (Camera_ToUse.transform.position.z >= Camera_Profile.UpMax)
                Camera_ToUse.transform.position = new Vector3(CamPosXValue(), CamPosYValue(), Camera_Profile.UpMax);
            if (Camera_ToUse.transform.position.z <= Camera_Profile.DownMax)
                Camera_ToUse.transform.position = new Vector3(CamPosXValue(), CamPosYValue(), Camera_Profile.DownMax);
            //Set Min/Max Camera Positions Horizontal!
            if (Camera_ToUse.transform.position.x >= Camera_Profile.RightMax)
                Camera_ToUse.transform.position = new Vector3(Camera_Profile.RightMax, CamPosYValue(), CamPosZValue());
            if (Camera_ToUse.transform.position.x <= Camera_Profile.LeftMax)
                Camera_ToUse.transform.position = new Vector3(Camera_Profile.LeftMax, CamPosYValue(), CamPosZValue());
            //Set Camera pos y limit
            if (Camera_ToUse.transform.position.y >= CamPosYValue())
                Camera_ToUse.transform.position = new Vector3(CamPosXValue(), CamPosYValue(), CamPosZValue());
            if (Camera_ToUse.transform.position.y <= CamPosYValue())
                Camera_ToUse.transform.position = new Vector3(CamPosXValue(), CamPosYValue(), CamPosZValue());


            //

            //Set Min/Max Camera Zoom limits!
            if (Camera_ToUse.orthographic)
            {
                //Set Size
                if (Camera_ToUse.orthographicSize >= Camera_Profile.ZoomMax)
                    Camera_ToUse.orthographicSize = Camera_Profile.ZoomMax;
                if (Camera_ToUse.orthographicSize <= Camera_Profile.ZoomMin)
                    Camera_ToUse.orthographicSize = Camera_Profile.ZoomMin;
                //Set Far / Near Planes
                if (Camera_ToUse.nearClipPlane != Camera_Profile.OrthNearPlane)
                    Camera_ToUse.nearClipPlane = Camera_Profile.OrthNearPlane;
                if (Camera_ToUse.farClipPlane != Camera_Profile.OrthFarPlane)
                    Camera_ToUse.farClipPlane = Camera_Profile.OrthFarPlane;


                //Set UI Camera to use same mode!
                if (UI_Camera != null)
                {
                    if (!UI_Camera.orthographic)
                        UI_Camera.orthographic = true;

                    //Set Size
                    if (UI_Camera.orthographicSize >= Camera_Profile.ZoomMax)
                        UI_Camera.orthographicSize = Camera_Profile.ZoomMax;
                    if (UI_Camera.orthographicSize <= Camera_Profile.ZoomMin)
                        UI_Camera.orthographicSize = Camera_Profile.ZoomMin;
                }
            }
            else
            {
                //Set FOV
                if (Camera_ToUse.fieldOfView >= Camera_Profile.FOVMax)
                    Camera_ToUse.fieldOfView = Camera_Profile.FOVMax;
                if (Camera_ToUse.fieldOfView <= Camera_Profile.FOVMin)
                    Camera_ToUse.fieldOfView = Camera_Profile.FOVMin;
                //Set Far / Near Planes
                if (Camera_ToUse.nearClipPlane != Camera_Profile.PersNearPlane)
                    Camera_ToUse.nearClipPlane = Camera_Profile.PersNearPlane;
                if (Camera_ToUse.farClipPlane != Camera_Profile.PersFarPlane)
                    Camera_ToUse.farClipPlane = Camera_Profile.PersFarPlane;


                //Set UI Camera to use same mode!
                if (UI_Camera != null)
                {
                    if (UI_Camera.orthographic)
                        UI_Camera.orthographic = false;

                    //Set FOV
                    if (UI_Camera.fieldOfView >= Camera_Profile.FOVMax)
                        UI_Camera.fieldOfView = Camera_Profile.FOVMax;
                    if (UI_Camera.fieldOfView <= Camera_Profile.FOVMin)
                        UI_Camera.fieldOfView = Camera_Profile.FOVMin;
                }
            }
        }
    }
}