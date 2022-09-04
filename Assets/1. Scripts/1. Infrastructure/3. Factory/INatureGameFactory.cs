using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infastructure
{
    public interface INatureGameFactory
    {
        Task<GameObject> CreateNature(NatureData data, Vector3 at);
    }
}