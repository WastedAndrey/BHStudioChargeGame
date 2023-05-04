using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerRecordTableView : MonoBehaviour, UIElement
    {
        [SerializeField]
        private ClientContext _clientContext;
        [SerializeField]
        private PlayerRecordItemView _prefabRecordView;
        [SerializeField]
        private Transform _layout;
        private PlayerRecordTableModel _model;

        private Dictionary<PlayerRecordItemModel, PlayerRecordItemView> _items = new Dictionary<PlayerRecordItemModel, PlayerRecordItemView>();

        public Action<UIElement> Closed { get; set; }

        public void SetModel(PlayerRecordTableModel model)
        {
            _model = model;
            Subscribe();
            _model.Enable();
        }
        private void OnDestroy()
        {
            Unsubscribe();
        }
        public void CloseElement()
        {
            foreach (var item in _items.Values)
            {
                item.CloseElement();
            }
            Closed?.Invoke(this);
            Destroy(this.gameObject);
        }

        private void OnEnable()
        {
            _model?.Enable();

        }
        private void OnDisable()
        {
            _model?.Disable();
        }

        private void Subscribe()
        {
            _model.UnitAdded += AddClientUnit;
            _model.UnitRemoved += RemoveClientUnit;
        }
        private void Unsubscribe()
        {
            _model.UnitAdded -= AddClientUnit;
            _model.UnitRemoved -= RemoveClientUnit;
        }
        private void AddClientUnit(PlayerRecordItemModel unit)
        {
            if (_items.ContainsKey(unit) == false)
            {
                _items.Add(unit, CreateNewItem(unit));
            }
        }
        private void RemoveClientUnit(PlayerRecordItemModel unit)
        {
            if (_items.ContainsKey(unit))
            {
                _items[unit].CloseElement();
                _items.Remove(unit);
            }
        }
        private PlayerRecordItemView CreateNewItem(PlayerRecordItemModel model)
        {
            var itemView = Instantiate(_prefabRecordView);
            itemView.SetModel(model);
            itemView.transform.SetParent(_layout);
            return itemView;
        }
    }
}
