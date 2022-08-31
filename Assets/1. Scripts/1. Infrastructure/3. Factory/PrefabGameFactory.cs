using UnityEngine;

namespace CodeBase.Infastructure
{
    public class PrefabGameFactory : IPrefabGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public PrefabGameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject CreateEmpty(Vector3? at = null, Transform parent = null) =>
            new GameObject()
                .With(_ => _.transform.parent = parent)
                .With(_ => _.transform.position = at.Value, at != null);

        public GameObject CreateHero(GameObject at) => throw new System.NotImplementedException();

        public GameObject CreateChunk(Vector3 at, Transform parent = null) =>
            _assetProvider.Instantiate(AssetsPath.ChunkPath, at)
                .With(_ => _.transform.parent = parent);

        public GameObject CreateDebugObject(Transform parent = null) =>
            _assetProvider.Instantiate(AssetsPath.DebugPath, Vector3.zero)
                .With(_ => _.transform.parent = parent);
    }
}