namespace GameCore.Navigation
{
    public class LobbyController : UISceneController
    {
        public void OnMinigame1Button()
        {
            NavigateToScene(SceneNames.GameSelection);
        }
    }
}