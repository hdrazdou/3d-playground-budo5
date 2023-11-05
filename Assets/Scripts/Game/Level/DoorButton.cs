using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Playground.Game.Level
{
    public class DoorButton : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private Door _door;

        [Header("Animation")]
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _unclickDelay;

        private bool _isClicked;
        private bool _isInTrigger;

        private Tween _tween;

        #endregion

        #region Unity lifecycle

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter");

            _isInTrigger = true;
            PlayClickAnimation();
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("OnTriggerExit");

            TryPlayUnclickAnimation();
            _isInTrigger = false;
        }


        [Button]
        private void PlayClickAnimation()
        {
            Debug.Log("PlayClickAnimation");

            _tween.Kill();
            _tween = transform
                .DOMove(_endPosition, _animationDuration)
                .SetUpdate(UpdateType.Fixed)
                .OnComplete(() =>
                {
                    _isClicked = true;
                    TryPlayUnclickAnimation();
                    _door.Open();
                });
            
            Debug.Log($"_isClicked = {_isClicked}");
        }

        [Button]
        private void PlayUnclickAnimation()
        {
            Debug.Log("PlayUnclickAnimation");

            _isClicked = false;
            _tween?.Kill();
            _tween = transform
                .DOMove(_startPosition, _animationDuration)
                .SetUpdate(UpdateType.Fixed)
                .SetDelay(_unclickDelay)
                .OnComplete(() => _door.Close());
        }
        private void TryPlayUnclickAnimation()
        {
            Debug.Log($"TryPlayUnclickAnimation _isClicked = {_isClicked}, _isInTrigger = {_isInTrigger}");

            if (_isClicked && !_isInTrigger)
            {
                PlayUnclickAnimation();
            }
        }

        #endregion
    }
}