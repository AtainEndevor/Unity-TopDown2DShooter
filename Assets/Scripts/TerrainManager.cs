using UnityEngine;

public class TerrainManager : MonoBehaviour {

    /*
     * This handles the map generation as the player moves about.
     */

    public int BufferX = 25;
    public int BufferY = 25;
    public int Seed = 1;
    public Transform Player;
    public float MaxDistanceFromCenter = 7;
    public BiomeType[] BiomeTypes;

    private SpriteRenderer[,] _renderers;

    public TerrainType SelectTerrain(int x, int y)
    {
        //Pulling what terrain tile to generate by determining what biome x,y is in and then selecting
        //a random tile from the provided pack. Uses a seeded random value so it can always be recalled.
        return BiomeTypes[RandomHelper.GetBiome(x,y,Seed)].getTerrain(x,y,Seed);
    }

    //Function called once the player reaches a certain point away from the center of the map.
    void RedrawMap()
    {
        transform.position = new Vector3((int)Player.position.x, (int)Player.position.y, Player.position.z);
        for (int x = 0; x < BufferX; x++) {
            for (int y = 0; y < BufferY; y++) {
                var spriteRenderer = _renderers[x,y];
                var terrain = SelectTerrain(
                    (int)transform.position.x + x,
                    (int)transform.position.y + y);
                spriteRenderer.sprite = terrain.getTileSprite();
                var animator = spriteRenderer.gameObject.GetComponent<Animator>();

                if (terrain.IsAnimated)
                {
                    if (animator == null)
                    {
                        animator = spriteRenderer.gameObject.AddComponent<Animator>();
                        animator.runtimeAnimatorController = terrain.AnimationController;
                    }
                }
                else
                {
                    if (animator != null)
                    {
                        GameObject.Destroy(animator);
                    }
                }
            }
        }
    }

	// Use this for initialization. Inital map draw
	void Start () {
        int sortIndex = 0;
        var offset = new Vector3(0 - BufferX / 2, 0 - BufferY / 2, 0);
        _renderers = new SpriteRenderer[BufferX, BufferY];
        for(int x = 0; x < BufferX; x++) {
            for (int y = 0; y < BufferY; y++) {
                var tile = new GameObject();
                tile.transform.position = new Vector3(x, y, 0) + offset;
                var renderer = _renderers[x,y] = tile.AddComponent<SpriteRenderer>();
                renderer.sortingOrder = sortIndex--;
                tile.name = "Terrain " + tile.transform.position;
                tile.transform.parent = transform;
            }
        }
        RedrawMap();
	}
	
	// Update is called once per frame
	void Update () {
        //Check to see if the map needs to be redrawn.
        if (MaxDistanceFromCenter < Vector3.Distance(Player.position, transform.position))
            RedrawMap();


        //Debug. To be removed.
        Debug.Log("Temperature: " + 
                  RandomHelper.TemperatureRange(Player.transform.position.x, Player.transform.position.y, Seed) +
                  " Moisture:" +
                  RandomHelper.MoistureRange(Player.transform.position.x, Player.transform.position.y, Seed) +
                  " Elevation: " +
                  RandomHelper.ElevationRange(Player.transform.position.x, Player.transform.position.y, Seed));
	}
}
