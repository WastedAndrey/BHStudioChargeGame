using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ChargeGame
{
    [CreateAssetMenu(fileName = "ClientContext", menuName = "ScriptableObjects/ClientContext")]
    public class ClientContext : ScriptableObject
    {
        public ClientUnit PlayerUnit { get; private set; }

        private HashSet<ClientUnit> _units = new HashSet<ClientUnit>();
        public HashSet<ClientUnit> Units { get => new HashSet<ClientUnit>(_units); }

        public Action<ClientUnit> PlayerUnitChanged;
        public Action<ClientUnit> UnitAdded;
        public Action<ClientUnit> UnitRemoved;

        public Action<string> NewMessageRecieved;

        StringBuilder _builder = new StringBuilder();

        public void SetPlayerUnit(ClientUnit unit)
        {
            PlayerUnit = unit;
            PlayerUnitChanged?.Invoke(unit);
        }

        public void AddUnit(ClientUnit unit)
        {
            if (_units.Contains(unit) == false)
            {
                _units.Add(unit);
                unit.Destroyed += RemoveUnit;
                UnitAdded?.Invoke(unit);
            }
        }

        private void RemoveUnit(ClientUnit unit)
        {
            if (_units.Contains(unit))
            {
                _units.Remove(unit);
                UnitRemoved?.Invoke(unit);
            }
        }

        public void SetPlayerWin(int winnerId)
        {
            foreach (var unit in _units)
            {
                if (unit.PlayerId == winnerId)
                {
                    _builder.Clear();
                    _builder.Append(unit.PlayerName);
                    _builder.Append(" WON!!!");
                    Debug.Log(_builder.ToString());
                    NewMessageRecieved?.Invoke(_builder.ToString());
                }
            }
        }
    }
}
