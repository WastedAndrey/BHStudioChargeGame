
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChargeGame
{
    public class PlayerRecordTableModel
    {
        private ClientContext _clientContext;

        private Dictionary<ClientUnit, PlayerRecordItemModel> _units = new Dictionary<ClientUnit, PlayerRecordItemModel>();
        public Action<PlayerRecordItemModel> UnitAdded;
        public Action<PlayerRecordItemModel> UnitRemoved;
        public List<PlayerRecordItemModel> Units { get => _units.Values.ToList(); }

        public PlayerRecordTableModel(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }

        public void Enable()
        {
            UpdateUnits();
            Subscribe();
        }

        public void Disable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _clientContext.UnitAdded += AddClientUnit;
            _clientContext.UnitRemoved += RemoveClientUnit;
        }
        private void Unsubscribe()
        {
            _clientContext.UnitAdded -= AddClientUnit;
            _clientContext.UnitRemoved -= RemoveClientUnit;
        }

        private void AddClientUnit(ClientUnit unit)
        {
            if (_units.ContainsKey(unit) == false)
            {
                _units.Add(unit, new PlayerRecordItemModel(unit));
                UnitAdded?.Invoke(_units[unit]);
            }
        }

        private void RemoveClientUnit(ClientUnit unit)
        {
            if (_units.ContainsKey(unit))
            {
                UnitRemoved?.Invoke(_units[unit]);
                _units.Remove(unit);
            }
        }

        private void UpdateUnits()
        {
            var contextUnits = _clientContext.Units;
            List<ClientUnit> alreadyAddedUnits = _units.Keys.ToList();
            
            for (int i = 0; i < alreadyAddedUnits.Count; i++)
            {
                if (contextUnits.Contains(alreadyAddedUnits[i]) == false)
                {
                    RemoveClientUnit(alreadyAddedUnits[i]);
                }
            }
            foreach (var unit in contextUnits)
            {
                AddClientUnit(unit);
            }
        }
    }
}