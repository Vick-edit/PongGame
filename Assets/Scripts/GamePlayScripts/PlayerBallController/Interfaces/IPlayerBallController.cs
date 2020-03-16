using UnityEngine;

namespace GamePlayScripts.PlayerBallController
{
    /// <summary>
    ///     Интерфейс логики контроллера шарика
    /// </summary>
    public interface IPlayerBallController
    {
        /// <summary> Начать движение шарика в заданном направлении </summary>
        /// <param name="movementDirection">Нормализованный вектор направления движения</param>
        void StartMovementInDirection(Vector2 movementDirection);

        /// <summary> Остановить движение шара </summary>
        void PauseBall();

        /// <summary> Изменить скалярную величину движения шарика, если новая скорость попадает в допустим интервал </summary>
        /// <param name="newSpeed">Новое значение скорости</param>
        void ChangeSpeed(float newSpeed);

        /// <summary> Изменить радиус шарика, если новый радус попадает в допустим интервал </summary>
        /// <param name="newScale">Новое значение размера шарика</param>
        void ChangeBallSize(float newScale);

        /// <summary> Изменить цвет мячика </summary>
        /// <param name="newColor">Новое значение цвета шарика</param>
        void ChangeBallColor(Color newColor);
    }
}