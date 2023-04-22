using Unity.Burst;
using Unity.Entities;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(CapabilitySystemGroup), OrderLast = true)]
    public partial struct ResetHitBufferSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new ResetHitBufferJob().Schedule();
        }
    }

    [BurstCompile]
    public partial struct ResetHitBufferJob : IJobEntity
    {
        [BurstCompile]
        private void Execute(ref DynamicBuffer<CapabilityHitBufferElement> hitBuffer)
        {
            for (var i = 0; i < hitBuffer.Length; i++)
            {
                var hit = hitBuffer[i];
                if (hit.IsHandled) continue;
                hit.IsHandled = true;
                hitBuffer[i] = hit;
            }
        }
    }
}