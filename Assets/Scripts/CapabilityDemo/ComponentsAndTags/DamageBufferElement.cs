using Unity.Entities;

namespace TMG.GDG23
{
    [InternalBufferCapacity(1)]
    public struct DamageBufferElement : IBufferElementData
    {
        public float Value;
    }
}