using CodeBase.TerrainGenerator;
using UnityEngine;
using CodeBase.Infastructure;
using Random = UnityEngine.Random;

public class TreeLayerGenerator : LayerGenerator
{
    private const int TreeAmount = 500;
    private TreeData[] _trees;

    public TreeLayerGenerator(TerrainMap map, INatureGameFactory natureFactory)
    {
        _map = map;
        _natureFactory = natureFactory;
        _trees = Resources.LoadAll<TreeData>(AssetsPath.TreesPath);
        CalculateNatureChance<TreeData>(_trees);
    }
    
    public override void GenerateLayer()
    {
        for (int i = 0; i <= TreeAmount; i++)
        {
            TreeData treeData = GetRandomNature<TreeData>(_trees, Random.Range(0, 1f));
            Vector3? objectWorldPosition = SetRandomPosition(new Vector2Int(treeData.Size.x, treeData.Size.z));
            if (objectWorldPosition == null)
                continue;
            
            _natureFactory.CreateNature(treeData, objectWorldPosition.Value);
        }
    }
    
    


}
