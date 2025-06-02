using Naninovel;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace Game
{
    public class QuestlogUI : CustomUI
    {
        [SerializeField] private Transform _questlogContainer;
        [SerializeField] private TextMeshProUGUI _messageTemplate;
        [SerializeField] private float _typeSymbolDelay = 0.05f;

        protected override void Awake()
        {
            base.Awake();
            _messageTemplate.gameObject.SetActive(false);
        }

        public async UniTask PrintMessageAsync(string newMessage, AsyncToken asyncToken = default)
        {
            if (string.IsNullOrEmpty(newMessage))
                return;

            var messageInstance = Instantiate(_messageTemplate, _questlogContainer);
            messageInstance.text = newMessage;
            messageInstance.maxVisibleCharacters = 0;

            messageInstance.gameObject.SetActive(true);

            for (int i = 0; i < newMessage.Length; i++)
            {
                if (asyncToken.CancellationToken.IsCancellationRequested)
                    return;

                messageInstance.maxVisibleCharacters++;

                await UniTask.Delay((int)(_typeSymbolDelay * 1000), cancellationToken: asyncToken.CancellationToken);
            }
        }
    }
}
