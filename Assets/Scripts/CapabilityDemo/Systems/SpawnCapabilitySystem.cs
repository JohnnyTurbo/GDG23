using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(CapabilitySystemGroup), OrderFirst = true)]
    public partial struct SpawnCapabilitySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            
            foreach (var input in SystemAPI.Query<PlayerCapabilityInput>())
            {
                if (input.Begin)
                {
                    new SpawnCapabilityJob { ECB = ecb }.ScheduleParallel();
                }
            }
        }
    }

    [BurstCompile]
    public partial struct SpawnCapabilityJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        
        [BurstCompile]
        private void Execute(in CapabilityPrefab capabilityPrefab, in LocalTransform transform, [ChunkIndexInQuery]int sortKey)
        {
            var newCapability = ECB.Instantiate(sortKey, capabilityPrefab.Value);
            ECB.SetComponent(sortKey, newCapability, LocalTransform.FromPositionRotation(transform.Position, transform.Rotation));
        }
    }
}