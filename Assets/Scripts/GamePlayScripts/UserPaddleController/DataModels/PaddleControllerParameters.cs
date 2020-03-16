using System;
using Components.GamePlayComponents;
using UnityEngine;

namespace GamePlayScripts.UserPaddleController.DataModels
{
    /// <summary>
    ///     Класс с параметрами которые задаются в <see cref="PlayerPaddleMono"/> и прокидываются в <see cref="IPaddleController"/>
    ///     И т.к. это ссылочный тип, то мы его можем настраивать в эдиторе, и наш чистый шарповый класс будет иметь всегда свежие данные
    ///     Что-то наподобии модели в концепции MVP/MVC
    /// </summary>
    [Serializable]
    public class PaddleControllerParameters
    {
        /// <summary> Трансформ площадки пользователя </summary>
        public Transform PaddleTransform;
        
        /// <summary> Спрайт площадки  </summary>
        public SpriteRenderer PaddleSprite;

        /// <summary> Визуальный эффект удара шарика по площадке </summary>
        public ParticleSystem PaddleHitEffect;

        /// <summary> Звук удара по панельке </summary>
        public AudioSource HitSound;

        /// <summary> Максимальная величина смещения, при ударе шарика о контроллер пользователя </summary>
        public float OnPlayerHitMaxOffset;

        /// <summary> Максимальная величина мёртвой зоны в центре площадки, при ударе шарика о контроллер пользователя </summary>
        public float OnPlayerHitDeadZoneInCenter;


        [SerializeField] private Transform LeftBorder;
        [SerializeField] private Transform RightBorder;

        private float _leftBorderXCoordinate = float.NaN;
        public float LeftBorderXCoordinate
        {
            get
            {
                if (float.IsNaN(_leftBorderXCoordinate))
                    _leftBorderXCoordinate = LeftBorder.position.x;
                return _leftBorderXCoordinate;
            }
        }

        private float _rightBorderXCoordinate = float.NaN;
        public float RightBorderXCoordinate
        {
            get
            {
                if (float.IsNaN(_rightBorderXCoordinate))
                    _rightBorderXCoordinate = RightBorder.position.x;
                return _rightBorderXCoordinate;
            }
        }

        public void CopyPrivateParams(PaddleControllerParameters parameters)
        {
            LeftBorder = parameters.LeftBorder;
            RightBorder = parameters.RightBorder;
            HitSound = parameters.HitSound;
        }
    }
}