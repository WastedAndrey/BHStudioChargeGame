using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerRecordItemModel
    {
        private ClientUnit _unit;
        private StringBuilder _builder = new StringBuilder();

        public string Name{ get; private set; }
        public string Score { get => _builder.ToString(); }
        public Action<string> NameChanged;
        public Action<string> ScoreChanged;

        public PlayerRecordItemModel(ClientUnit unit)
        {
            _unit = unit;
        }
        public void Enable()
        {
            OnNameChanged(_unit.PlayerName);
            UpdateScore();
            Subscribe();
        }
        public void Disable()
        {
            Unsubscribe();
        }
        private void Subscribe()
        {
            _unit.NameChanged += OnNameChanged;
            _unit.ScoreChanged += OnScoreChanged;
            _unit.FailsChanged += OnScoreChanged;
        }
        private void Unsubscribe()
        {
            _unit.NameChanged -= OnNameChanged;
            _unit.ScoreChanged -= OnScoreChanged;
            _unit.FailsChanged -= OnScoreChanged;
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

