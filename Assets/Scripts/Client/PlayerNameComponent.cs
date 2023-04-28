using TMPro;
using UnityEngine;

namespace ChargeGame
{
    public class PlayerNameComponent : MonoBehaviour
    {
        [Header("Component links")]
        [SerializeField]
        private TextMeshPro _text;
        [SerializeField]
        private ClientUnit _unit;

        private void Start()
        {
            UpdateText(_unit.PlayerName);
        }

        private void OnEnable()
        {
            _unit.NameChanged += UpdateText;
        }

        private void OnDisable()
        {
            _unit.NameChanged -= UpdateText;
        }

        private void UpdateText(string name)
        {
            _text.text = name;
        }
    }
}
