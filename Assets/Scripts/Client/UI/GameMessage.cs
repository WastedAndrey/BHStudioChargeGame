using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class GameMessage : MonoBehaviour
    {
        [SerializeField]
        private ClientContext _context;
        [SerializeField]
        private TextMeshProUGUI _message;
        [SerializeField]
        private float _messageTime = 5;
        [SerializeField]
        private Gradient _messageColorGradient;
        [SerializeField]
        private AnimationCurve _messageSizeCurve;

        private float _messageTimeCurrent = 0;

        private void Awake()
        {
            _context.NewMessageRecieved += UpdateText;
            this.gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            _context.NewMessageRecieved -= UpdateText;
        }
        private void Update()
        {
            _messageTimeCurrent += Time.deltaTime;

            if (_messageTimeCurrent >= _messageTime)
            {
                this.gameObject.SetActive(false);
                _message.color = _messageColorGradient.Evaluate(0);
                float size = _messageSizeCurve.Evaluate(0);
                this.transform.localScale = new Vector3(size, size, size);
            }
            else
            {
                float value = 1 - (_messageTimeCurrent / _messageTime);
                value = Mathf.Clamp(value, 0f, 1f);
                _message.color = _messageColorGradient.Evaluate(value);
                float size = _messageSizeCurve.Evaluate(value);
                this.transform.localScale = new Vector3(size, size, size);
            }
        }

        private void UpdateText(string message)
        {
            _message.text = message;
            _messageTimeCurrent = 0;
            this.gameObject.SetActive(true);
        }
    }
}

