using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(CapabilitySystemGroup))]
    public partial struct DestroyOnTimerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            new DestroyOnTimerJob
            {
                DeltaTime = deltaTime,
                ECB = ecb
            }.Schedule();

            new FlashOnTimerJob
            {
                ECB = ecb
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct DestroyOnTimerJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute(Entity entity, ref DestroyOnTimer onTimer)
        {
            onTimer.Value -= DeltaTime;
            
            if (onTimer.Value <= 0f)
            {
                ECB.AddComponent<DestroyEntityTag>(entity);
            }
        }
    }

    [BurstCompile]
    public partial struct FlashOnTimerJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        
        [BurstCompile]
        private void Execute(Entity entity, ref FlashOnTimer flashOnTimer, in DestroyOnTimer destroyOnTimer)
        {
            var currentToggleInterval = destroyOnTimer.Value <= flashOnTimer.RapidToggleThreshold
                ? flashOnTimer.RapidToggleInterval
                : flashOnTimer.RegularToggleInterval;
            
            if (destroyOnTimer.Value <= flashOnTimer.NextToggleTime)
            {
                flashOnTimer.IsRed = !flashOnTimer.IsRed;
                var color = flashOnTimer.IsRed ? new float4(1, 0, 0, 1) : new float4(0, 0, 0, 1);
                ECB.SetComponent(entity, new URPMaterialPropertyBaseColor { Value = color });
                flashOnTimer.NextToggleTime = destroyOnTimer.Value - currentToggleInterval;
            }
        }
    }
}