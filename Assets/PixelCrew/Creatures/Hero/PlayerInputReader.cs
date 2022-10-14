using PixelCrew.UI.PauseMenu;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Creatures.Hero
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        private PauseMenuController _pause;

        private void Start()
        {
            if (_pause == null)
                _pause = FindObjectOfType<PauseMenuController>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UsePotion();
            }
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.canceled && context.duration < 1)
            {
                _hero.Throw();
            }

            if (context.canceled && context.duration > 1)
            {
                _hero.HoldingThrow();
            }
        }

        public void OnPauseGame(InputAction.CallbackContext context)
        {
            if (context.performed)
                _pause.PauseGame();
        }
    }
}