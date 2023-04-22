using Unity.Entities;

namespace TMG.GDG23
{
    public struct HopProperties : IComponentData
    {
        public float MaxHeight;
        public float JumpMultiplier;
        public float ChargeTime;
    }
}