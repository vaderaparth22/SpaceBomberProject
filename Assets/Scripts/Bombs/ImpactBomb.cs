using UnityEngine;
namespace Bombs
{

    public class ImpactBomb : Bomb
    {
        
        protected override void Initialize()
        {
           
          
        }

        protected override void Refresh()
        {
           

        }


        private void OnTriggerEnter(Collider other)
        {
            Explode();
        }


    }

}
