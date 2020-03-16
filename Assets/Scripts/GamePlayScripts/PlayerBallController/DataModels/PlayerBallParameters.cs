using System;
using Components.GamePlayComponents;
using UnityEngine;

namespace GamePlayScripts.PlayerBallController.DataModels
{
    /// <summary>
    ///     Класс с параметрами которые задаются в <see cref="PlayerBallMono"/> и прокидываются в <see cref="IPlayerBallController"/>
    ///     И т.к. это ссылочный тип, то мы его можем настраивать в эдиторе, и наш чистый шарповый класс будет иметь всегда свежие данные
    ///     Что-то наподобии модели в концепции MVP/MVC
    /// </summary>
    [Serializable]
    public class PlayerBallParameters
    {
        /// <summary> Движется ли шарик, если да, то его нельзя запускать </summary>
        public bool IsRunning { get; set; }

        /// <summary> Трансформ шарика на котором были установлены эти параметры </summary>
        public Transform Transform;

        /// <summary> Rigidbody шарика </summary>
        public SpriteRenderer BallSprite;

        /// <summary> Rigidbody шарика </summary>
        public Rigidbody2D Rigidbody;

        /// <summary> Радиус шарика </summary>
        public float BallRadius;
        public float MinBallRadius;
        public float MaxBallRadius;

        /// <summary> Скорость шарика </summary>
        public float BallSpeedPower;
        public float MinForcePower;
        public float MaxForcePower;
    }
}