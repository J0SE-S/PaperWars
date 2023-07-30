using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mirror;

public class TilemapManager : NetworkBehaviour {
    public enum TileData : byte {
        STONE,
        GRASS,
        DIRT,
        SAND,
        SHALLOW_WATER,
        WATER,
        DEEP_WATER
    }

    public class ExtendedTileData {}

    public readonly SyncDictionary<Vector3Int, TileData> tiles = new SyncDictionary<Vector3Int, TileData>();

    public Tilemap visualTilemap;
    public Tilemap physicalTilemap;

    public Tile stone;
    public Tile grass;
    public Tile dirt;
    public Tile sand;
    public Tile shallowWater;
    public Tile water;
    public Tile deepWater;

    public override void OnStartServer() {}

    public override void OnStartClient() {
        tiles.Callback += OnTilemapUpdate;

        foreach (KeyValuePair<Vector3Int, TileData> kvp in tiles)
            OnTilemapUpdate(SyncDictionary<Vector3Int, TileData>.Operation.OP_ADD, kvp.Key, kvp.Value);
    }

    public Tile DataToTile(TileData item) {
        switch (item) {
            case TileData.STONE: return stone;
            case TileData.GRASS: return grass;
            case TileData.DIRT: return dirt;
            case TileData.SAND: return sand;
            case TileData.SHALLOW_WATER: return shallowWater;
            case TileData.WATER: return water;
            case TileData.DEEP_WATER: return deepWater;
        }
        return null;
    }

    void OnTilemapUpdate(SyncDictionary<Vector3Int, TileData>.Operation op, Vector3Int key, TileData item) {
        if (item == TileData.GRASS || item == TileData.DIRT || item == TileData.SAND) {
            switch (op) {
                case SyncIDictionary<Vector3Int, TileData>.Operation.OP_ADD:
                    visualTilemap.SetTile(key, DataToTile(item));
                    break;
                case SyncIDictionary<Vector3Int, TileData>.Operation.OP_SET:
                    visualTilemap.SetTile(key, DataToTile(item));
                    break;
                case SyncIDictionary<Vector3Int, TileData>.Operation.OP_REMOVE:
                    break;
                case SyncIDictionary<Vector3Int, TileData>.Operation.OP_CLEAR:
                    visualTilemap.ClearAllTiles();
                    break;
            }
        }
    }
}