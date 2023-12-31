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

        [Header("Jump")]
        [SerializeField] private float _jumpHeight = 1f;

        private Vector3 _fallVector;
        private IInputService _inputService;

        private Vector3 _moveVector;

        #endregion

        #region Properties

        public Vector3 Velocity => _moveVector;

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
            _moveVector = transform.right * axis.x + transform.forward * axis.y;
            _moveVector *= _speed;

            bool isGrounded =
                Physics.CheckSphere(_checkGroundTransform.position, _checkGroundRadius, _checkGroundLayerMask);

            if (isGrounded && _fallVector.y < 0)
            {
                _fallVector.y = 0;
            }

            float gravity = Physics.gravity.y * _gravityMultiplier;

            if (isGrounded && _inputService.IsJump)
            {
                _fallVector.y = Mathf.Sqrt(_jumpHeight * -2f * gravity);
            }

            Debug.Log($"isGrounded = {isGrounded}");

            _fallVector.y += gravity * Time.deltaTime;
        }

        private void FixedUpdate()
        {
            _characterController.Move(_moveVector * Time.fixedDeltaTime);
            _characterController.Move(_fallVector * Time.fixedDeltaTime);
        }

        #endregion
    }
}