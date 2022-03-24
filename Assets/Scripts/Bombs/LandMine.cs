using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bombs;

public class LandMine : Bomb
{
    protected override void Initialize()
    {
        
    }

    protected override void Refresh()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ground"))
            Explode();
    }
}
