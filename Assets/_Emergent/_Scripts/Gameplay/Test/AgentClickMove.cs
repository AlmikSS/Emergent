using Emergent.Gameplay.Agents.Movers;
using KofeyekToolkit.Core.TickSystem.Interfaces;
using UnityEngine;

namespace _Emergent._Scripts.Gameplay.Test
{
    public sealed class AgentClickMove : MonoBehaviour, IGameplayTickable
    {
        [SerializeField] private NavMeshAgentMover _mover;
        
        public void Tick(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (_mover.CanReachToDestination(hit.point))
                        _mover.MoveToDestination(hit.point);
                }
            }
        }
    }
}