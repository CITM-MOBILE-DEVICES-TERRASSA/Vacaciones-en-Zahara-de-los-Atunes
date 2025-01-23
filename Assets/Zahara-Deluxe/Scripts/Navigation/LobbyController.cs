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
        public void OnMinigame3Button()
        {
            NavigateToScene(SceneNames.FelixJump);
        }
        public void OnMinigame4Button()
        {
            NavigateToScene(SceneNames.Fiki);
        }
    }
}