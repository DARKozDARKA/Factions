using UnityEngine;
using CodeBase.TerrainGenerator;
using CodeBase.Infastructure;
using Zenject;
using System;

public class TerrainGenerator : ITerrainGenerator
{
    public Action OnLoaded { get; set; }

    private const string ParentObjectName = "TerrainChunks";

    private ChunkObject[,] _chunks;
    private ChunkMeshGenerator _meshGenerator;
    private BlockTextureAtlas _atlas;
    private IPrefabGameFactory _prefabGameFactory;
    private INatureGameFactory _natureFactory;
    private Material _material;
    private NavMeshSurfaceBaker _navMeshBaker;
    private TerrainMap _terrainMap;
    private TerrainGeneratorParametersData _parameters;
    private TreeLayerGenerator _treeLayerGeneratorGenerator;
    private TerrainMap _map;

    private TerrainGenerator(BlockTextureAtlas textureAtlas, IPrefabGameFactory prefabFactory, IStaticDataService staticDataService, INatureGameFactory natureFactory)
    {
        SetParameters(staticDataService);

        _atlas = textureAtlas;
        _prefabGameFactory = prefabFactory;
        _natureFactory = natureFactory;
        _navMeshBaker = new NavMeshSurfaceBaker();
        _meshGenerator = new ChunkMeshGenerator(_atlas, _parameters.BlockSize, _parameters.ChunkSize);


        _material = _atlas.GetMaterial();
    }

    public void GenerateTerrain()
    {
        _chunks = new ChunkObject[_parameters.MapSize.x, _parameters.MapSize.y];

        GenerateChunks();
        GenerateMeshes();

        _navMeshBaker.GenerateNavMesh(_prefabGameFactory);

        _map = new TerrainMap(_chunks, new Vector2Int(_parameters.ChunkSize.x, _parameters.ChunkSize.z), _parameters.MapSize);
        
        OnLoaded?.Invoke();
    }

    public TerrainMap GetTerrainMap()
    {
        return _map;
    }

    private void GenerateChunks()
    {
        for (int x = 0; x < _parameters.MapSize.x; x++)
        {
            for (int z = 0; z < _parameters.MapSize.y; z++)
            {
                ChunkParameters parameters = new ChunkParameters()
                    .With(_ => _.chunkSize = _parameters.ChunkSize)
                    .With(_ => _.mapSize = _parameters.MapSize)
                    .With(_ => _.offset = new Vector2Int(x, z))
                    .With(_ => _.terrainParameters = _parameters.TerrainParameters)
                    .With(_ => _.heightModifier = _parameters.HeightModifier);

                _parameters.ChunkObject = new ChunkObject(parameters);
                _chunks[x, z] = _parameters.ChunkObject;

            }
        }
    }

    private void GenerateMeshes()
    {
        GameObject parentObject = _prefabGameFactory.CreateEmpty()
            .With(_ => _.name = ParentObjectName);

        for (int x = 0; x < _parameters.MapSize.x; x++)
        {
            for (int z = 0; z < _parameters.MapSize.y; z++)
            {
                _parameters.ChunkObject = _chunks[x, z];

                Vector3 position = new Vector3(x * (int)_parameters.ChunkSize.x, 0f, z * (int)_parameters.ChunkSize.z);
                var newObject = _prefabGameFactory.CreateChunk(position, parent: parentObject.transform);

                HandleGameObject(newObject);
            }
        }
    }

    private void HandleGameObject(GameObject newObject)
    {
        Mesh mesh = GetMesh(newObject);
        _meshGenerator.SetChunkMesh(mesh, _parameters.ChunkObject, _chunks);

        SetCollider(newObject, ref mesh);
    }

    private void SetParameters(IStaticDataService staticDataService)
    {
        _parameters = staticDataService.GetTerrainGeneratorParametersData();
    }

    private Mesh GetMesh(GameObject gameObject)
    {
        Mesh mesh;
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();  
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();     

        meshRenderer.material = _material;

        if (Application.isEditor)
        {
            mesh = meshFilter.sharedMesh;
            if (mesh == null)
            {
                meshFilter.sharedMesh = new Mesh();
                mesh = meshFilter.sharedMesh;
            }
        }
        else
        {
            mesh = meshFilter.mesh;
            if (mesh == null)
            {
                meshFilter.mesh = new Mesh();
                mesh = meshFilter.mesh;
            }
        }

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        return mesh;
    }

    private void SetCollider(GameObject newObject, ref Mesh m)
    {
        MeshCollider mc = newObject.GetComponent<MeshCollider>();
        mc.sharedMesh = m;
    }
}
