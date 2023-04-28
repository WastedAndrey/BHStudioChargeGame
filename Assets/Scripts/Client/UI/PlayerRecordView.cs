using System.Text;
using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerRecordView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _playerName;
        [SerializeField]
        private TextMeshProUGUI _playerScore;

        private PlayerRecordModel _model;

        public void SetModel(PlayerRecordModel model)
        {
            _model = model;
            OnNameChanged(_model.Name);
            OnScoreChanged(_model.Score);
            Subscribe();
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
            if(_model != null)
                Subscribe();
        }
        private void OnDisable()
        {
            if (_model != null)
                Unsubscribe();
        }
        private void OnDestroy()
        {
            if (_model != null)
                _model.Destroy();
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

