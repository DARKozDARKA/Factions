using System;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace CodeBase.Infastructure
{
    public class NatureGameFactory : INatureGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public NatureGameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async Task<GameObject> CreateNature(NatureData data, Vector3 at)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(data.Prefab);
            return GameObject.Instantiate(prefab, at, CreateRotation(data.RotationType));
        } 
           
        
        private Quaternion CreateRotation(RotationType rotationType)
        {
            return rotationType switch
            {
                RotationType.DoNotRotate => Quaternion.identity,
                RotationType.RandomizeBy90 => Quaternion.Euler(0, Random.Range(0, 4) * 90, 0),
                RotationType.RandomizeFully => Quaternion.Euler(0, Random.Range(0, 360), 0),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}