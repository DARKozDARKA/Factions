using System;
using System.Collections.Generic;
using CodeBase.TerrainGenerator;
using UnityEngine;

public class GizmozDraw : MonoBehaviour
{
    public bool activate = false;
    private TerrainMap _map;
    private MapProvider _provider;
    private bool _generated = false;
    private List<GizmozBlock> _blocks = new List<GizmozBlock>();

    public void Construct(MapProvider provider)
    {
        _provider = provider;
        DontDestroyOnLoad(gameObject);
    }

    public void SetMap()
    {
        _map = _provider.map;
        if (_map == null)
            throw new NullReferenceException($"{_map} provider doesn't have map");
    }

    private void OnDrawGizmos()
    {
        if (_map == null)
            return;

        if (activate == false)
            return;

        if (_generated)
            DrawGizmoz();
        else
            GenerateGizmoz();
    }

    private void DrawGizmoz()
    {
        foreach (var item in _blocks)
        {
            Gizmos.color = item.color;
            Gizmos.DrawCube(item.center, Vector3.one);
        }
    }

    private void GenerateGizmoz()
    {
        var chunks = _map.chunks;

        for (int i = 0; i < _map.mapSize.x * _map.chunkSize.x; i++)
        {
            for (int k = 0; k < _map.mapSize.y * _map.chunkSize.y; k++)
            {
                var block = _map.GetSurfaceBlock(new Vector2Int(i, k));
                GizmozBlock gizmozBlock;
                if (block.isOccupied)
                {
                    gizmozBlock = new GizmozBlock()
                        .With(_ => _.center = new Vector3(i, block.surfaceHeight + 1, k))
                        .With(_ => _.color = Color.red);
                }
                else
                {
                    gizmozBlock = new GizmozBlock()
                        .With(_ => _.center = new Vector3(i, block.surfaceHeight + 1, k))
                        .With(_ => _.color = Color.green);
                }

                _blocks.Add(gizmozBlock);
            }
        }

        _generated = true;
    }

    public class GizmozBlock
    {
        public Vector3 center;
        public Color color;
    }
}