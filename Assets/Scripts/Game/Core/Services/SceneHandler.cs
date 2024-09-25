using System.Collections.Generic;
using Game.Core.Components;
using Game.Core.Entities;
using UnityEngine;

namespace Game.Core.Services
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] private List<SpawnWaypoint> _waypoints;
        [SerializeField] private float _spawnRadius = 1f;

        private const int CountIterations = 20;
        
        public Vector3 GetFreeSpawnPoint()
        {
            var availableWaypoints = new List<SpawnWaypoint>(_waypoints);

            for (var i = 0; i < CountIterations && availableWaypoints.Count > 0; i++)
            {
                var index = Random.Range(0, availableWaypoints.Count);
                var spawnPoint = availableWaypoints[index].Position;

                if (IsPointFree(spawnPoint))
                {
                    return spawnPoint; 
                }

                availableWaypoints.RemoveAt(index);
            }

            return _waypoints[Random.Range(0, _waypoints.Count)].Position;
        }

        private bool IsPointFree(Vector3 position)
        {
            var colliders = Physics.OverlapSphere(position, _spawnRadius);

            foreach (var collider in colliders)
            {
                if (collider.GetComponentInParent<IEntityView>() != null)
                {
                    return false; 
                }
            }

            return true; 
        }
    }
}