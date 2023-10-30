using UnityEngine;
using UnityEngine.UI;

namespace Playground.UI
{
    public class SplashScreenImageAnimation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image _image;
        [SerializeField] private float _circleTime = 1f;

        private float _fillSpeed;
        private float _timer;

        #endregion

        #region Properties

        private bool IsClockWise
        {
            get => _image.fillClockwise;
            set => _image.fillClockwise = value;
        }

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _image.fillMethod = Image.FillMethod.Radial360;
            _image.fillOrigin = (int)Image.Origin360.Top;
            _image.fillAmount = 1;
            _fillSpeed = 1 / _circleTime;
        }

        private void Update()
        {
            float speed = _fillSpeed * Time.deltaTime;
            _image.fillAmount += IsClockWise ? speed : -speed;
            if (_image.fillAmount == 0 || _image.fillAmount == 1)
            {
                IsClockWise = !IsClockWise;
            }
        }

        #endregion
    }
}