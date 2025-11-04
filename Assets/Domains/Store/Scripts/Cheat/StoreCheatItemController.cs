using Core;
using UnityEngine;

namespace Store
{
    public class StoreCheatItemController : IController
    {
        private const string VIEW_PREFAB_PATH = "StoreCheatItemView";
        private readonly CheatMapping _cheatMapping;
        private readonly PlayerData _playerData;
        private readonly Transform _parent;
        private StoreCheatView _view;

        public StoreCheatItemController(
            CheatMapping cheatMapping,
            PlayerData playerData,
            Transform parent)
        {
            _cheatMapping = cheatMapping;
            _playerData = playerData;
            _parent = parent;
        }

        public void Start()
        {
            InitView();
            _playerData.DataChanged += OnDataChanged;
        }

        private void Stop()
        {
            if (!_view)
            {
                return;
            }

            _view.ButtonClicked -= OnButtonClicked;
            _view.Dispose();
        }

        private void OnDataChanged()
        {
            RefreshText();
        }

        private void OnButtonClicked()
        {
            _cheatMapping.RewardAction.Execute(_playerData);
        }

        private void InitView()
        {
            var prefab = Resources.Load<StoreCheatView>(VIEW_PREFAB_PATH);
            _view = Object.Instantiate(prefab, _parent);
            var displayer = _cheatMapping.RewardDisplayer;
            _view.Init($"{displayer.GetRewardName()}:", displayer.GetRewardAmount(_playerData));
            _view.ButtonClicked += OnButtonClicked;
        }

        private void RefreshText()
        {
            _view.SetAmount(_cheatMapping.RewardDisplayer.GetRewardAmount(_playerData));
        }
    }
}