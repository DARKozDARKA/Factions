using System.Collections.Generic;
using CodeBase.Infastructure;
using CodeBase.TerrainGenerator;

public class LayersGenerator : ILayersGenerator
{
    private readonly MapProvider _mapProvider;
    private readonly INatureGameFactory _natureGameFactory;
    private readonly List<LayerGeneratorData> _generators;

    public LayersGenerator(MapProvider mapProvider, INatureGameFactory natureGameFactory, IStaticDataService dataService)
    {
        _mapProvider = mapProvider;
        _natureGameFactory = natureGameFactory;
        _generators = dataService.GetData<LayerGeneratorsData>(StaticDataPath.LayerGeneratorsDataPath).LayersGenerators;
    }

    public void GenerateLayers()
    {
        TerrainMap map = _mapProvider.map;

        foreach (var generator in _generators)
            generator.LayerGenerator.GenerateLayer(_mapProvider.map, _natureGameFactory);
        
    }
}