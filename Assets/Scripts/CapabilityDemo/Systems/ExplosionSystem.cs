using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(CapabilitySystemGroup))]
    public partial struct ExplosionSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new ExplosionJob { DeltaTime = deltaTime }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct ExplosionJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(ref LocalTransform transform, ref ExplosionData explosionData)
        {
            explosionData.Timer += DeltaTime;
            var t = EaseOutCubic(explosionData.Timer, explosionData.Duration);
            var scale = math.lerp(0.1f, explosionData.MaxRadius, t);
            transform.Scale = scale;
        }

        [BurstCompile]
        private static float EaseOutCubic(float time, float duration)
        {
            time /= duration;
            time--;
            return time * time * time + 1;
        }
    }
}