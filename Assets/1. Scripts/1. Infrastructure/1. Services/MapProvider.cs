using System;
using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    public class MapProvider
    {
        public TerrainMap map => _map;
        private TerrainMap _map;
        public void SetMap(TerrainMap map)
        {
            _map = map;
        }


    }
}


