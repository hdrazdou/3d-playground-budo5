using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Playground.Game.Level
{
    public class TestAnim : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Vector3 _startPoint;
        [SerializeField] private Vector3 _midPoint;
        [SerializeField] private Vector3 _endPoint;

        [SerializeField] private float toMidDuration = 2f;
        [SerializeField] private float toEndDuration = 2f;

        #endregion

        #region Public methods

        [Button]
        public void Play()
        {
            PlayAsync().Forget();
        }

        #endregion

        #region Private methods

        private async UniTask PlayAsync()
        {
            await transform.DOMove(_midPoint, toMidDuration).From(_startPoint);
            await transform.DOMove(_endPoint, toEndDuration);
        }

        #endregion
    }
}