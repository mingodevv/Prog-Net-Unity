using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterMovementController))]
public class InputController : MonoBehaviour
{
    [SerializeField] 
    private CharacterAnimationController characterAnimationController; 
    
    private CharacterMovementController _movementController;
    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _crouchAction;
    private InputAction _rollAction;
    private InputAction _sprintAction;

   
    private InputAction _skill1Action;
    private InputAction _skill2Action;
    private InputAction _skill3Action;

    void Awake()
    {
        _movementController = GetComponent<CharacterMovementController>();
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _crouchAction = _playerInput.actions["Crouch"];
        _rollAction = _playerInput.actions["Roll"];
        _sprintAction = _playerInput.actions["Sprint"]; 

       
        _skill1Action = _playerInput.actions["Skill1"];
        _skill2Action = _playerInput.actions["Skill2"];
        _skill3Action = _playerInput.actions["Skill3"];
    }

    void OnEnable()
    {
        _jumpAction.performed += OnJump;
        _crouchAction.performed += OnCrouch;
        _crouchAction.canceled += OnCrouchCanceled;
        _rollAction.performed += OnRoll;
        _sprintAction.performed += OnSprint;
        _sprintAction.canceled += OnSprintCanceled;
        
        
        _skill1Action.performed += OnSkill1;
        _skill2Action.performed += OnSkill2;
        _skill3Action.performed += OnSkill3;
    }

    void OnDisable()
    {
        _jumpAction.performed -= OnJump;
        _crouchAction.performed -= OnCrouch;
        _crouchAction.canceled -= OnCrouchCanceled;
        _rollAction.performed -= OnRoll;
        _sprintAction.performed -= OnSprint;
        _sprintAction.canceled -= OnSprintCanceled;

        _skill1Action.performed -= OnSkill1;
        _skill2Action.performed -= OnSkill2;
        _skill3Action.performed -= OnSkill3;
    }

    void Update()
    {
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();
        _movementController.SetMoveDirection(moveInput);
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            characterAnimationController.setWalking(true);
        }
        else
        {
            characterAnimationController.setWalking(false);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _movementController.Jump();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        _movementController.Crouch(true);
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        _movementController.Crouch(false);
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        _movementController.Roll();
        characterAnimationController.setRolling();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        _movementController.SetSprinting(true);
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        _movementController.SetSprinting(false);
    }
    
    private void OnSkill1(InputAction.CallbackContext context)
    {
        // Ici on appelle le CharacterSkillController
        CharacterSkillController.Instance.ActivateSkill("Skill1");
    }

    private void OnSkill2(InputAction.CallbackContext context)
    {
        CharacterSkillController.Instance.ActivateSkill("Skill2");
    }

    private void OnSkill3(InputAction.CallbackContext context)
    {
        CharacterSkillController.Instance.ActivateSkill("Skill3");
    }

}

