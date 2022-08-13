using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

using Penwyn.Tools;
namespace Penwyn.Game
{
    public class CursorManager : SingletonMonoBehaviour<CursorManager>
    {
        public Texture2D NormalCursor;
        public Texture2D TargettingCursor;

        protected bool _isPermanentlyHide;

        public virtual void ShowMouse()
        {
            ChangeMouseMode(true);
        }

        public virtual void HideMouse()
        {
            if (_isPermanentlyHide)
                ChangeMouseMode(false);
        }

        protected virtual void ChangeMouseMode(bool isVisible, CursorLockMode lockMode = CursorLockMode.Confined)
        {
            Cursor.visible = isVisible;
            Cursor.lockState = lockMode;
        }

        public Vector3 GetMousePosition()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.farClipPlane * 0.5F;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        public RaycastHit2D GetRayHitUnderMouse()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, GetMousePosition());
            Debug.DrawRay(hit.point, Vector3.up * 1000, Color.green);
            return hit;
        }

        public void ResetCursor()
        {
            ChangeCursorSprite(NormalCursor);
        }

        public void ChangeCursorSprite(Texture2D cursorSprite)
        {
            Cursor.SetCursor(cursorSprite, Vector3.zero * cursorSprite.height / 2f, CursorMode.Auto);
        }


        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
        }

        public virtual void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public virtual void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}

