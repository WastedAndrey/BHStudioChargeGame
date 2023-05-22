using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerRecordItemView : ViewBase, UIElement
    {
        [SerializeField]
        private TextMeshProUGUI _playerName;
        [SerializeField]
        private TextMeshProUGUI _playerScore;

        private PlayerRecordItemModel _model;

        public void SetModel(PlayerRecordItemModel model)
        {
            _model = model;
            Subscribe();
            _model.Enable();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        private void Subscribe()
        {
            _model.NameChanged += OnNameChanged;
            _model.ScoreChanged += OnScoreChanged;
        }
        private void Unsubscribe()
        {
            _model.NameChanged -= OnNameChanged;
            _model.ScoreChanged -= OnScoreChanged;
        }
        private void OnEnable()
        {
            _model?.Enable();
        }
        private void OnDisable()
        {
            _model?.Disable();
        }
       

        private void OnNameChanged(string name)
        {
            _playerName.text = name;
        }

        private void OnScoreChanged(string score)
        {
            _playerScore.text = score;
        }
    }
}

