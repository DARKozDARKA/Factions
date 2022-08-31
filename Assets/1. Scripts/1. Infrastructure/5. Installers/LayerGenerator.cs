using CodeBase.Infastructure;
using CodeBase.TerrainGenerator;

public class LayersGenerator : ILayersGenerator
{
    private readonly MapProvider _mapProvider;
    private readonly INatureGameFactory _natureGameFactory;

    public LayersGenerator(MapProvider mapProvider, INatureGameFactory natureGameFactory)
    {
        _mapProvider = mapProvider;
        _natureGameFactory = natureGameFactory;
    }

    public void GenerateLayers()
    {
        TerrainMap map = _mapProvider.map;
        
        GenerateTreeLayer(map);
        GenerateStoneLayer(map);
    }

    private void GenerateTreeLayer(TerrainMap map)
    {
        var treeGenerator = new TreeLayerGenerator(map, _natureGameFactory);
        treeGenerator.GenerateLayer();
    }
    
    private void GenerateStoneLayer(TerrainMap map)
    {
        //var treeGenerator = new Tree(map, _natureGameFactory);
        //treeGenerator.GenerateLayer();
    }
}