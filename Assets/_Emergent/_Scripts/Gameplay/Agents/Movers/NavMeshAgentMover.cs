using System.Threading;
using Cysharp.Threading.Tasks;
using Emergent.Gameplay.Agents.Interfaces;
using KofeyekToolkit.Core.LifeCycle.Core.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

namespace Emergent.Gameplay.Agents.Movers
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class NavMeshAgentMover : MonoBehaviour, IMover, IConstructable
    {
        [SerializeField] private float _moveSpeed;

        private NavMeshAgent _nmAgent;
        private CancellationTokenSource _setSpeedMultiplierCts;
        private float _speedMultiplier = 1f;
        
        public bool IsBlocked { get; private set; }
        
        public void OnConstruct()
        {
            _nmAgent = GetComponent<NavMeshAgent>();
            _nmAgent.speed = _moveSpeed;
        }

        public bool CanReachToDestination(Vector3 destination)
        {
            var path = new NavMeshPath();
            _nmAgent.CalculatePath(destination, path);
            return Vector3.Distance(destination, path.corners[^1]) < 0.1f;
        }

        public void MoveToDestination(Vector3 destination)
        {
            if (_nmAgent.speed <= 0f)
                return;
            
            _nmAgent.ResetPath();
            _nmAgent.SetDestination(destination);
        }
        
        public void SetSpeedMultiplier(float speedMultiplier, float time = -1f)
        {
            _speedMultiplier = speedMultiplier;
            _nmAgent.speed *= _speedMultiplier;

            if (time < 0)
                return;

            _setSpeedMultiplierCts?.Cancel();
            _setSpeedMultiplierCts?.Dispose();
                        
            _setSpeedMultiplierCts = new CancellationTokenSource();
            ResetSpeedMultiplier(time, _setSpeedMultiplierCts.Token).Forget();
        }

        private async UniTaskVoid ResetSpeedMultiplier(float time, CancellationToken token)
        {
            await UniTask.WaitForSeconds(time, cancellationToken: token);
            
            _speedMultiplier = 1f;
            _nmAgent.speed = _moveSpeed;
        }

        public void Block() => IsBlocked = true;
        public void Unblock() => IsBlocked = false;
    }
}