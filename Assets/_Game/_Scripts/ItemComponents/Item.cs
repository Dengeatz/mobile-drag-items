using UnityEngine;

namespace TestTask.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _boxCollider;
        private int _startLayer;

        public Rigidbody RigidbodyHandler { get => _rigidbody; }

        #region Unity Methods
        private void Awake()
        {
            _startLayer = this.transform.gameObject.layer;
        }
        #endregion

        #region Public Methods
        public void EnableCollision()
        {
            _boxCollider.enabled = true;
        }

        public void DisableCollision() 
        {
            _boxCollider.enabled = false;
        }

        public void SetLayer(int layer)
        {
            this.transform.gameObject.layer = layer;
        }

        public void ResetLayer()
        {
            this.transform.gameObject.layer = _startLayer;
        }

        public void Throw(Vector3 playerPosition)
        {
            EnableCollision();

            if (Physics.SphereCast(this.transform.position, 5f, this.transform.forward, out _) || this.transform.position.y < 0f)
            {
                this.transform.position = playerPosition;
            }
            else
            {
                _rigidbody.AddForce(this.transform.forward * 100f);
            }
        }
        #endregion
    }
}
