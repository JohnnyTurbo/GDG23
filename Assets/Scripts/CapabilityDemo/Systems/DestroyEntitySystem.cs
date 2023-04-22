using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace TMG.GDG23
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct DestroyEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            new DestroyEntityJob { ECB = ecb }.Schedule();
            new SpawnOnDestroyJob { ECB = ecb }.Schedule();
        }
    }

    [BurstCompile]
    [WithAll(typeof(DestroyEntityTag))]
    public partial struct DestroyEntityJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        
        [BurstCompile]
        private void Execute(Entity entityToDestroy)
        {
            ECB.DestroyEntity(entityToDestroy);
        }
    }
    
    [BurstCompile]
    [WithAll(typeof(DestroyEntityTag))]
    public partial struct SpawnOnDestroyJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        
        [BurstCompile]
        private void Execute(in SpawnOnDestroy spawnOnDestroy, in LocalTransform transform)
        {
            var spawnedEntity = ECB.Instantiate(spawnOnDestroy.Value);
            var spawnedEntityTransform = LocalTransform.FromPositionRotationScale(transform.Position, transform.Rotation, 0.1f);
            ECB.SetComponent(spawnedEntity, spawnedEntityTransform);
        }
    }
}