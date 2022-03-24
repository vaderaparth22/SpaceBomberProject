using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bombs;

public enum BombType
{
    FuseBomb,
    LandMine,
    ImpactBomb
}

public class BombFactory
{
    private readonly Dictionary<BombType, Bomb> loadedBombs = new Dictionary<BombType, Bomb>();

    public BombFactory()
    {
        LoadBombsInDictionary();
    }

    private void LoadBombsInDictionary()
    {
        loadedBombs.Add(BombType.FuseBomb, Resources.Load<FuseBomb>("Prefabs/Bombs/FuseBomb"));
        loadedBombs.Add(BombType.LandMine, Resources.Load<LandMine>("Prefabs/Bombs/LandMine"));
        loadedBombs.Add(BombType.ImpactBomb, Resources.Load<ImpactBomb>("Prefabs/Bombs/Impact"));
    }

    public Bomb CreateBomb(BombType type, Vector3 pos, Quaternion rot)
    {
        Bomb newBomb = GameObject.Instantiate(loadedBombs[type], pos, rot);
        return newBomb;
    }
}
