using System.Threading.Tasks;
using CodeBase.Infastructure;
using CodeBase.TerrainGenerator;
using UnityEngine;

public class EntityGameFactory
{
    private readonly IAssetProvider _assetProvider;
    private readonly MapProvider _mapProvider;

    public EntityGameFactory(IAssetProvider assetProvider, MapProvider mapProvider)
    {
        _assetProvider = assetProvider;
        _mapProvider = mapProvider;
    }

    public void CreateRobot()
    {
        
    }

    public void Cleanup()
    {
        _assetProvider.Cleanup();
    }
}