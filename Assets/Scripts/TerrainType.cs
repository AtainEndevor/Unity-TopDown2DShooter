using System;
using UnityEngine;

/*
 * Serialized script to hold tile types. This is pulled from the Biomes and returns a random tile from its
 * collection along with information on that tile such as movement modifications of if you can even walk on it.
 */
[Serializable]
public class TerrainType {
    public string Name;
    public bool IsWalkable = true;
    public float SpeedMultiplier = 1;
    public Sprite Tile;
    public bool IsAnimated;
    public RuntimeAnimatorController AnimationController;

    public Sprite getTileSprite()
    {
        return Tile;
    }
}
