using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] BombType bombType;
    [SerializeField] private GameObject bombPickUpParticle;
    [SerializeField] private AudioClip pickupAudioClip;
    [SerializeField] private LayerMask groundLayerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.equippedBombType = this.bombType;

            HudUI.SetBombIcon(bombType);

            if (!AmmoManager.Instance.IsBombEquiped)
            {
                AmmoManager.Instance.IsBombEquiped = true;
            }

            AudioSource.PlayClipAtPoint(pickupAudioClip, transform.position, 1f);

            AmmoManager.Instance.CurrentAmmoCount--;
            GameObject particle =  Instantiate(bombPickUpParticle, transform.position, Quaternion.identity);
            Destroy(particle, 1.5f);
            Destroy(gameObject);
        }

        if (other.gameObject.layer == 7)
        {
            AmmoManager.Instance.CurrentAmmoCount--;
            Destroy(gameObject);
        }
    }
}
