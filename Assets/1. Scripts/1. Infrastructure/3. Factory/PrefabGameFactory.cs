using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infastructure
{
    public class PrefabGameFactory : IPrefabGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public PrefabGameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;
        
        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(AssetsPath.ChunkPath);
        }

        public GameObject CreateEmpty(Vector3? at = null, Transform parent = null) =>
            new GameObject()
                .With(_ => _.transform.parent = parent)
                .With(_ => _.transform.position = at.Value, at != null);


        public GameObject CreateHero(GameObject at) => throw new System.NotImplementedException();

        public async Task<GameObject> CreateChunk(Vector3 at, Transform parent = null)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetsPath.ChunkPath);
            return Object.Instantiate(prefab)
                .With(_ => _.transform.position = at)
                .With(_ => _.transform.parent = parent);
        }


        public async Task<GameObject> CreateDebugObject(Transform parent = null)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetsPath.DebugPath);
            return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        }


        public void Cleanup()
        {
            _assetProvider.Cleanup();
        }
    }
}