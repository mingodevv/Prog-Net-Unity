using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerHandling
{
    public class LocalPlayerController : MonoBehaviour
    {
        #region Singleton
        public static LocalPlayerController Instance { get; private set; }

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
    
        private InputSystem_Actions m_actions;
    
    
        // Setter pour le champion
        private ChampionCharacter m_championCharacter;

        public void SetChampionCharacter(ChampionCharacter a_championCharacter)
        {
            m_championCharacter = a_championCharacter;
        }
    
    
        void Start()
        {
            m_actions = new InputSystem_Actions();
            m_actions.Enable();
        
            m_actions.Player.Interact.started += HandleInteractStarted;
        }

        private void Update()
        {
            if (!m_championCharacter)
                return;
        
            // On récupère et envoie l'input du movement au character.
            var moveInput = m_actions.Player.Move.ReadValue<Vector2>();
            m_championCharacter.SetMovementInput(moveInput);
        }

        private void HandleInteractStarted(InputAction.CallbackContext a_obj)
        {
            Debug.Log("Interact started");
        }
    }
}
