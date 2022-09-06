using System;
using CodeBase.TerrainGenerator;
using UnityEngine;
using CodeBase.Infastructure;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class BasicLayerGenerator : LayerGenerator
{
    [SerializeField]
    private int NatureAmount = 500;

    [SerializeField]
    private string NaturePath;

    [SerializeField]
    private StructureType _structureType;

    private NatureData[] _natures;

    public override void GenerateLayer(TerrainMap map, INatureGameFactory natureFactory)
    {
        _map = map;
        _natureFactory = natureFactory;
        
        _natures = Resources.LoadAll<NatureData>(NaturePath);
        CalculateNatureChance<NatureData>(_natures);
        
        for (int i = 0; i <= NatureAmount; i++)
        {
            NatureData natureData = GetRandomNature<NatureData>(_natures, Random.Range(0, 1f));
            Vector3? objectWorldPosition = SetRandomPosition(new Vector2Int(natureData.Size.x, natureData.Size.z), _structureType);
            if (objectWorldPosition == null)
                continue;

            _natureFactory.CreateNature(natureData, objectWorldPosition.Value);
        }
    }
}