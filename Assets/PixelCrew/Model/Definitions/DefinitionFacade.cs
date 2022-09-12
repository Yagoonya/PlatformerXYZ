using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefinitionFacade", fileName = "DefinitionFacade")]
    public class DefinitionFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDefinition _items;

        public InventoryItemsDefinition Items => _items;
        
        private static DefinitionFacade _instance;
        public static DefinitionFacade I => _instance == null ? LoadDefinitions() : _instance;

        private static DefinitionFacade LoadDefinitions()
        {
            return _instance = Resources.Load<DefinitionFacade>("DefinitionFacade");
        }
    }
}