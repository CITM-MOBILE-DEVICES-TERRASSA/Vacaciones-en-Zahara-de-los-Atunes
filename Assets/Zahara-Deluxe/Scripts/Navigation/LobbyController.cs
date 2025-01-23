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
            NavigateToScene(SceneNames.ColorsMagic);
        }
        public void OnMinigame4Button()
        {
            NavigateToScene(SceneNames.FelixJump);
        }
        public void OnMinigame5Button()
        {
            NavigateToScene(SceneNames.Fiki);
        }
    }
}