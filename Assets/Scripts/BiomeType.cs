using System;
using UnityEngine;



/*
 * Serialized script to hold Biome types. Based on the value passed in from the 
 * Random Helper, the Biome will return a set of Terrain Types that the tile can
 * be in that Biome. This allows for varying tiles within the same Biome so you
 * don't have the same tile for everything.
 */
[Serializable]
public class BiomeType
{
    public string Name;
    public float Weight;
    public TerrainType[] TerrainTypes;

    public TerrainType getTerrain(float x, float y, int seed)
    {
        return TerrainTypes[RandomHelper.StandardRange(x, y, seed, TerrainTypes.Length)];
    }
}
