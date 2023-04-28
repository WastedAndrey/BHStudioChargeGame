using Mirror;
using System;
using UnityEngine;

namespace ChargeGame
{
    public class ClientFromServer : NetworkBehaviour
    {
        [SerializeField]
        private ClientContext _clientContext;

        [SerializeField]
        [SyncVar(hook = nameof(SetPlayerId))]
        private int _playerId;

        public Action<int> PlayerIdChanged;

        public void Init(int playerId)
        {
            SetPlayerId(_playerId, playerId);
        }

        private void SetPlayerId(int oldId, int newId)
        {
            _playerId = newId;
            PlayerIdChanged?.Invoke(newId);
        }

        [ClientRpc]
        public void SetPlayerWin(int winnerId)
        {
            _clientContext.SetPlayerWin(winnerId);
        }
    }
}