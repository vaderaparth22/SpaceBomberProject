using UnityEngine;
using Bombs;

namespace Managers
{
    public class BombManager
    {
        #region Singleton
        private BombManager() { }
        private static BombManager _instance;
        public static BombManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BombManager();
                return _instance;
            }
        }
        #endregion
        private BombFactory bombFactory;

        public void Initialize()
        {
            bombFactory = new BombFactory();
        }

        public void GameStart()
        {

        }

        public void Refresh()
        {
           
        }

        public void FixedRefresh()
        {

        }

        public void ThrowBomb(BombType equippedBombType, Vector3 direction, Vector3 shootPos, Vector3 forwardDir, float forceValue)
        {
            Bomb fuseBombObj = bombFactory.CreateBomb( equippedBombType, shootPos, Quaternion.identity);
            Rigidbody bombRB = fuseBombObj.GetComponent<Rigidbody>();
            //bombRB.transform.forward = direction;
            bombRB.velocity = forwardDir * forceValue;
            Vector3 temp = bombRB.velocity;
            temp.y += Mathf.Lerp(0f, 13f, (forceValue / 25f));
            bombRB.velocity = temp;
        }
    }
}