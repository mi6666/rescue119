using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Logic.InGame.Player
{
    public class LocomotionLogic : ILocomotionLogic
    {
        public LocomotionLogic
        (
            ILocomotionModel locomotionModel
        )
        {
            LocomotionModel = locomotionModel;
        }

        private const float Threshold = 0.01f;

        public Vector2 CalcVelocity(LocomotionArgument argument)
        {
            var moveTo = math.normalizesafe(argument.CurrentVelocity);
            var speed = CalcSpeed(argument);

            if (math.lengthsq(moveTo) < Threshold | math.lengthsq(speed) < Threshold)
            {
                moveTo = argument.MoveInput;
            }

            var midSpeed = moveTo * speed;
            var result = PostProcess(midSpeed, LocomotionModel.WallFriction, argument);

            DebugLogger.Log("speed", speed.ToString("F1"));
            DebugLogger.Log("move to", moveTo.ToString());
            DebugLogger.Log("mid speed", midSpeed.ToString());

            return result;
        }

        private float CalcSpeed(LocomotionArgument argument)
        {
            var moveInput = argument.MoveInput;
            var currentVelocity = argument.CurrentVelocity;
            var deltaTime = argument.DeltaTime;
            var angle = GetAngle(moveInput, currentVelocity);

            var hasInput = math.lengthsq(moveInput) > Threshold; // 入力はあるか
            var isMoving = math.lengthsq(currentVelocity) > Threshold; // 移動中か
            var inputIsReverse = angle > LocomotionModel.ReverseAngleThreshold; // 入力は反転か
            var isReverse = isMoving & inputIsReverse;
            var deceleration = !hasInput | isReverse;
            DebugLogger.Log("deceleration", deceleration.ToString());

            if (deceleration)
            {
                LocomotionModel.DecreaseTime(deltaTime);
                var result = GetSpeed();
                return result;
            }

            LocomotionModel.IncreaseTime(deltaTime);
            return GetSpeed();
        }


        [BurstCompile]
        private static float2 PostProcess(float2 moveTo, float wallFriction, LocomotionArgument argument)
        {
            if (argument.FrontObjects.Length == 0)
            {
                return moveTo;
            }

            var frontObject = argument.FrontObjects[0];
            float2 normal = frontObject.normal;

            // 壁に向かっている場合のみ、壁に沿って滑らせる
            var dotProduct = math.dot(moveTo, normal);
            if (dotProduct >= 0)
            {
                return moveTo;
            }

            // 壁方向の速度成分を打ち消す
            var result = moveTo - dotProduct * normal;
            return result * wallFriction;
        }

        private float GetSpeed()
        {
            var accelTime = LocomotionModel.AccelerationTime;
            var duration = LocomotionModel.AccelerationDuration;
            var maxSpeed = LocomotionModel.MaxSpeed;

            var currentSpeed = LocomotionModel.GetSpeedCurve(accelTime / duration);
            DebugLogger.Log("curve result", currentSpeed.ToString("F1"));
            DebugLogger.Log("accel time", accelTime.ToString("F1"));

            return maxSpeed * currentSpeed;
        }

        [BurstCompile]
        private static float GetAngle(float2 moveInput, float2 currentVelocity)
        {
            return math.degrees(math.acos(math.dot(math.normalize(moveInput), math.normalize(currentVelocity))));
        }

        private ILocomotionModel LocomotionModel { get; }
    }
}