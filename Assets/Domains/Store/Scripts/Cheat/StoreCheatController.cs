using Core;
using UnityEngine;

namespace Store
{
    public class StoreCheatController : IController
    {
        private const string CHEAT_MAPPING_FILE_NAME = "CheatMappingHolder";
        private readonly PlayerData _playerData;
        private readonly Transform _parent;

        public StoreCheatController(
            PlayerData playerData,
            Transform parent)
        {
            _playerData = playerData;
            _parent = parent;
        }

        public void Start()
        {
            foreach (var item in LoadMapping().Mapping)
            {
                new StoreCheatItemController(
                    item,
                    _playerData,
                    _parent).Start();
            }
        }

        private CheatMappingHolder LoadMapping()
        {
            return Resources.Load<CheatMappingHolder>(CHEAT_MAPPING_FILE_NAME);
        }
    }
}