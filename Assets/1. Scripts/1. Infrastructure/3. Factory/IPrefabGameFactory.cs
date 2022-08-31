using UnityEngine;

namespace CodeBase.Infastructure
{
    public interface IPrefabGameFactory : IService
    {
        GameObject CreateEmpty(Vector3? at = null, Transform parent = null);
        GameObject CreateHero(GameObject at);
        GameObject CreateChunk(Vector3 at, Transform parent = null);
        GameObject CreateDebugObject(Transform parent = null);
    }
}