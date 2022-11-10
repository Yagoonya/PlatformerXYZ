using PixelCrew.Model.Definitions.Repository;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefinitionFacade", fileName = "DefinitionFacade")]
    public class DefinitionFacade : ScriptableObject
    {
        [SerializeField] private ItemsRepository _items;
        [SerializeField] private ThrowableRepository _throwableItems;
        [SerializeField] private PotionRepository _potions;
        [SerializeField] private PlayerDefinition _player;

        public ItemsRepository Items => _items;
        public ThrowableRepository Throwable => _throwableItems;
        public PotionRepository Potions => _potions;
        
        public PlayerDefinition Player => _player;
        
        private static DefinitionFacade _instance;
        public static DefinitionFacade I => _instance == null ? LoadDefinitions() : _instance;

        private static DefinitionFacade LoadDefinitions()
        {
            return _instance = Resources.Load<DefinitionFacade>("DefinitionFacade");
        }
    }
}