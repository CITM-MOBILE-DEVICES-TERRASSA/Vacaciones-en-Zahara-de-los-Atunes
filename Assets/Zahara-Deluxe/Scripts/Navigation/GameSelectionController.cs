namespace GameCore.Navigation
{
    public class GameSelectionController : UISceneController
    {
        public void OnGame1Button()
        {
            SceneLoader.Instance.LoadSceneWithLoading(SceneNames.PescaitoFrito);
        }

        public void OnGame2Button()
        {
            SceneLoader.Instance.LoadSceneWithLoading(SceneNames.Game2);
        }
    }
}