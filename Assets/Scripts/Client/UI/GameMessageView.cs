using System;
using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class GameMessageView : ViewBase, UIElement
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private TextMeshProUGUI _text;

        private GameMessageModel _model;

        public void SetModel(GameMessageModel model)
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
            _model.MessageChanged += OnMessageChanged;
        }
        private void Unsubscribe()
        {
            _model.MessageChanged -= OnMessageChanged;
        }
       
        private void OnEnable()
        {
            _model?.Enable();
        }
        private void OnDisable()
        {
            _model?.Disable();
        }



        private void OnMessageChanged(string message)
        {
            _text.text = message;
            _animator.enabled = true;
            _animator.Play("Show", 0, 0);
        }
    }
}

