using Naninovel;

namespace Game
{
    [CommandAlias("questlog")]
    public class QuestlogCommand : Command
    {

        public StringParameter Message;

        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var questlogUI = Engine.GetService<IUIManager>().GetUI<QuestlogUI>();
            if (questlogUI == null)
                return;

            await questlogUI.PrintMessageAsync(Message.Value);
        }
    }
}
