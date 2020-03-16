using System;
using Extensions;
using GamePlayScripts.WallController.DataModels;
using UnityEngine;
using Utils.EffectPool;

namespace GamePlayScripts.WallController
{
    public class WallController : IWallController
    {
        private readonly WallControllerParameters _wallParameters;
        private readonly IEffectPool _effectPool;


        /// <inheritdoc />
        public WallController(WallControllerParameters wallParameters, IEffectPool effectPool)
        {
            _wallParameters = wallParameters;
            _effectPool = effectPool;
        }


        /// <inheritdoc/>
        public void OnWallHit(ContactPoint2D ballHitContact)
        {
            PlayEffect(ballHitContact);
            OffsetBallOnHit(ballHitContact);
        }


        private void PlayEffect(ContactPoint2D ballHitContact)
        {
            var effectPosition = ballHitContact.point;

            var normalVector = ballHitContact.normal * -1f; //Нормаль будет показывать от шарика в стену, поэтому умножаем на -1
            var effectRotation = Quaternion.LookRotation(normalVector);

            _effectPool.AddEffectRequest(_wallParameters.HitEffect, effectPosition, effectRotation);
            _wallParameters.HitSound.time = 0f;
            _wallParameters.HitSound.Play();
        }

        private void OffsetBallOnHit(ContactPoint2D ballHitContact)
        {
            var ballRigidBody = ballHitContact.rigidbody;
            if (ballRigidBody == null)
                throw new Exception($"Предполагается, что в игре у шарика обязательно должен быть {nameof(Rigidbody2D)}");

            var ballReflectedDirection = ballRigidBody.velocity.normalized;
            var normalVector = ballHitContact.normal * -1f; //Нормаль будет показывать от шарика в стену, поэтому умножаем на -1
            var degreeFromSurfaceNormal = Vector2.Angle(normalVector, ballReflectedDirection);
            if(degreeFromSurfaceNormal > _wallParameters.MinAllowedReflectionToNormal)
                return;

            //потому что вращение происходит вокруг Vector3.Forward и как повернуть зависит от того, летит объект вниз и влево или право
            var correctionAngelSign = Mathf.Sign(ballReflectedDirection.y) * Mathf.Sign(ballReflectedDirection.x);
            var correctionAngel = correctionAngelSign * _wallParameters.AdditionalAngel;
            var correctedVelocity = ballRigidBody.velocity.RotateByAngle(correctionAngel);
            ballRigidBody.velocity = correctedVelocity;
        }
    }
}