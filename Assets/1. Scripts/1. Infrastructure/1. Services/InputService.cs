using UnityEngine;

public class InputService : IInputService
{
    private const string Horizontal = "Horizontal";

    private const string Vertical = "Vertical";

    Vector2 IInputService.Axis => new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));

    bool IInputService.IsFireUp()
    {
        return Input.GetKeyUp(KeyCode.X);
    }

    bool IInputService.IsJumpDown()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }

    bool IInputService.IsJumpUp()
    {
        return Input.GetKeyUp(KeyCode.Z);
    }
}