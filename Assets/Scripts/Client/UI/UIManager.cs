
using System.Collections.Generic;
using UnityEngine;

namespace ChargeGame
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance { get => _instance; }

        [Header("Global links")]
        [SerializeField]
        private ClientContext _clientContext;
        [Header("Prefabs")]
        [SerializeField]
        private PlayerRecordTableView _prefabRecordTable;
        [SerializeField]
        private GameMessageView _prefabGameMessage;
        [Header("Components")]
        [SerializeField]
        private Canvas _canvas;

        private HashSet<UIElement> _uiElements = new HashSet<UIElement>();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                CreateRecordTable();
                CreateGameMessage();
            }
            else
                Destroy(this.gameObject);
        }

        public void CreateRecordTable()
        { // здесь могло бы быть что-то вроде метода OpenWindow<КлассОкна>, который в своей реализации открывал бы префаб
            // через adressables или типа того. Но в данном задании мне показалось избыточным
            var newRecordTable = Instantiate(_prefabRecordTable);
            newRecordTable.SetModel(new PlayerRecordTableModel(_clientContext));
            InitUIElement(newRecordTable);
        }

        public void CreateGameMessage()
        {
            var newGameMessage = Instantiate(_prefabGameMessage);
            newGameMessage.SetModel(new GameMessageModel(_clientContext));
            InitUIElement(newGameMessage);
        }

        private void InitUIElement(UIElement uiElement)
        {
            uiElement.Closed += OnElementClosed;
            uiElement.SetParent(_canvas.transform);
            _uiElements.Add(uiElement);
        }

        public void CloseAll()
        {
            var tempList = new List<UIElement>(_uiElements);
            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].CloseElement();
            }
        }

        private void OnElementClosed(UIElement element)
        {
            if (_uiElements.Contains(element))
            {
                _uiElements.Remove(element);
            }
        }
    }
}