using Playground.Services.Input;
using UnityEngine;
using Zenject;

namespace Playground.Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _gravityMultiplier = 1f;

        [Header("Ground")]
        [SerializeField] private Transform _checkGroundTransform;
        [SerializeField] private float _checkGroundRadius = 1f;
        [SerializeField] private LayerMask _checkGroundLayerMask;

        private Vector3 _fallVector;
        private IInputService _inputService;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            Vector2 axis = _inputService.Axis;
            Vector3 moveVector = transform.right * axis.x + transform.forward * axis.y;
            moveVector *= _speed;

            _characterController.Move(moveVector * Time.deltaTime);

            bool isGrounded =
                Physics.CheckSphere(_checkGroundTransform.position, _checkGroundRadius, _checkGroundLayerMask);

            if (isGrounded)
            {
                _fallVector.y = 0;
            }

            if (_inputService.IsJump) { }

            Debug.Log($"isGrounded = {isGrounded}");

            float gravity = Physics.gravity.y * _gravityMultiplier;
            _fallVector.y += gravity * Time.deltaTime;

            _characterController.Move(_fallVector * Time.deltaTime);
        }

        #endregion
    }
}