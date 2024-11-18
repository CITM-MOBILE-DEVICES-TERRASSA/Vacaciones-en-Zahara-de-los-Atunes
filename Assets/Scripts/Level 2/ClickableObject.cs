using UnityEngine;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour
{
    // Tipo de objeto y los puntos asociados
    public enum ObjectType { Elefante, Monarca, PedroSanchez, ReinaSofia }
    public ObjectType objectType;

    // Puntos de cada tipo de objeto
    private int elefantePoints = 100;
    private int monarcaPoints = -75;
    private int pedroSanchezPoints = 500;
    private int reinaSofiaPoints = -1000;

    // Referencia al controlador de puntaje
    private ScoreManagerLevel scoreManager;
    private SettingsButton settings;

    void Start()
    {
        // Busca el ScoreManager en la escena para actualizar el puntaje
        scoreManager = FindObjectOfType<ScoreManagerLevel>();

        if (scoreManager == null)
        {
            Debug.LogError("No se encontró el ScoreManager en la escena.");
        }

        settings = FindObjectOfType<SettingsButton>();

        if (settings == null)
        {
            Debug.LogError("No se encontró el SettingsButton en la escena.");
        }
    }


    void OnMouseDown()
    {
        // Determina los puntos basados en el tipo de objeto
        int points = 0;

        if (settings.isPaused != true) {
            switch (objectType)
            {
                case ObjectType.Elefante:
                    points = elefantePoints;
                    break;
                case ObjectType.Monarca:
                    points = monarcaPoints;
                    break;
                case ObjectType.PedroSanchez:
                    points = pedroSanchezPoints;
                    break;
                case ObjectType.ReinaSofia:
                    points = reinaSofiaPoints;
                    break;
            }

            // Actualiza el puntaje
            if (scoreManager != null)
            {
                scoreManager.UpdateScoreLevel2(points);
            }

            // Destruye el objeto al hacer clic
            Destroy(gameObject);
        }

    }
}
