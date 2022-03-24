using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InputManager
    {
        #region Singleton
        private InputManager() { }
        private static InputManager _instance;
        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InputManager();
                return _instance;
            }
        }
        #endregion

        private LayerMask _groundMask;
        private Camera _mainCamera;

        public float GetHorizontalInput => Input.GetAxisRaw("Horizontal");
        public float GetVerticalInput => Input.GetAxisRaw("Vertical");
        public bool IsLeftMouseButtonDown => Input.GetMouseButtonDown(0);
        public bool IsLeftMouseButtonHolding => Input.GetMouseButton(0);
        public bool IsLeftMouseButtonUp => Input.GetMouseButtonUp(0);

        public void Initialize(Camera mainCamera)
        {
            this._mainCamera = mainCamera;
            _groundMask = LayerMask.GetMask("Ground", "LowerGround");
        }


        /// <summary>
        /// Returns a Vector3 with horizontal and vertical input values for Player to move
        /// </summary>
        public Vector3 GetMovementVector()
        {
            Vector3 moveVector = new Vector3(GetHorizontalInput, 0, GetVerticalInput);
            return moveVector;
        }


        /// <summary>
        /// Returns the position where raycast from mouse pointer hits.
        /// This function is used to get a position for the Player to look at (mouse pointer position)
        /// </summary>
        public Vector3 GetDirectionToMousePosition()
        {
            Ray rayFromCameraPosition = _mainCamera.ScreenPointToRay(Input.mousePosition);

            Vector3 playerToMouse = Vector3.zero;
            
            if (Physics.Raycast(rayFromCameraPosition, out var rayHit, Mathf.Infinity, _groundMask))
            {
                playerToMouse = (rayHit.point - PlayerManager.Instance.Player.transform.position);
                playerToMouse.y = 0;
            }

            return playerToMouse;
        }
    }
}
