using Unity.Burst;
using Unity.Entities;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(CapabilitySystemGroup))]
    public partial struct DamageOnCollisionSystem : ISystem
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

            new DamageOnCollisionJob
            {
                ECB = ecb
            }.Schedule();
        }

        [BurstCompile]
        public partial struct DamageOnCollisionJob : IJobEntity
        {
            public EntityCommandBuffer ECB;
            
            [BurstCompile]
            private void Execute(in DynamicBuffer<CapabilityHitBufferElement> hitBuffer, in DamageOnCollision damageOnCollision)
            {
                foreach (var hit in hitBuffer)
                {
                    if (hit.IsHandled) continue;
                    ECB.AppendToBuffer(hit.HitEntity, new DamageBufferElement { Value = damageOnCollision.Value });
                }
            }
        }
    }
}