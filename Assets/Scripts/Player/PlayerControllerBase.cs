using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// Abstract base for any PlayerController. Holds shared references,
    /// leaves movement logic to derived classes.
    /// A 3D variant (<see cref="PlayerController3D"/>) is provided; you can
    /// derive others from it (PlayerController2D, PlayerControllerTopDown...).
    /// </summary>
    [RequireComponent(typeof(PlayerStats), typeof(InputHandler))]
    public abstract class PlayerControllerBase : MonoBehaviour
    {
        [SerializeField] protected PlayerStats _stats;
        [SerializeField] protected InputHandler _input;

        public bool IsControlEnabled { get; protected set; } = true;
        public Vector3 CurrentVelocity { get; protected set; }

        protected virtual void Awake()
        {
            if (_stats == null) _stats = GetComponent<PlayerStats>();
            if (_input == null) _input = GetComponent<InputHandler>();
            Debug.Assert(_stats != null, "[PlayerControllerBase] _stats is null"); // R5
            Debug.Assert(_input != null, "[PlayerControllerBase] _input is null"); // R5
        }

        public void EnableControl() { IsControlEnabled = true; }
        public void DisableControl() { IsControlEnabled = false; CurrentVelocity = Vector3.zero; }
    }
}
