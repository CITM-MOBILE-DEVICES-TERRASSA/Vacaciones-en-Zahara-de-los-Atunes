namespace GameCore.Navigation
{
    public class MainMenuController : UISceneController
    {
        public void OnPlayButton()
        {
            SceneLoader.Instance.LoadScene(SceneNames.Lobby);
        }

        public void OnCollectiblesButton()
        {
            NavigateToScene(SceneNames.Collectibles);
        }

        public void OnQuitButton()
        {
            QuitGame();
        }
    }
}