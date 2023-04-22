using Unity.Entities;

namespace TMG.GDG23
{
    public struct ExplosionData : IComponentData
    {
        public float Duration;
        public float MaxRadius;
        public float Timer;
    }
}