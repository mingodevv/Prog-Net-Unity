using UnityEngine;
using System.Collections;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterMovementController : NetworkBehaviour
{
    //TODO: Enlever public sur ces variables ci-dessous ( ne respecte pas les besoin
    
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 3f;
    public float crouchSpeedMultiplier = 0.75f;
    public float rollForce = 4.5f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float crouchModelOffset = -0.5f;
    public float crouchTransitionSpeed = 5f;

    [Header("References")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform camTransform;
    [SerializeField] private CharacterAnimationController animController;

    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Vector3 _moveDirection;
    private bool _isGrounded;
    private bool _isCrouching;
    private bool _isRolling;
    private bool _isSprinting;

    private float _originalHeight;
    private Vector3 _originalModelPosition;

    protected override void OnNetworkPostSpawn()
    {
        base.OnNetworkPostSpawn();

        if (IsOwner)
        {
            InputController.Instance.SetCharacterMovementController(this);
        }
    }
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _originalHeight = _collider.height;

        if (model != null)
            _originalModelPosition = model.localPosition;

        if (animController == null)
            animController = GetComponentInChildren<CharacterAnimationController>();
    }

    void Update()
    {
        if (_isRolling) return;

        float currentSpeed = moveSpeed;
        if (_isSprinting && !_isCrouching)
            currentSpeed *= sprintMultiplier;
        else if (_isCrouching)
            currentSpeed *= crouchSpeedMultiplier;
        
        Vector3 move = _moveDirection * currentSpeed;
        Vector3 velocity = _rb.linearVelocity;
        velocity.x = move.x;
        velocity.z = move.z;
        _rb.linearVelocity = velocity;
        
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, 10f * Time.deltaTime);
        }
        
        float moveAmount = _moveDirection.magnitude;
        if (animController != null)
        {
            float animSpeed ;
            if (moveAmount > 0.1f)
                animSpeed = _isSprinting ? 1f : 0.5f;
            else
                animSpeed = 0f;

            animController.SetSpeed(animSpeed);
        }

        UpdateCrouchVisuals();
    }

    private void UpdateCrouchVisuals()
    {
        if (model != null)
        {
            Vector3 targetModelPos = _originalModelPosition;
            if (_isCrouching) targetModelPos.y += crouchModelOffset;
            model.localPosition = Vector3.Lerp(model.localPosition, targetModelPos, Time.deltaTime * crouchTransitionSpeed);
        }
    }

    public void SetMoveDirection(Vector2 input)
    {
        if (camTransform == null)
        {
            _moveDirection = new Vector3(input.x, 0, input.y);
            return;
        }

        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0f;
        right.y = 0f;
        _moveDirection = (right * input.x + forward * input.y).normalized;
    }

    public void Jump()
    {
        if (_isGrounded && !_isRolling)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    public void Crouch(bool state)
    {
        if (_isRolling) return;

        _isCrouching = state;
        _collider.height = _isCrouching ? crouchHeight : _originalHeight;
        _collider.center = new Vector3(0, _collider.height / 2f, 0);
    }

    public void Roll()
    {
        if (_isGrounded && !_isRolling && _moveDirection.sqrMagnitude > 0.1f)
            StartCoroutine(RollCoroutine());
    }

    private IEnumerator RollCoroutine()
    {
        _isRolling = true;

        if (animController != null)
            animController.SetRolling();

        _rb.AddForce(_moveDirection * rollForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.6f);
        _isRolling = false;
    }

    public void SetSprinting(bool state)
    {
        _isSprinting = state && !_isCrouching;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _isGrounded = true;
    }
}
