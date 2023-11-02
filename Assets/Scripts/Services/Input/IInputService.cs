using UnityEngine;

namespace Playground.Services.Input
{
    public interface IInputService
    {
        #region Properties

        Vector2 Axis { get; }
        bool IsJump { get; }

        #endregion
    }
}