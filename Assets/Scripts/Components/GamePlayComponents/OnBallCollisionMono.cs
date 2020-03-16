using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Компонент в который могут быть переданы сведения о том, что произошла коллизия с мячиком пользователя
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class OnBallCollisionMono : MonoBehaviour
    {
        [SerializeField] private LayerMask _userLayerMask;

        [SerializeField]
        private OnBallCollisionEvent _onBallCollision = new OnBallCollisionEvent();

        [System.Serializable]
        public class OnBallCollisionEvent : UnityEvent<ContactPoint2D>{}

        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_userLayerMask.IsContainsLayer(collision.gameObject.layer))
            {
                var contactsNumber = collision.contacts.Length;
                if (contactsNumber == 0)
                    return;

                if (contactsNumber > 1)
                    Debug.LogWarning("<color=red>Произошло более 1 контакта с шариком, это противоречит логике игры</color>");

                var contact = collision.contacts.First();
                _onBallCollision.Invoke(contact);
            }
        }


#if UNITY_EDITOR
        /// <summary> Уменьшаем ручной труд и используем хардкод и рефлекшен, чтобы заполнить поля, которые скорее всего так и будут заполненны </summary>
        private void OnValidate()
        {
            if (_userLayerMask.value == 0)
            {
                _userLayerMask = LayerMask.GetMask("PlayerBall");
            }

            if (_onBallCollision.GetPersistentEventCount() == 0)
            {
                var allComponents = GetComponents<MonoBehaviour>();
                var allCallbacks = allComponents
                    .Select(c => new
                    {
                        Instanse = c,
                        Methods = c.GetType()
                            .GetMethods()
                            .Where(m => m.IsPublic && !m.IsGenericMethod && !m.IsStatic)
                            .Where(m => m.GetParameters().Length == 1)
                            .Where(m => m.GetParameters().First().ParameterType == typeof(ContactPoint2D))
                    });

                foreach (var evenCallback in allCallbacks)
                {
                    foreach (var callbackMethod in evenCallback.Methods)
                    {
                        var callBackInvocationDelegate = callbackMethod.CreateDelegate(typeof(UnityAction<ContactPoint2D>), evenCallback.Instanse);
                        if (!(callBackInvocationDelegate is UnityAction<ContactPoint2D> castedDelegate))
                            continue;

                        UnityEditor.Events.UnityEventTools.AddPersistentListener(_onBallCollision, castedDelegate);
                    }
                }
            }
        }
#endif
    }
}