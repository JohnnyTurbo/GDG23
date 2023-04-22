using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
    }

    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(playerEntity, new MoveSpeed { Value = authoring.MoveSpeed });
            AddComponent<PlayerMoveInput>(playerEntity);
        }
    }
}