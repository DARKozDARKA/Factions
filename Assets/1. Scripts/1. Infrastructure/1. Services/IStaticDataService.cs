using CodeBase.TerrainGenerator;
using UnityEngine;

namespace CodeBase.Infastructure
{
    public interface IStaticDataService
    {
        void Load();
        BlockTextureDataContainer GetTextureContainer();
        TerrainGeneratorParametersData GetTerrainGeneratorParametersData();
        T GetData<T>(string path) where T : ScriptableObject;
    }
}