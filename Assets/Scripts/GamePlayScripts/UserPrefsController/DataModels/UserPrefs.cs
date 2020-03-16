using System;

namespace GamePlayScripts.UserPrefsController.DataModels
{
    /// <summary>
    ///     Класс с параметрами настроек пользователя
    /// </summary>
    [Serializable]
    public class UserPrefs
    {
        /// <summary> Наилучший результат пользователя </summary>
        public int UserScore;
    }
}