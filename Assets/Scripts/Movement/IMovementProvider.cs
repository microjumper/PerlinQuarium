using UnityEngine;

namespace Movement
{
    public interface IMovementProvider
    {
        Vector3 ProvideVelocityVector();
    }
}