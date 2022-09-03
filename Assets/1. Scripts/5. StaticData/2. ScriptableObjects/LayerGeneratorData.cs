using UnityEngine;

[CreateAssetMenu(fileName = "LayerGeneratorData", menuName = "StaticData/LayerGeneration/LayerGeneratorData")]
public class LayerGeneratorData : ScriptableObject
{
    public LayersType LayerType;
    
    [SerializeReference]
    public LayerGenerator LayerGenerator;
}