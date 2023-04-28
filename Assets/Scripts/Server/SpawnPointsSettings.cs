using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeGame
{
    [CreateAssetMenu(fileName = "SpawnPointsSettings", menuName = "ScriptableObjects/SpawnPointsSettings")]
    public class SpawnPointsSettings : ScriptableObject
    {
        [SerializeField]
        private List<Vector3> _spawnPointsPositions = new List<Vector3>();
        private List<Vector3> _unusedSpawnPointsPositions = new List<Vector3>();

        [SerializeField]
        [PropertyOrder(+1)]
        private List<Transform> _spawnPointsTransforms = new List<Transform>();

        [Button]
        private void AddPositionsFromTransforms()
        {
            _spawnPointsPositions.Clear();
            for (int i = 0; i < _spawnPointsTransforms.Count; i++)
            {
                if (_spawnPointsTransforms[i] != null)
                    _spawnPointsPositions.Add(_spawnPointsTransforms[i].position);
            }
            _spawnPointsTransforms.Clear();
        }

        public void ResetPositions()
        {
            _unusedSpawnPointsPositions.Clear();
            for (int i = 0; i < _spawnPointsPositions.Count; i++)
            {
                _unusedSpawnPointsPositions.Add(_spawnPointsPositions[i]);
            }

        }

        public Vector3 GetNewPosition()
        {
            if (_unusedSpawnPointsPositions.Count == 0)
                ResetPositions();

            int rnd = Random.Range(0, _unusedSpawnPointsPositions.Count);
            Vector3 result = _unusedSpawnPointsPositions[rnd];
            _unusedSpawnPointsPositions.RemoveAt(rnd);

            return result;
        }
    }
}
