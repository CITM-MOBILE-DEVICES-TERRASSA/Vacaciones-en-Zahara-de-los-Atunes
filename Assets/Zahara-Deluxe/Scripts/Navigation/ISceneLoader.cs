namespace GameCore.Navigation
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName);
        void QuitGame();
    }
}