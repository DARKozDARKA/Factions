using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infastructure
{
    public interface IAssetProvider : IService
    {
        Task<GameObject> Instantiate(string path, Vector3 at);
        Task<GameObject> Instantiate(string path);
        Task<T> Load<T>(AssetReference monsterDataPrefabReference) where T : class;
        void Cleanup();
        Task<T> Load<T>(string address) where T : class;
        void Initialize();
        Task<GameObject> Instantiate(string address, Transform under);
    }
}