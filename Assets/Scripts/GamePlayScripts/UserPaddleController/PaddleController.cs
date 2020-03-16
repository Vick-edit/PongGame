using System;
using GameEventParams;
using GamePlayScripts.UserPaddleController.DataModels;
using UnityEngine;
using Utils.Dispatcher;
using Utils.EffectPool;

namespace GamePlayScripts.UserPaddleController
{
    public class PaddleController : IPaddleController
    {
        private readonly PaddleControllerParameters _paddleParameters;
        private readonly IDispatcher _dispatcher;
        private readonly IEffectPool _effectPool;

        /// <inheritdoc />
        public PaddleController(PaddleControllerParameters paddleParameters, IDispatcher dispatcher, IEffectPool effectPool)
        {
            _paddleParameters = paddleParameters;
            _dispatcher = dispatcher;
            _effectPool = effectPool;
        }


        /// <inheritdoc />
        public Vector2 MovePaddleHorizontal(Vector2 newPosition)
        {
            var paddleHalfSize = _paddleParameters.PaddleSprite.size.x/2f;

            var minPossibleXCoordinate = _paddleParameters.LeftBorderXCoordinate + paddleHalfSize;
            newPosition.x = Mathf.Max(newPosition.x, minPossibleXCoordinate);

            var maxPossibleXCoordinate = _paddleParameters.RightBorderXCoordinate - paddleHalfSize;
            newPosition.x = Mathf.Min(newPosition.x, maxPossibleXCoordinate);

            _paddleParameters.PaddleTransform.position = newPosition;
            _dispatcher.Rise(this, new OnPaddleMovedEventParams(newPosition.x));
            return newPosition;
        }

        /// <inheritdoc />
        public void OnPaddleHit(ContactPoint2D ballHitContact)
        {
            var ballRigidBody = ballHitContact.rigidbody;
            if (ballRigidBody == null)
                throw new Exception($"Предполагается, что в игре у шарика обязательно должен быть {nameof(Rigidbody2D)}");

            PlayPaddleEffect(ballHitContact, ballRigidBody);
            AddBallMovementOffset(ballHitContact, ballRigidBody);

            _dispatcher.Rise(this, GameManagementEvent.OnPaddleHit());
        }

        private void PlayPaddleEffect(ContactPoint2D ballHitContact, Rigidbody2D ballRigidBody)
        {
            var parentForEffect = _paddleParameters.PaddleTransform.gameObject;
            var effectPosition = parentForEffect.transform.InverseTransformPoint(ballHitContact.point);

            var effectSpawnDirection = ballRigidBody.velocity.normalized; //Визуально лучше всего выглядело, когда искры летели вслед за шариком
            var localNormalDirection = parentForEffect.transform.InverseTransformDirection(effectSpawnDirection);
            var effectRotation = Quaternion.LookRotation(localNormalDirection);
            
            _effectPool.AddEffectRequest(_paddleParameters.PaddleHitEffect, parentForEffect, effectPosition, effectRotation);
            _paddleParameters.HitSound.time = 0f;
            _paddleParameters.HitSound.Play();
        }

        private void AddBallMovementOffset(ContactPoint2D ballHitContact, Rigidbody2D ballRigidBody)
        {
            var paddleTransform = _paddleParameters.PaddleTransform;
            var vectorFromPaddleToHitPoint = ballHitContact.point - (Vector2) paddleTransform.position;
            var localHitPositionProjection = Vector3.Project(vectorFromPaddleToHitPoint, Vector3.right);
            var offsetDirection = vectorFromPaddleToHitPoint.x < 0 ? Vector2.left : Vector2.right;

            var hitDistance = localHitPositionProjection.magnitude;
            var offsetPower = 0f;
            if (hitDistance > _paddleParameters.OnPlayerHitDeadZoneInCenter)
            {
                //расчёты этих величин не слишком точные, но учитывая размеры бъекта и назначение этих величин, я посчитал, что такая точность меня устраивает
                var paddleBounds = _paddleParameters.PaddleSprite.bounds;
                var extendSize = paddleBounds.extents.magnitude;
                offsetPower = Mathf.Clamp(Mathf.Abs(hitDistance / extendSize), 0, 1);
            }

            var offset = _paddleParameters.OnPlayerHitMaxOffset * offsetPower * offsetDirection;
            var oldVelocity = ballRigidBody.velocity;
            var velocityMagnitude = oldVelocity.magnitude;
            var velocityWithOffset = (oldVelocity + offset).normalized * velocityMagnitude;
            ballRigidBody.velocity = velocityWithOffset;
        }
    }
}