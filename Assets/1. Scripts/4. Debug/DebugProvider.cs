using System.Threading.Tasks;
using CodeBase.Infastructure;
using CodeBase.TerrainGenerator;
using UnityEngine;
using Zenject;

public class DebugProvider : IService
{
    private readonly IPrefabGameFactory _gameFactory;
    private readonly MapProvider _mapProvider;
    public GizmozDraw GizmozDrawer;
    
    public DebugProvider(IPrefabGameFactory gameFactory, MapProvider mapProvider)
    {
        _gameFactory = gameFactory;
        _mapProvider = mapProvider;
    }

    public async Task<GizmozDraw> CreateDebugObject()
    {
        GameObject newObject = await _gameFactory.CreateDebugObject();
        GizmozDraw gizmozDraw = newObject.GetComponent<GizmozDraw>();
        gizmozDraw.Construct(_mapProvider);
        
        return newObject.GetComponent<GizmozDraw>();
    }
}