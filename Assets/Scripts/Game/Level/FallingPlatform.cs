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

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _startPosition = transform.position;
            _endPosition = transform.position;
            _endPosition.y = -2.36f;
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
            sequence.Append(transform.DOMove(_endPosition, _animationDuration));
        }

        #endregion
    }
}