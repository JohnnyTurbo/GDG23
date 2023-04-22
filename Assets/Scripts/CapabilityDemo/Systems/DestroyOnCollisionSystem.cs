using Unity.Burst;
using Unity.Entities;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(CapabilitySystemGroup))]
    public partial struct DestroyOnCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            new DestroyOnCollisionJob { ECB = ecb }.Schedule();
        }
    }

    [BurstCompile]
    [WithAll(typeof(DestroyOnCollisionTag))]
    public partial struct DestroyOnCollisionJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        
        [BurstCompile]
        private void Execute(Entity entity, in DynamicBuffer<CapabilityHitBufferElement> hitBuffer)
        {
            foreach (var hit in hitBuffer)
            {
                if (hit.IsHandled) continue;
                ECB.AddComponent<DestroyEntityTag>(entity);
            }
        }
    }
}