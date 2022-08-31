using System.Collections;
using System.Collections.Generic;
using CodeBase.Infastructure;
using UnityEngine;
using Zenject;

public class CoroutineInstaller : MonoInstaller
{
    [SerializeField] private CoroutineRunner _runner;
    public override void InstallBindings()
    {
        Container.Bind<ICoroutineRunner>().FromInstance(_runner).AsSingle().NonLazy();
    }
}
