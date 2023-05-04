
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
        {
            var newRecordTable = Instantiate(_prefabRecordTable);
            newRecordTable.SetModel(new PlayerRecordTableModel(_clientContext));
            newRecordTable.Closed += OnElementClosed;
            newRecordTable.transform.SetParent(_canvas.transform, false);
            _uiElements.Add(newRecordTable);
        }

        public void CreateGameMessage()
        {
            var newGameMessage = Instantiate(_prefabGameMessage);
            newGameMessage.SetModel(new GameMessageModel(_clientContext));
            newGameMessage.Closed += OnElementClosed;
            newGameMessage.transform.SetParent(_canvas.transform, false);
            _uiElements.Add(newGameMessage);
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