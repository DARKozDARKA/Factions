using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerGeneratorsData", menuName = "StaticData/LayerGeneration/LayerGeneratorsData")]
public class LayerGeneratorsData : ScriptableObject
{
    public List<LayerGeneratorData> LayersGenerators;
}