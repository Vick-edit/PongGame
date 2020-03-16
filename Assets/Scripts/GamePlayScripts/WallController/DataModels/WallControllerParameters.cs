using System;
using GamePlayScripts.PlayerBallController;
using UnityEngine;

namespace GamePlayScripts.WallController.DataModels
{
    /// <summary>
    ///     Класс с параметрами которые задаются в <see cref="WallController"/> и прокидываются в <see cref="IPlayerBallController"/>
    ///     И т.к. это ссылочный тип, то мы его можем настраивать в эдиторе, и наш чистый шарповый класс будет иметь всегда свежие данные
    ///     Что-то наподобии модели в концепции MVP/MVC
    /// </summary>
    [Serializable]
    public class WallControllerParameters
    {
        /// <summary> Эффект удара по стене </summary>
        public ParticleSystem HitEffect;

        /// <summary> Звук удара по стене </summary>
        public AudioSource HitSound;

        /// <summary> Минимально допустимый угол отскока шарика от стены по отношению к нормали стены </summary>
        [Range(0f, 90f)]
        public float MinAllowedReflectionToNormal = 4f;

        /// <summary> Угол, который будет добавлен к углу отражения шарика, если он был слишком маленький относительно нормали </summary>
        [Range(0f, 20f)]
        public float AdditionalAngel = 8f;
    }
}