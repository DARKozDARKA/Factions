using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infastructure
{
    public interface IPrefabGameFactory : IService
    {
        GameObject CreateEmpty(Vector3? at = null, Transform parent = null);
        GameObject CreateHero(GameObject at);
        Task<GameObject> CreateChunk(Vector3 at, Transform parent = null);
        Task<GameObject> CreateDebugObject(Transform parent = null);
        Task WarmUp();
        void Cleanup();
    }
}