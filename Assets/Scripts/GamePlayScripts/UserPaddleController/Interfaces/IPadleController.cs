
using UnityEngine;

namespace GamePlayScripts.UserPaddleController
{
    /// <summary>
    ///     Интерфейс контроллера площадки, которой управляет пользователь
    /// </summary>
    public interface IPaddleController
    {
        /// <summary> Сместить панельку пользователя по горизонтальной оси </summary>
        /// <param name="newPosition">Новая ожидаемая координата панельки</param>
        /// <returns>Позиция в которую установится панелька, с учетом всех ограничений</returns>
        Vector2 MovePaddleHorizontal(Vector2 newPosition);

        /// <summary> Обработка удара шариком по площадке </summary>
        /// <param name="ballHitContact">Сведения о контакте с шариком</param>
        void OnPaddleHit(ContactPoint2D ballHitContact);
    }
}