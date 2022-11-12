using PixelCrew.UI.Settings;
using PixelCrew.UI.Windows.PauseMenu;
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

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.NextItem();
            }
        }

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.canceled && context.duration < 1)
            {
                _hero.UseInventoryItem(false);
            }

            if (context.canceled && context.duration > 1)
            {
                _hero.UseInventoryItem(true);
            }
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("throw");
                _hero.SetImmune();
            }
        }

        public void OnPauseGame(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var optionWindow = FindObjectOfType<SettingsWindow>();
                if (optionWindow != null)
                    optionWindow.Close();
                else
                    _pause.PauseGame();
            }
        }
    }
}