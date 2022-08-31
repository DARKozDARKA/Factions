using System;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

namespace CodeBase.Infastructure
{
    public class NatureGameFactory : INatureGameFactory
    {
        public GameObject CreateNature(NatureData data, Vector3 at) => 
            GameObject.Instantiate(data.Prefab, at, CreateRotation(data.RotationType));
        
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