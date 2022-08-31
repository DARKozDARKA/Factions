using CodeBase.TerrainGenerator;

namespace CodeBase.Infastructure
{
    public interface IStaticDataService
    {
        void Load();
        BlockTextureDataContainer GetTextureContainer();
        TerrainGeneratorParametersData GetTerrainGeneratorParametersData();
    }
}