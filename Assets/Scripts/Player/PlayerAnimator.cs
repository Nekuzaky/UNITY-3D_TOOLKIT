using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// Generic bridge between PlayerControllerBase and an Animator. Assumes
    /// "Speed" (float) and "Grounded" (bool) parameters; names are configurable.
    /// </summary>
    [RequireComponent(typeof(PlayerControllerBase))]
    public sealed class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _speedParam = "Speed";
        [SerializeField] private string _groundedParam = "Grounded";
        [SerializeField] private string _jumpParam = "Jump";

        private PlayerControllerBase _controller;
        private int _speedHash;
        private int _groundedHash;
        private int _jumpHash;

        private void Awake()
        {
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
            _controller = GetComponent<PlayerControllerBase>();
            Debug.Assert(_animator != null, "[PlayerAnimator] _animator is null"); // R5
            Debug.Assert(_controller != null, "[PlayerAnimator] _controller is null"); // R5
            _speedHash = Animator.StringToHash(_speedParam);
            _groundedHash = Animator.StringToHash(_groundedParam);
            _jumpHash = Animator.StringToHash(_jumpParam);
        }

        private void Update()
        {
            if (_animator == null || _controller == null) return;
            Vector3 v = _controller.CurrentVelocity;
            v.y = 0f;
            _animator.SetFloat(_speedHash, v.magnitude);
            if (_controller is PlayerController3D ctrl3D) _animator.SetBool(_groundedHash, ctrl3D.IsGrounded);
        }

        public void TriggerJump() { if (_animator != null) _animator.SetTrigger(_jumpHash); }
    }
}
