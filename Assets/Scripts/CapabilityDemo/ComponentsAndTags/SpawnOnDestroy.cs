using Unity.Entities;

namespace TMG.GDG23
{
    public struct SpawnOnDestroy : IComponentData
    {
        public Entity Value;
    }
}