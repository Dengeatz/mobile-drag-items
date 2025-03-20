using UnityEngine;

namespace TestTask.PlayerComponents
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public Camera CameraHandler { get; private set; }

        #region Unity Methods
        private void Awake()
        {
            CameraHandler = _camera;
        }
        #endregion

        #region Public Methods
        public void RotateCamera(Vector2 direction)
        {
            _camera.transform.rotation = Quaternion.Euler(direction.x, direction.y, 0);
        }

        public void CameraToPosition(Vector3 pos)
        {
            _camera.transform.position = pos;
        }
        #endregion
    }
}