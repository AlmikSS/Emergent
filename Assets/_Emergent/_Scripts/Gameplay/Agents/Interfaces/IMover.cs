using UnityEngine;

namespace Emergent.Gameplay.Agents.Interfaces
{
    public interface IMover
    {
        bool IsBlocked { get; }
        bool CanReachToDestination(Vector3 destination);
        void MoveToDestination(Vector3 destination);
        void Block();
        void Unblock();
        void SetSpeedMultiplier(float speedMultiplier, float time = -1f);
    }
}