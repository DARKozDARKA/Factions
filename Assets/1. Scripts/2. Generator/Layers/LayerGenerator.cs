using System;
using System.Linq;
using CodeBase.Infastructure;
using CodeBase.TerrainGenerator;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public abstract class LayerGenerator
{
    protected TerrainMap _map;
    protected INatureGameFactory _natureFactory;

    public abstract void GenerateLayer(TerrainMap map, INatureGameFactory natureFactory);

    protected Vector3? SetRandomPosition(Vector2Int size, StructureType structureType = StructureType.Null)
    {
        Vector2Int position = new Vector2Int(Random.Range(0, _map.mapSize.x * _map.chunkSize.x),
            Random.Range(0, _map.mapSize.y * _map.chunkSize.y));
        var fixedPosition = new Vector2Int(position.x - size.x / 2, position.y - size.y / 2);

        if (!_map.CheckCleanRect(fixedPosition, size))
            return null;

        Vector3 objectWorldPosition = new Vector3(position.x, _map.GetSurfaceBlock(fixedPosition).surfaceHeight,
            position.y);
        _map.OccupieRect(fixedPosition, size, structureType);

        return objectWorldPosition;
    }

    protected void CalculateNatureChance<T>(T[] data) where T : NatureData
    {
        float sum = data.Sum(_ => _.Rarity);
        float currentStartRange = 0f;

        foreach (var item in data)
        {
            float rangeLenght = item.Rarity / sum;

            item.Range.x = currentStartRange;
            item.Range.y = currentStartRange + rangeLenght;

            currentStartRange += rangeLenght;
        }
    }

    protected T GetRandomNature<T>(T[] data, float chance) where T : NatureData =>
        data
            .Where(_ => _.Range.x <= chance)
            .First(_ => _.Range.y >= chance);
}
