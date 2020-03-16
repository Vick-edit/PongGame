using UnityEngine;

namespace GamePlayScripts.WallController
{
    /// <summary>
    ///    Контроллер со всей логикой работы стенок
    /// </summary>
    public interface IWallController
    {
        /// <summary> Обработка удара шариком по стене </summary>
        /// <param name="ballHitContact">Сведения о контакте с шариком</param>
        void OnWallHit(ContactPoint2D ballHitContact);
    }
}