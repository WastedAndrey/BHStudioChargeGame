using System.Collections.Generic;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerRecordTable : MonoBehaviour
    {
        [SerializeField]
        private ClientContext _clientContext;
        [SerializeField]
        private PlayerRecordView _prefabRecordView;
        [SerializeField]
        private Transform _layout;


        private Dictionary<ClientUnit, PlayerRecordView> _units = new Dictionary<ClientUnit, PlayerRecordView>();

        private void OnEnable()
        {
            UpdateUnits();
            _clientContext.UnitAdded += AddClientUnit;
        }
        private void OnDisable()
        {
            _clientContext.UnitAdded += AddClientUnit;
        }

       
        private void UpdateUnits()
        {
            var unitsHashset = _clientContext.Units;
            List<ClientUnit> unitsList = new List<ClientUnit>(_units.Keys);
            for (int i = 0; i < unitsList.Count; i++)
            {
                if (unitsHashset.Contains(unitsList[i]) == false)
                {
                    RemoveClientUnit(unitsList[i]);
                }
            }
            foreach (var unit in unitsHashset)
            {
                AddClientUnit(unit);
            }
        }

        private void AddClientUnit(ClientUnit unit)
        {
            if (_units.ContainsKey(unit) == false)
            {
                _units.Add(unit, CreateNewVew(unit));
            }
        }

        private void RemoveClientUnit(ClientUnit unit)
        {
            if (_units.ContainsKey(unit))
            {
                Destroy(_units[unit].gameObject);
                _units.Remove(unit);
            }
        }

        private PlayerRecordView CreateNewVew(ClientUnit unit)
        {
            var model = new PlayerRecordModel(unit);
            var view = Instantiate(_prefabRecordView);
            view.SetModel(model);
            view.transform.SetParent(_layout);
            return view;
        }
    }
}
