using TestTask.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TestTask.PlayerComponents
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _itemLayerMask;
        [SerializeField] private int _itemInHandsLayerMask;
        [SerializeField] private float _distanceItem;
        [SerializeField] private CharacterController _controller;
        [Inject] private PlayerCamera _camera;
        private PlayerInput _input;
        private FixedJoystick _joystick;
        private Item _currentItemInHands;

        private readonly DiContainer _playerContainer;

        #region Inject Method
        [Inject]
        private void Construct(FixedJoystick joystick)
        {
            _joystick = joystick;
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            var playerInput = this.transform.GetComponent<PlayerInput>();
            _input = playerInput;
            _input.OnClick += RaycastItem;
            _input.OnDropClick += DropItem;
        }

        private void Update()
        {
            MovePlayer();
            RotateCamera();
            UpdateItemPosition();
            CheckDropUI();
            _camera.CameraToPosition(this.transform.position);
        }
        #endregion

        #region Private Methods
        private void RotateCamera()
        {
            var rotateValues = _input.GetRotateValues();
            _camera.RotateCamera(_input.GetRotateValues());
        }

        private void MovePlayer()
        {
            var dir = _input.GetMoveDirection(_joystick);
            _controller.Move(new Vector3(dir.x, -9.81f, dir.y) * Time.deltaTime);
        }

        private void RaycastItem(PointerEventData clickData)
        {
            if (_currentItemInHands != null) return;

            var ray = _camera.CameraHandler.ScreenPointToRay(clickData.position);
            if (Physics.Raycast(ray, out RaycastHit hit, _distanceItem, 1 << _itemLayerMask))
            {
                TakeItem(hit.transform.gameObject);
            }
        }

        private void TakeItem(GameObject item)
        {
            _currentItemInHands = item.GetComponent<Item>();
            _currentItemInHands.DisableCollision();
            _currentItemInHands.SetLayer(_itemInHandsLayerMask);
        }

        private void UpdateItemPosition()
        {
            if (_currentItemInHands == null) return;

            _currentItemInHands.transform.position = _camera.CameraHandler.transform.position + (_camera.CameraHandler.transform.forward * 2f) + (_camera.CameraHandler.transform.up * -0.3f);
            _currentItemInHands.transform.rotation = _camera.CameraHandler.transform.rotation;
        }

        private void DropItem()
        {
            if (_currentItemInHands == null) return;

            _currentItemInHands.Throw(this.transform.position);
            _currentItemInHands.ResetLayer();
            _currentItemInHands = null;
        }

        private void CheckDropUI()
        {
            if (_currentItemInHands)
            {
                _input.DropUI.SetActive(true);
            }
            else
            {
                _input.DropUI.SetActive(false);
            }
        }
        #endregion
    }
}