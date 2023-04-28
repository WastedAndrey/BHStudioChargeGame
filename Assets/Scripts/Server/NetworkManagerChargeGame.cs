using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChargeGame
{
    public class NetworkManagerChargeGame : NetworkRoomManager
    {
        private class ClientInfo
        {
            public ClientToServer ToServer;
            public ClientFromServer FromServer;
            public ServerUnit ServerUnit;
            public ClientUnit ClientUnit;
            public ClientInfo(ClientToServer toServer, ClientFromServer fromServer, ServerUnit serverUnit, ClientUnit clientUnit)
            {
                ToServer = toServer;
                FromServer = fromServer;
                ServerUnit = serverUnit;
                ClientUnit = clientUnit;
            }
        }

        [Header("GlobalLinks")]
        [SerializeField]
        private GlobalSettings _globalSettings;
        [SerializeField]
        private SpawnPointsSettings _spawnPoints;
        [Header("Prefabs")]
        [SerializeField]
        private ServerUnit _prefabServerUnit;
        [SerializeField]
        private ClientUnit _prefabClientUnit;
        private Dictionary<int, ClientInfo> _clientInfoData = new Dictionary<int, ClientInfo>();
        private SimpleTimer _winTimer = new SimpleTimer();

        #region Server Callbacks

        /// <summary>
        /// This is called on the server when a client disconnects.
        /// </summary>
        /// <param name="conn">The connection that disconnected.</param>
        public override void OnRoomServerDisconnect(NetworkConnectionToClient conn) 
        {
            if (_clientInfoData.ContainsKey(conn.connectionId))
            {
                NetworkServer.Destroy(_clientInfoData[conn.connectionId].ClientUnit.gameObject);
                _clientInfoData[conn.connectionId].ServerUnit.ScoreChanged -= CheckScore;
                Destroy(_clientInfoData[conn.connectionId].ServerUnit.gameObject);
                _clientInfoData.Remove(conn.connectionId);
            }
        }

        /// <summary>
        /// This is called on the server when a networked scene finishes loading.
        /// </summary>
        /// <param name="sceneName">Name of the new scene.</param>
        public override void OnRoomServerSceneChanged(string sceneName) 
        {
            _clientInfoData.Clear();

            if (sceneName == GameplayScene)
            {
                _spawnPoints.ResetPositions();
            }
        }

        /// <summary>
        /// This is called on the server when it is told that a client has finished switching from the room scene to a game player scene.
        /// <para>When switching from the room, the room-player is replaced with a game-player object. This callback function gives an opportunity to apply state from the room-player to the game-player object.</para>
        /// </summary>
        /// <param name="conn">The connection of the player</param>
        /// <param name="roomPlayer">The room player object.</param>
        /// <param name="gamePlayer">The game player object.</param>
        /// <returns>False to not allow this player to replace the room player.</returns>
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            int playerId = conn.connectionId;
            string playerName = $"Player {playerId}";
            string objectSuffix = $" Id: {playerName}";

            gamePlayer.name += objectSuffix;
            ClientToServer clientToServer = gamePlayer.GetComponent<ClientToServer>();
            ClientFromServer clientFromServer = gamePlayer.GetComponent<ClientFromServer>();
            clientFromServer.Init(playerId);

            ServerUnit serverUnit = Instantiate(_prefabServerUnit, _spawnPoints.GetNewPosition(), Quaternion.identity);
            serverUnit.name += objectSuffix;
            serverUnit.ScoreChanged += CheckScore;
            serverUnit.Init(playerName, playerId, clientToServer);

            ClientUnit clientUnit = Instantiate(_prefabClientUnit, serverUnit.transform.position, serverUnit.transform.rotation);
            clientUnit.name += objectSuffix;
            clientUnit.SetUnit(serverUnit);

            NetworkServer.Spawn(clientUnit.gameObject, conn);

            ClientInfo clientInfo = new ClientInfo(clientToServer, clientFromServer, serverUnit, clientUnit);
            _clientInfoData.Add(playerId, clientInfo);

            return base.OnRoomServerSceneLoadedForPlayer(conn, roomPlayer, gamePlayer);
        }

        #endregion


        // TODO Better move this logics to other class
        #region Game Logics
        private void CheckScore(ServerUnit unit, short score)
        {
            if (score >= _globalSettings.ScoreForWin && _winTimer.IsActive == false)
            {
                SendWinnerMessage(unit.PlayerId);
                _winTimer.Elapsed += StartNewRound;
                _winTimer.Start(_globalSettings.NewRoundAwaitTime);
            }        
        }

        private void SendWinnerMessage(int winnerId)
        {
            _clientInfoData.First().Value.FromServer.SetPlayerWin(winnerId);
        }

        private void StartNewRound()
        {
            _winTimer.Elapsed -= StartNewRound;
            foreach (var clientInfo in _clientInfoData)
            {
                clientInfo.Value.ServerUnit.transform.position = _spawnPoints.GetNewPosition();
                clientInfo.Value.ServerUnit.ResetScoreAndFails();
            }
        }

        public override void Update()
        {
            _winTimer.Update(Time.deltaTime);
            base.Update();
        }

        #endregion
    }

}

