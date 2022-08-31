using UnityEngine;

namespace CodeBase.Infastructure
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, GameObject at);
        GameObject Instantiate(string path, Vector3 at);
    }
}
