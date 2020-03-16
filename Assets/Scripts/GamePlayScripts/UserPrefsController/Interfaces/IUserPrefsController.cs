using GamePlayScripts.UserPrefsController.DataModels;
using UnityEngine;

namespace GamePlayScripts.UserPrefsController.Interfaces
{
    /// <summary>
    ///     Интерфейс контроллера настроек пользователя
    /// </summary>
    public interface IUserPrefsController
    {
        /// <summary> Получить текущие настройки пользователя </summary>
        UserPrefs GetUserPrefs();

        /// <summary> Сохранить в память новые настройки </summary>
        void Save(UserPrefs newUserPrefs);
    }
}