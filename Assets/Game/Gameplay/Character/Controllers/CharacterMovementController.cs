using UnityEngine;
using System.Collections;
using Unity.Netcode;
using UnityEngine.Serialization;

public class CharacterMovementController : MonoBehaviour
{
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float rollForce = 4.5f;
    
    private Rigidbody _rigidbody;
    private CharacterAnimationController animController;

    private Vector3 _moveDirection;
    private bool _isGrounded;
    private bool _isRolling;
    private bool _isSprinting;

    public Rigidbody CharacterRigidbody
    {
        get => _rigidbody;
        set => _rigidbody = value;
    }

    public CharacterAnimationController AnimController
    {
        get => animController;
        set => animController = value;
    }

    void Update()
    {
        float currentSpeed = moveSpeed;
        if (_isSprinting)
            currentSpeed *= sprintMultiplier;
        
        Vector3 move = _moveDirection * currentSpeed;
        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.x = move.x;
        velocity.z = move.z;
        _rigidbody.linearVelocity = velocity;
        
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, 10f * Time.deltaTime);
        }
        
        float moveAmount = _moveDirection.magnitude;
        if (AnimController != null)
        {
            float animSpeed ;
            if (moveAmount > 0.1f)
                animSpeed = _isSprinting ? 1f : 0.5f;
            else
                animSpeed = 0f;

            AnimController.SetSpeed(animSpeed);
        }

    }
    

    public void SetMoveDirection(Vector2 input)
    {
        Vector3 forward = gameObject.transform.forward;
        Vector3 right = gameObject.transform.right;
        forward.y = 0f;
        right.y = 0f;
        _moveDirection = (right * input.x + forward * input.y).normalized;
    }

    public void Jump()
    {
        if (_isGrounded && !_isRolling)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    public void Roll()
    {
        if (_isGrounded && !_isRolling && _moveDirection.sqrMagnitude > 0.1f)
            StartCoroutine(RollCoroutine());
    }

    private IEnumerator RollCoroutine()
    {
        _isRolling = true;

        if (AnimController != null)
            AnimController.SetRolling();

        _rigidbody.AddForce(_moveDirection * rollForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.6f);
        _isRolling = false;
    }

    public void SetSprinting(bool state)
    {
        _isSprinting = state;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _isGrounded = true;
    }
}
