using DG.Tweening;
using UnityEngine;

namespace Playground.Game.Level
{
    public class FallingPlatform : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private float _fallDelay = 1f;
        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private LayerMask _groundMask;
        private BoxCollider _collider;

        private bool _isUsingGravity;
        private Rigidbody _rb;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _startPosition = transform.position;

            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
            _rb.useGravity = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("FallingPlatform OnTriggerEnter");
            FallPlatform();
        }

        #endregion

        #region Private methods

        private void FallPlatform()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(_fallDelay);
            _collider.isTrigger = false;
            _rb.useGravity = true;
            sequence.AppendInterval(_fallDelay);
            transform.DOMove(_startPosition, _animationDuration);
        }

        #endregion
    }
}