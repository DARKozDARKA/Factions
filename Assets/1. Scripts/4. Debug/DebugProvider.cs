using CodeBase.Infastructure;
using UnityEngine;
using Zenject;

public class DebugProvider : IService
{
    public GizmozDraw GizmozDrawer;
    
    public DebugProvider(IPrefabGameFactory gameFactory)
    {
        GizmozDrawer = gameFactory.CreateDebugObject().GetComponent<GizmozDraw>();
    }
}