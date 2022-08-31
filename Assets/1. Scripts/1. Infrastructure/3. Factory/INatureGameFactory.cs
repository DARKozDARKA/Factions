using UnityEngine;

namespace CodeBase.Infastructure
{
    public interface INatureGameFactory
    {
        GameObject CreateNature(NatureData data, Vector3 at);
    }
}