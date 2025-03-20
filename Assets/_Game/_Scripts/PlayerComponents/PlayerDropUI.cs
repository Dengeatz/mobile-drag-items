using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestTask.PlayerComponents
{
    public class PlayerDropUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject _dropUIView;

        public Action OnDropButton;

        #region Unity Methods
        public void OnPointerClick(PointerEventData eventData)
        {
            OnDropButton?.Invoke();
        }
        #endregion

        #region Public Methods
        public void SetActive(bool isActive)
        {
            _dropUIView.SetActive(isActive);
        }
        #endregion
    }
}