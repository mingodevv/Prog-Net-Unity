using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 3f;
    public float crouchSpeedMultiplier = 0.75f;
    public float rollForce = 4.5f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float crouchModelOffset = -0.5f;
    public float cameraCrouchOffset = -0.5f;
    public float crouchTransitionSpeed = 5f;

    [Header("Model Reference")]
    [SerializeField] private Transform model;

    [Header("Camera Reference")]
    [SerializeField] private Transform camTransform;

    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Vector3 _moveDirection;
    private bool _isGrounded;
    private bool _isCrouching;
    private bool _isRolling;
    private bool _isSprinting;

    private float _originalHeight;
    private Vector3 _originalModelPosition;
 

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _originalHeight = _collider.height;

        if (model != null)
            _originalModelPosition = model.localPosition;
        
        else
            Debug.LogWarning("⚠️ Caméra non assignée dans Inspector ! Déplacement relatif caméra désactivé.");
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

        forward.Normalize();
        right.Normalize();

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
