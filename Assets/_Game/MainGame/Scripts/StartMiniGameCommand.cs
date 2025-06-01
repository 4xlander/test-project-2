using Naninovel;

namespace Game
{
    [CommandAlias("minigame")]
    public class StartMiniGameCommand : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken)
        {
            var miniGameService = Engine.GetService<MiniGameService>();
            bool isWin = await miniGameService.PlayMiniGameAsync();

            asyncToken.ThrowIfCanceled();

            Engine.GetService<ICustomVariableManager>()
                .SetVariableValue("MiniGameResult", isWin.ToString());
        }
    }
}
