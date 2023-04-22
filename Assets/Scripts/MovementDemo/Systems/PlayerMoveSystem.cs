using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(MovementSystemGroup), OrderFirst = true)]
    public partial struct PlayerMoveSystem : ISystem, ISystemStartStop
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new PlayerMoveJob
            {
                DeltaTime = deltaTime
            }.Schedule();
        }

        #region UI

        public void OnStartRunning(ref SystemState state)
        {
            MovementOptionsUIController.Instance.SetMoveButtonColor(true);
        }

        public void OnStopRunning(ref SystemState state)
        {
            MovementOptionsUIController.Instance.SetMoveButtonColor(false);
        }

        #endregion
    }

    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, in PlayerMoveInput input, in MoveSpeed moveSpeed)
        {
            transform.Position += input.Current * moveSpeed.Value * DeltaTime;
            transform.Rotation = quaternion.LookRotation(input.LastTrueInput, math.up());
        }
    }
}