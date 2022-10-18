using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefinitionFacade", fileName = "DefinitionFacade")]
    public class DefinitionFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDefinition _items;
        [SerializeField] private ThrowableItemsDefinition _throwableItems;
        [SerializeField] private UsableItemsDefinition _usableItems;
        [SerializeField] private PlayerDefinition _player;

        public InventoryItemsDefinition Items => _items;
        public ThrowableItemsDefinition Throwable => _throwableItems;
        public UsableItemsDefinition Usable => _usableItems;
        
        public PlayerDefinition Player => _player;
        
        private static DefinitionFacade _instance;
        public static DefinitionFacade I => _instance == null ? LoadDefinitions() : _instance;

        private static DefinitionFacade LoadDefinitions()
        {
            return _instance = Resources.Load<DefinitionFacade>("DefinitionFacade");
        }
    }
}