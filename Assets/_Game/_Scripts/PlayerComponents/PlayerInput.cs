using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TestTask.PlayerComponents
{
    public class PlayerInput : MonoBehaviour
    {
        [Inject] private PlayerTouchInput _rotateInput;
        [Inject] private PlayerDropUI _dropUI;
        private float _xRotateValue;
        private float _yRotateValue;
        
        public Action OnDropClick;
        public Action<PointerEventData> OnClick;

        public PlayerDropUI DropUI { get => _dropUI; }

        #region Unity Methods
        private void Awake()
        {
            _rotateInput.OnScreenClick += ClickHandler;
            _dropUI.OnDropButton += OnDropClickHandler;
        }

        private void Update()
        {
            Rotate();
        }
        #endregion

        #region Public Methods
        public Vector2 GetRotateValues()
        {
            return new Vector2(_xRotateValue, _yRotateValue);
        }

        public Vector2 GetMoveDirection(FixedJoystick joystick)
        {
            return joystick.Direction;
        }
        #endregion

        #region Private Methods
        private void Rotate()
        {
            if (!_rotateInput.IsDragged) return;

            _xRotateValue -= _rotateInput.YValue;
            _yRotateValue += _rotateInput.XValue;
            _xRotateValue = Mathf.Clamp(_xRotateValue, -90f, 90f);
        }

        private void ClickHandler(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData);
        }

        private void OnDropClickHandler()
        {
            OnDropClick?.Invoke();
        }
        #endregion
    }
}