using Unity.Entities;
using Unity.Mathematics;

namespace TMG.GDG23
{
    [InternalBufferCapacity(1)]
    public struct CapabilityHitBufferElement : IBufferElementData
    {
        public Entity HitEntity;
        public float3 HitPosition;
        public float3 HitNormal;
        public bool IsHandled;
    }
}