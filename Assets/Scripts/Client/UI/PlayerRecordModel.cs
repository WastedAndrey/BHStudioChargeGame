using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerRecordModel
    {
        private ClientUnit _unit;
        private StringBuilder _builder = new StringBuilder();

        public string Name{ get; private set; }
        public string Score { get => _builder.ToString(); }
        public Action<string> NameChanged;
        public Action<string> ScoreChanged;

        public PlayerRecordModel(ClientUnit unit)
        {
            _unit = unit;
            OnNameChanged(_unit.PlayerName);
            UpdateScore();
            Subscribe();
        }
        public void Destroy()
        {
            Unsubscribe();
        }
        private void Subscribe()
        {
            if (_unit != null)
            {
                _unit.NameChanged += OnNameChanged;
                _unit.ScoreChanged += OnScoreChanged;
                _unit.FailsChanged += OnScoreChanged;
            } 
        }
        private void Unsubscribe()
        {
            if (_unit != null)
            {
                _unit.NameChanged -= OnNameChanged;
                _unit.ScoreChanged -= OnScoreChanged;
                _unit.FailsChanged -= OnScoreChanged;
            }
        }
       

        private void OnNameChanged(string name)
        {
            Name = name;
            NameChanged?.Invoke(name);
        }

        private void OnScoreChanged(short score)
        {
            UpdateScore();
        }
        private void UpdateScore()
        {
            _builder.Clear();
            _builder.Append(_unit.Score.ToString());
            _builder.Append("/");
            _builder.Append(_unit.Fails.ToString());
            ScoreChanged?.Invoke(Score);
        }
    }
}

