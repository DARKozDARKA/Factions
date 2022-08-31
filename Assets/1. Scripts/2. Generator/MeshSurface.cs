using UnityEngine.AI;
using CodeBase.Infastructure;

public class NavMeshSurfaceBaker
{
    private const string ObjectName = "NavMeshSurface";

    public void GenerateNavMesh(IPrefabGameFactory factory)
    {
        factory.CreateEmpty()
            .With(_ => _.AddComponent<NavMeshSurface>().BuildNavMesh())
            .With(_ => _.name = ObjectName);
    }
}
