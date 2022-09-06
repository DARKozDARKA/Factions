using UnityEngine.AI;
using CodeBase.Infastructure;

public class NavMeshSurfaceBaker
{
    private const string ObjectName = "NavMeshSurface";

    public void GenerateNavMesh(IPrefabGameFactory factory) =>
        factory.CreateEmpty()
            .AddComponent<NavMeshSurface>()
            .With(_ => _.useGeometry = NavMeshCollectGeometry.PhysicsColliders)
            .With(_ => _.BuildNavMesh())
            .With(_ => _.gameObject.name = ObjectName);
}
