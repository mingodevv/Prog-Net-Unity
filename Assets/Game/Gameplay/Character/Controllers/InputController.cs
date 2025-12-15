using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    [SerializeField] private CinemachineCamera _camera;

    private Character _character;
    private AssassinClass _assassin;

    private InputSystem_Actions m_actions;
    private InputAction _attackAction;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        m_actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        if (m_actions == null) return;

        m_actions.Enable();

        // === SKILLS ===
        m_actions.Player.Skill1.performed += _ => _assassin?.Skill_Attack();
        m_actions.Player.Skill2.performed += _ => _assassin?.Skill_Crouch();
        m_actions.Player.Skill3.performed += _ => _assassin?.Skill_PowerUp();

        // === SPRINT ===
        m_actions.Player.Sprint.performed += _ =>
            _character?.MovementController.SetSprinting(true);

        m_actions.Player.Sprint.canceled += _ =>
            _character?.MovementController.SetSprinting(false);
        
        _attackAction.performed += Attack_Click;
        
    }

    private void OnDisable()
    {
        if (m_actions == null) return;

        m_actions.Disable();
    }

    void Update()
    {
        Vector2 moveInput = m_actions.Player.Move.ReadValue<Vector2>();
        _character.MovementController.SetMoveDirection(moveInput);
    }

    public void SetCharacter(Character aCharacter)
    {
        _character = aCharacter;
        _assassin = aCharacter.GetComponent<AssassinClass>();

        _camera.Target.TrackingTarget = aCharacter.transform;
    }

    public void Attack_Click(InputAction.CallbackContext callbackContext)
    {
        _character.Skill_Attack();
    }
    
}