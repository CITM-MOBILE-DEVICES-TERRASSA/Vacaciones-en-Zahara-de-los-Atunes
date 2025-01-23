namespace GameCore.Navigation
{
    public class LobbyController : UISceneController
    {
        public void OnMinigame1Button()
        {
            NavigateToScene(SceneNames.GameSelection);
        }

        public void OnMinigame2Button()
        {
            NavigateToScene(SceneNames.JumpingJack);
        }
    }
}