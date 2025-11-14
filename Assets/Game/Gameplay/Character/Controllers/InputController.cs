using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    #region Singleton
    public static InputController Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
    #endregion
    
    public void SetCharacter(Character aCharacter)
    {
        _character = aCharacter;

        _camera.Target.TrackingTarget = aCharacter.transform; 
    }
    
    [SerializeField] private CinemachineCamera _camera; 
    
    private Character _character;

    private InputSystem_Actions m_actions;
    
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _crouchAction;
    private InputAction _rollAction;
    private InputAction _sprintAction;
    private InputAction _skill1Action;
    private InputAction _skill2Action;
    private InputAction _skill3Action;

    void Start()
    {
        m_actions = new InputSystem_Actions();
        m_actions.Enable();

        // _moveAction = _playerInput.actions["Move"];
        // _jumpAction = _playerInput.actions["Jump"];
        // _crouchAction = _playerInput.actions["Crouch"];
        // _rollAction = _playerInput.actions["Roll"];
        // _sprintAction = _playerInput.actions["Sprint"]; 
        // _skill1Action = _playerInput.actions["Skill1"];
        // _skill2Action = _playerInput.actions["Skill2"];
        // _skill3Action = _playerInput.actions["Skill3"];
    }

    void OnEnable()
    {
		return; 
        _jumpAction.performed += OnJump;
        _rollAction.performed += OnRoll;
        _sprintAction.performed += OnSprint;
        _sprintAction.canceled += OnSprintCanceled;
        
        _skill1Action.performed += OnSkill1;
        _skill2Action.performed += OnSkill2;
        _skill3Action.performed += OnSkill3;
    }

    void OnDisable()
    {
		return; 
        _jumpAction.performed -= OnJump;
        _rollAction.performed -= OnRoll;
        _sprintAction.performed -= OnSprint;
        _sprintAction.canceled -= OnSprintCanceled;

        _skill1Action.performed -= OnSkill1;
        _skill2Action.performed -= OnSkill2;
        _skill3Action.performed -= OnSkill3;
    }

    void Update()
    {
        if (!_character)
            return;
        
        Vector2 moveInput = m_actions.Player.Move.ReadValue<Vector2>();
        _character.MovementController.SetMoveDirection(moveInput);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _character.MovementController.Jump();
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        _character.MovementController.Roll();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        _character.MovementController.SetSprinting(true);
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        _character.MovementController.SetSprinting(false);
    }

    private void OnSkill1(InputAction.CallbackContext context)
    {
        // CharacterSkillController.Instance?.ActivateSkill("Skill1");
    }

    private void OnSkill2(InputAction.CallbackContext context)
    {
        // CharacterSkillController.Instance?.ActivateSkill("Skill2");
    }

    private void OnSkill3(InputAction.CallbackContext context)
    {
        // CharacterSkillController.Instance?.ActivateSkill("Skill3");
    }
}
