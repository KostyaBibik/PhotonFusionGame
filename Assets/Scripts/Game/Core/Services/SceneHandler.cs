using System.Collections.Generic;
using Game.Core.Components;
using Game.Core.Entities;
using UnityEngine;

namespace Game.Core.Services
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] private List<SpawnWaypoint> _waypoints;
        [SerializeField] private float _spawnRadius = 2f;

        private const int CountIterations = 10;
        
        public Vector3 GetFreeSpawnPoint()
        {
            Vector3 spawnPoint;
            bool isPointFree;

            for (var i = 0; i < CountIterations; i++)
            {
                spawnPoint = _waypoints[Random.Range(0, _waypoints.Count)].Position;
                isPointFree = IsPointFree(spawnPoint);

                if (isPointFree)
                {
                    return spawnPoint; 
                }
            }

            return _waypoints[Random.Range(0, _waypoints.Count)].Position;
        }

        private bool IsPointFree(Vector3 position)
        {
            var colliders = Physics.OverlapSphere(position, _spawnRadius);

            foreach (var collider in colliders)
            {
                if (collider.GetComponent<IEntityView>() != null)
                {
                    return false; 
                }
            }

            return true; 
        }
    }
}