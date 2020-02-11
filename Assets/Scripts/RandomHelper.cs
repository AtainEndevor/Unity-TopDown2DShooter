using UnityEngine;

/*
 * This is where we develop all random functionality. Since it's all based on a seed, we can always recall
 * the same information for saving purposes. All functions will return the same value every time for each
 * x and y passed to it based off the seed, so even though it's "random" to the player, the system will
 * always pull the same information whenever the player visits a certain point.
 */

public class RandomHelper {

    public static int StandardRange(int x, int y, int seed, int range)
    {
        return StandardRange((float)x, (float)y, seed, range);
    }

    /* This is your everyday random function that mimics standard noise. There is no pattern from one position
     * to the next. This will primarity be used within the Terrain generation to mix up the various tiles to
     * give the terrain a more natural look. Example: http://imgur.com/a/3oN5m
     */
    public static int StandardRange(float x, float y, int seed, int range)
    {
        uint hash = (uint)seed;
        hash ^= (uint)x;
        hash *= 0x51d7348d;
        hash ^= 0x85dbdda2;
        hash = (hash << 16) ^ (hash >> 16);
        hash *= 0x7588f287;
        hash ^= (uint)y;
        hash *= 0x487a5559;
        hash ^= 0x64887219;
        hash = (hash << 16) ^ (hash >> 16);
        hash *= 0x63288691;
        return (int)(hash % range);
    }

    public static float TemperatureRange(int x, int y, int seed)
    {
        return TemperatureRange((float)x, (float)y, seed);
    }

    /*
     * The Perlin Algorithms return a more coordinated noise where the points surrounding the point called are
     * similar. This results in a wave like affect that we can use more for Biome establishment. Manipulating
     * the values returned by the Perlin Noise function blur the 'edges' and sizes of the biomes.
     * Example: http://imgur.com/a/v2xnQ
     * 
     * We determine Biomes by layering several Perlin maps on top of each other:
     *      Temperature
     *      Moisture
     *      Elevation
     *      Salinity
     *      
     *                  L                Moisture                 H
     *                  <----------------------------------------->
     *  E ^         H ^
     *  L |           |  
     *  E |         T |  Desert      Grassland    Tropical Forest
     *  V |         E |  Desert      Grassland    Temperate Forest
     *  A |         M |  Tundra      Snow         Boreal Forest
     *  T |         P |  Tundra      Snow         Frozen Forest
     *  I |           |  Ice         Ice          Ice
     *  O |         C |
     *  N | Beach
     *    | Water
     * 
     * *Currently Salinity is not being used, but
     * 
     *           ^
     *  Salinity | Marine           Rocky Terrain
     *           | Freshwater       Smooth Terrain
     * 
     */


    //Temperature Layer
    public static float TemperatureRange(float x, float y, int seed)
    {
        float v1 = 1.00f;
        float v2 = 0.50f;
        float v3 = 0.25f;
        float v4 = 0.13f;

        float nx = (x + seed) / ((x + seed).ToString().Length * 50);
        float ny = (y + seed) / ((y + seed).ToString().Length * 50);
        float v = (v1 * Mathf.PerlinNoise(1 * nx, 1 * ny)
                 + v2 * Mathf.PerlinNoise(2 * nx, 2 * ny)
                 + v3 * Mathf.PerlinNoise(4 * nx, 4 * ny)
                 + v4 * Mathf.PerlinNoise(8 * nx, 8 * ny));
        v /= (v1 + v2 + v3 + v4);
        return v;
    }

    public static float MoistureRange(int x, int y, int seed)
    {
        return MoistureRange((float)x, (float)y, seed);
    }

    //Moisture Layer
    public static float MoistureRange(float x, float y, int seed)
    {
        float v1 = 1.00f;
        float v2 = 0.50f;
        float v3 = 0.25f;
        float v4 = 0.13f;

        float nx = (x + seed) / ((x - seed).ToString().Length * 50);
        float ny = (y + seed) / ((y + seed).ToString().Length * 50);
        float v = (v1 * Mathf.PerlinNoise(1 * nx, 1 * ny)
                 + v2 * Mathf.PerlinNoise(2 * nx, 2 * ny)
                 + v3 * Mathf.PerlinNoise(4 * nx, 4 * ny)
                 + v4 * Mathf.PerlinNoise(8 * nx, 8 * ny));
        v /= (v1 + v2 + v3 + v4);
        return v;
    }

    public static float ElevationRange(int x, int y, int seed)
    {
        return ElevationRange((float)x, (float)y, seed);
    }

    //Elevation Layer
    public static float ElevationRange(float x, float y, int seed)
    {
        float v1 = 1.00f;
        float v2 = 0.75f;
        float v3 = 0.33f;
        float v4 = 0.33f;

        float nx = (x + seed) / ((x + seed).ToString().Length * 50);
        float ny = (y + seed) / ((y - seed).ToString().Length * 50);
        float v = (v1 * Mathf.PerlinNoise(1 * nx, 1 * ny)
                 + v2 * Mathf.PerlinNoise(2 * nx, 2 * ny)
                 + v3 * Mathf.PerlinNoise(4 * nx, 4 * ny)
                 + v4 * Mathf.PerlinNoise(8 * nx, 8 * ny));
        v /= (v1 + v2 + v3 + v4);
        return v;
    }

    public static float SalinityRange(int x, int y, int seed)
    {
        return SalinityRange((float)x, (float)y, seed);
    }


    //Salinity Layer
    public static float SalinityRange(float x, float y, int seed)
    {
        float v1 = 1.00f;
        float v2 = 0.50f;
        float v3 = 0.25f;
        float v4 = 0.13f;

        float nx = (x + seed) / ((x - seed).ToString().Length * 50);
        float ny = (y + seed) / ((y - seed).ToString().Length * 50);
        float v = (v1 * Mathf.PerlinNoise(1 * nx, 1 * ny)
                 + v2 * Mathf.PerlinNoise(2 * nx, 2 * ny)
                 + v3 * Mathf.PerlinNoise(4 * nx, 4 * ny)
                 + v4 * Mathf.PerlinNoise(8 * nx, 8 * ny));
        v /= (v1 + v2 + v3 + v4);
        return v;
    }


    /*
     * 
     * Here determines what Biome is called for what value. Perlin Noise returns a value
     * between 0 and 1. If we want to see more or less of a certain biome, then we adjust
     * the range here. Note that adjusting the algorithms put against the layers will
     * also affect biome size and shape.
     * 
     * 0  - Water
     * 1  - Beach
     * 2  - Tropical Forest
     * 3  - Temperate Forest
     * 4  - Boreal Forest
     * 5  - Frozen Forest
     * 6  - Ice
     * 7  - Grassland
     * 8  - Snow
     * 9 - Tundra
     * 10 - Dessert
     * 11 - Error
     */

    public static int GetBiome(float x, float y, int seed)
    {
        float t = TemperatureRange(x, y, seed);
        float e = ElevationRange(x, y, seed);
        float m = MoistureRange(x, y, seed);
        //float s = SalinityRange(x, y, seed);

        //Water
        if (e < 0.4)
            return 0;
        //Beach
        if (e < 0.41    )
            return 1;
        //Frozen
        if (t > 0.90)
        {
            //Frozen Forest
            if (m > 0.50) return 5;
            //Ice
            if (m > 0.00) return 6;
        }
        //Boreal
        if (t > 0.60)
        {
            //Boreal Forest
            if (m > 0.55) return 4;
            //Snow
            if (m > 0.30) return 8;
            //Tundra
            if (m > 0.00) return 9;
        }
        //Temperate
        if (t > 0.30)
        {
            //Temperate Forest
            if (m > 0.55) return 3;
            //Grassland
            if (m > 0.30) return 7;
            //Dessert
            if (m > 0.00) return 10;
        }
        //Tropical
        //Tropical Forest
        if (m > 0.60) return 2;
        //Grassland
        if (m > 0.40) return 7;
        //Dessert
        if (m > 0.00) return 10;

        //Error
        return 11;
    }
}