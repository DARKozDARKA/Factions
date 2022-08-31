using System;
using CodeBase.TerrainGenerator;

public interface ITerrainGenerator
{
    void GenerateTerrain();
    TerrainMap GetTerrainMap();
    Action OnLoaded { get; set; }
}
