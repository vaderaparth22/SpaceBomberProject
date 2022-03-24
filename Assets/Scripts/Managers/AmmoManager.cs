
using UnityEngine;

namespace Managers
{

    public class AmmoManager
    {
        #region Singleton
        private AmmoManager() { }
        private static AmmoManager _instance;
        public static AmmoManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AmmoManager();
                return _instance;
            }
        }
        #endregion

        private Transform ammoSpawnCircleObj;
        private GameObject ammoParent;

        private GameObject[] ammoPrefabArr;

        private float spawnAfter = 3f;
        private int maxAmmoCount = 5;
        private float timerCalculate;
        private LayerMask ammoLayer;
        private int currentAmmoCount;
        public int CurrentAmmoCount
        {
            get { return currentAmmoCount; }
            set
            {
                if (value < 0)
                    throw new System.ArgumentOutOfRangeException(nameof(CurrentAmmoCount), "Current ammo count must not be negative.");
                currentAmmoCount = value;
            }
        }
        public bool IsBombEquiped { get; set; }
        
        public void Initialize()
        {
            ammoPrefabArr = Resources.LoadAll<GameObject>("Prefabs/AmmoBox/");
            currentAmmoCount = 0;
        }

        public void GameStart()
        {
            ammoSpawnCircleObj = GameObject.Find("Ground").transform;
            ammoParent = new GameObject("AmmoParent");
            ammoLayer = LayerMask.GetMask("Ammo");
        }

        public void Refresh()
        {
            RefreshSpawnTimer();
        }

        public void FixedRefresh()
        {

        }

        private void RefreshSpawnTimer()
        {
            timerCalculate += Time.deltaTime;

            if (timerCalculate >= spawnAfter)
            {
                SpawnBomb();
                timerCalculate = 0;
            }
        }

        private void SpawnBomb()
        {
            if (currentAmmoCount >= maxAmmoCount)
                return;

            Vector3 getRandomPos = GetRandomSpawnPosition();
            int boxIndex = GetRandomBoxByIndex();
            GameObject ammo = GameObject.Instantiate(ammoPrefabArr[boxIndex]);
            ammo.transform.position = getRandomPos;
            ammo.transform.SetParent(ammoParent.transform);

            currentAmmoCount++;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Collider[] objColliders;
            Vector3 randomPos;

            do
            {
                randomPos = new Vector3(
                Random.Range(-ammoSpawnCircleObj.localScale.x / 4.8f, ammoSpawnCircleObj.localScale.x / 4.8f), // Dividing by 4.8f so boxes dont spawn too far on the edge
                1.5f,
                Random.Range(-ammoSpawnCircleObj.localScale.z / 4.8f, ammoSpawnCircleObj.localScale.z / 4.8f));

                objColliders = Physics.OverlapSphere(randomPos, 1.5f, ammoLayer);
            }
            while (objColliders.Length > 0);

            return randomPos;
        }   

        private int GetRandomBoxByIndex()
        {
            return Random.Range(0, ammoPrefabArr.Length);
        }
    }

}
