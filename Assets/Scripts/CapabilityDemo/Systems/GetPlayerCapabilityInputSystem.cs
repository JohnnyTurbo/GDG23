using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    [UpdateAfter(typeof(GetPlayerMoveInputSystem))]
    public partial struct GetPlayerCapabilityInputSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var currentCapabilityInput = GetCapabilityInput();

            foreach (var capabilityInput in SystemAPI.Query<RefRW<PlayerCapabilityInput>>())
            {
                capabilityInput.ValueRW.Previous = capabilityInput.ValueRO.Current;
                capabilityInput.ValueRW.Current = currentCapabilityInput;
            }
        }
        
        private bool GetCapabilityInput()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }
}