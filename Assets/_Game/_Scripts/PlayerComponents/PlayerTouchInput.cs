using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestTask.PlayerComponents
{
    public class PlayerTouchInput : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public bool IsDragged;
        public float XValue;
        public float YValue;

        public Action<PointerEventData> OnScreenClick;

        #region Unity Methods
        public void OnDrag(PointerEventData eventData)
        {
            IsDragged = true;
            XValue = Input.GetAxis("Mouse X");
            YValue = Input.GetAxis("Mouse Y");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            IsDragged = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnScreenClick?.Invoke(eventData);
        }
        #endregion
    }
}
