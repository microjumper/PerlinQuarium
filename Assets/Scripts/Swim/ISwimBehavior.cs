using UnityEngine;

namespace Swim
{
    public interface ISwimBehavior
    {
        void Swim(Transform transform, float swimSpeed);
    }
}