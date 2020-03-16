using UnityEngine;

namespace Extensions
{
    /// <summary>
    ///     Набор расширяющих методов для типа <see cref="LayerMask"/>
    /// </summary>
    public static class LayerMaskExtensions
    {
        /// <summary>Проверить, что аска слоёв содержит укзаанный слой </summary>
        public static bool IsContainsLayer(this LayerMask layerMask, int layerToCheck)
        {
            return (layerMask.value & 1 << layerToCheck) == (1 << layerToCheck);
        }
    }
}