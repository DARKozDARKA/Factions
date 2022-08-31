using UnityEngine;
using CodeBase.Infastructure;

public interface IInputService : IService
{
    Vector2 Axis { get; }
    bool IsJumpUp();
    bool IsJumpDown();
    bool IsFireUp();
}
