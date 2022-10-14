using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/PlayerDefinition", fileName = "PlayerDefinition")]
    public class PlayerDefinition : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;
        public int InventorySize => _inventorySize;
        public int MaxHealth => _maxHealth;
    }
}