using Units;
using UnityEngine;

namespace Level
{
    [System.Serializable]
    public class EnemySpawn
    {
        public UnitType enemyType;
        public Vector3 spawnCoords;
        

        public EnemySpawn(UnitType enemyType, Vector3 spawnCoords)
        {
            this.enemyType = enemyType;
            this.spawnCoords = spawnCoords;
        }
    }
}