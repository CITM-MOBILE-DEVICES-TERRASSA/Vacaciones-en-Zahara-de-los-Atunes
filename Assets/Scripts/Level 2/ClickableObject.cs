using UnityEngine;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour
{    
    public enum ObjectType { Elefante, Monarca, PedroSanchez, ReinaSofia }
    public ObjectType objectType;
        
    private int elefantePoints = 100;
    private int monarcaPoints = -75;
    private int pedroSanchezPoints = 500;
    private int reinaSofiaPoints = -1000;
        
    private ScoreManagerLevel scoreManager;
    private SettingsButton settings;

    void Start()
    {        
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

        if (objectType == ObjectType.PedroSanchez)
        {            
            RandomMover randomMover = gameObject.AddComponent<RandomMover>();
            randomMover.canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        }
    }


    void OnMouseDown()
    {        
        int points = 0;

        if (settings.isPaused != true)
        {
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
                        
            if (scoreManager != null)
            {
                scoreManager.UpdateScoreLevel2(points);
            }

            // Destruye el objeto al hacer clic
            Destroy(gameObject);
        }

    }
}
