using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    [UpdateAfter(typeof(BeginSimulationEntityCommandBufferSystem))]
    [UpdateBefore(typeof(GetPlayerCapabilityInputSystem))]
    public partial struct GetPlayerMoveInputSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var currentMoveInput = GetMoveInput();
            
            foreach (var moveInput in SystemAPI.Query<RefRW<PlayerMoveInput>>())
            {
                moveInput.ValueRW.Previous = moveInput.ValueRO.Current;
                moveInput.ValueRW.Current = currentMoveInput;

                if (math.lengthsq(currentMoveInput) > 0.1f)
                {
                    moveInput.ValueRW.LastTrueInput = currentMoveInput;
                }
            }
        }

        private float3 GetMoveInput()
        {
            var playerInput = float3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                playerInput.z += 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                playerInput.x -= 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                playerInput.z -= 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerInput.x += 1;
            }

            return playerInput.Equals(float3.zero) ? float3.zero : math.normalize(playerInput);
        }
    }
}