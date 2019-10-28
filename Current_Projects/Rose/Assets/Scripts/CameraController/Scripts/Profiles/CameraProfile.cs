// CameraProfile.cs - By Jimbob Games 2019.
using UnityEngine;


namespace Bizniz.Profile
{
    [CreateAssetMenu(fileName = "Camera", menuName = "BIZNIZ/Profiles/Camera", order = 150)]
    public partial class CameraProfile : ScriptableObject
    {
        //Movement
        [Tooltip("Camera Movement Mode!")]
        public MovementMode MoveMode;
        public enum MovementMode
        {
            Keyboard,
            ClickDrag,
            Keyboard_and_ClickDrag,
            Target_Follow
        };
        [Tooltip("Move Camera when Cursor is near Screen Edge!")]
        public bool Move_NearScreenEdge = true;
        [Range(0, 1000)]
        public int ScreenBorder = 50;
        [Tooltip("Can Rotate the Camera?!")]
        public bool Can_Rotate = true;
        [Tooltip("Speed Up Camera Movement while holding key down!")]
        public bool Use_SpeedBoost = true;
        [Tooltip("If True Up/Forward will move in the Direction your facing, else if False it will use axis and Up/Forward will always be north no matter which direction your facing!")]
        public bool Use_ForwardFacing = true;


        //Inputs
        [Tooltip("Input Name!")]
        public string ForwardBackwardInputName = "Vertical";
        [Tooltip("Input Name!")]
        public string LeftRightInputName = "Horizontal";
        [Tooltip("Input Name of Mouse ScrollWheel!")]
        public string MouseScrollWheelInputName = "Mouse ScrollWheel";
        [Tooltip("Key to be used!")]
        public KeyCode ZoomInKey = KeyCode.X;
        [Tooltip("Key to be used!")]
        public KeyCode ZoomOutKey = KeyCode.Z;
        [Tooltip("Key to be used!")]
        public KeyCode DragKey = KeyCode.Mouse1;
        [Tooltip("Key to be used!")]
        public KeyCode RotateLeft = KeyCode.E;
        [Tooltip("Key to be used!")]
        public KeyCode RotateRight = KeyCode.Q;
        [Tooltip("Key to be used!")]
        public KeyCode SpeedBoostKey = KeyCode.LeftShift;


        //Speed Settings     
        [Tooltip("Move Speed!")]
        [Range(0.1f, 2f)]
        public float Move_Speed = 0.4f;
        [Range(0.1f, 10f)]
        [Tooltip("Multiply Move Speed!")]
        public float Move_SpeedMulti = 1f;
        [Tooltip("Zoom Speed!")]
        [Range(0.1f, 2f)]
        public float Zoom_Speed = 0.5f;
        [Tooltip("Multiply Zoom Speed!")]
        [Range(0.1f, 10f)]
        public float Zoom_SpeedMulti = 1f;
        [Tooltip("Rotate Speed!")]
        [Range(0.1f, 2f)]
        public float Rotate_Speed = 1f;
        [Tooltip("Multiply Rotate Speed!")]
        [Range(0.1f, 10f)]
        public float Rotate_SpeedMulti = 1f;
        [Tooltip("Multiply Boost Move Speed!")]
        [Range(0.1f, 10f)]
        public float BoostMove_SpeedMulti = 2f;


        //Limit Settings
        [Range(0f, 10000f)]
        [Tooltip("Set Camera default height!")]
        public float Height = 10f;

        [Range(-10000f, 10000f)]
        [Tooltip("Set Backwards limit!")]
        public float DownMax = -40f;
        [Range(-10000f, 10000f)]
        [Tooltip("Set Forwards limit!")]
        public float UpMax = 40f;

        [Range(-10000f, 10000f)]
        [Tooltip("Set Left limit!")]
        public float LeftMax = 0f;
        [Range(-10000f, 10000f)]
        [Tooltip("Set Right limit!")]
        public float RightMax = 15f;


        //Orthographic Settings
        [Range(0f, 100f)]
        [Tooltip("If Camera is Orthographic, set size value!")]
        public float ZoomMin = 6f;
        [Range(0f, 100f)]
        [Tooltip("If Camera is Orthographic, set size value!")]
        public float ZoomMax = 10f;
        [Range(-10000f, 10000f)]
        [Tooltip("If Camera is Orthographic, set Near Plane value!")]
        public float OrthNearPlane = -10f;
        [Range(-10000f, 10000f)]
        [Tooltip("If Camera is Orthographic, set Far Plane value!")]
        public float OrthFarPlane = 50f;


        //Perspective Settings
        [Range(1f, 120f)]
        [Tooltip("If Camera is Perspective, set FOV value!")]
        public float FOVMin = 40f;
        [Range(1f, 120f)]
        [Tooltip("If Camera is Perspective, set FOV value!")]
        public float FOVMax = 60f;
        [Range(-10000f, 10000f)]
        [Tooltip("If Camera Perspective, set Near Plane value!")]
        public float PersNearPlane = 0.02f;
        [Range(-10000f, 10000f)]
        [Tooltip("If Camera Perspective, set Far Plane value!")]
        public float PersFarPlane = 100f;

        //

        // USED WITH EDITOR!
        public bool ShowHelp = false;
        public int currentTab = 0;
        public string[] TabStrings = new string[] { "Main", "Orthographic", "Perspective", "Inputs" };

        //

        protected CameraController m_cameraController = null;
        protected CameraController cameraController
        {
            get
            {
                if (m_cameraController == null)
                    m_cameraController = CameraController.Instance;
                return m_cameraController;
            }
        }

        //

        /// <summary>
        /// While playing, you can move the camera to the position your setting, and this will take the Camera's current position and set the Cam_Max value above!
        /// </summary>
        public virtual void SetLimit_UpMax()
        {
            if (!cameraController)
                return;
            if (!cameraController.Camera_ToUse)
                return;

            UpMax = cameraController.Camera_ToUse.transform.position.z;
        }
        /// <summary>
        /// This will set the Cam_Max value to the maximum value, for making setting the Camera Max values easier. -PRO TIP! Disable Move Near Screen Edge while settings Max Values!!
        /// </summary>
        public virtual void ResetLimit_UpMax()
        {
            UpMax = 10000f;
        }
        //
        /// <summary>
        /// While playing, you can move the camera to the position your setting, and this will take the Camera's current position and set the Cam_Max value above!
        /// </summary>
        public virtual void SetLimit_DownMax()
        {
            if (!cameraController)
                return;
            if (!cameraController.Camera_ToUse)
                return;

            DownMax = cameraController.Camera_ToUse.transform.position.z;
        }
        /// <summary>
        /// This will set the Cam_Max value to the maximum value, for making setting the Camera Max values easier. -PRO TIP! Disable Move Near Screen Edge while settings Max Values!!
        /// </summary>
        public virtual void ResetLimit_DownMax()
        {
            DownMax = -10000f;
        }
        //
        /// <summary>
        /// While playing, you can move the camera to the position your setting, and this will take the Camera's current position and set the Cam_Max value above!
        /// </summary>
        public virtual void SetLimit_LeftMax()
        {
            if (!cameraController)
                return;
            if (!cameraController.Camera_ToUse)
                return;

            LeftMax = cameraController.Camera_ToUse.transform.position.x;
        }
        /// <summary>
        /// This will set the Cam_Max value to the maximum value, for making setting the Camera Max values easier. -PRO TIP! Disable Move Near Screen Edge while settings Max Values!!
        /// </summary>
        public virtual void ResetLimit_LeftMax()
        {
            LeftMax = -10000f;
        }
        //
        /// <summary>
        /// While playing, you can move the camera to the position your setting, and this will take the Camera's current position and set the Cam_Max value above!
        /// </summary>
        public virtual void SetLimit_RightMax()
        {
            if (!cameraController)
                return;
            if (!cameraController.Camera_ToUse)
                return;

            RightMax = cameraController.Camera_ToUse.transform.position.x;
        }
        /// <summary>
        /// This will set the Cam_Max value to the maximum value, for making setting the Camera Max values easier. -PRO TIP! Disable Move Near Screen Edge while settings Max Values!!
        /// </summary>
        public virtual void ResetLimit_RightMax()
        {
            RightMax = 10000f;
        }
    }
}