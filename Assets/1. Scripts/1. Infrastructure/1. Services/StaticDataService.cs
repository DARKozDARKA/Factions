using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.TerrainGenerator;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infastructure
{
    public class StaticDataService : IStaticDataService
    {
        private BlockTextureDataContainer _textureDataContainer;
        private TerrainGeneratorParametersData _terrainGeneratorParametersData;

        public StaticDataService()
        {
            Load();
        }

        public void Load()
        {
            _textureDataContainer = Resources
              .Load<BlockTextureDataContainer>(StaticDataPath.BlockTextureDataPath);

            _terrainGeneratorParametersData = Resources
              .Load<TerrainGeneratorParametersData>(StaticDataPath.TerrainGeneratorParametersDataPath);
        }

        public BlockTextureDataContainer GetTextureContainer()
        {
            return _textureDataContainer;
        }

        public TerrainGeneratorParametersData GetTerrainGeneratorParametersData()
        {
            return _terrainGeneratorParametersData;
        }

        public T GetData<T>(string path) where T : ScriptableObject
        {
            return Resources.Load<T>(path);
        }
    }
}


