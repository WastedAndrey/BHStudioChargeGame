using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerRecordItemView : MonoBehaviour, UIElement
    {
        [SerializeField]
        private TextMeshProUGUI _playerName;
        [SerializeField]
        private TextMeshProUGUI _playerScore;

        private PlayerRecordItemModel _model;

        public Action<UIElement> Closed { get; set; }

        public void SetModel(PlayerRecordItemModel model)
        {
            _model = model;
            Subscribe();
            _model.Enable();
        }
        public void CloseElement()
        {
            Closed?.Invoke(this);
            Destroy(this.gameObject);
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

