using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button myButton; // Referencia al bot�n
    private LevelSelector levelSelector; // Referencia al LevelSelector

    private void Start()
    {
        // Obtener la referencia a LevelSelector (aseg�rate de que haya una instancia en la escena)
        levelSelector = LevelSelector.Instance;

        if (myButton != null && levelSelector != null)
        {
            // Asignar el evento OnClick del bot�n para que llame a la funci�n ChangeToLevelSelector
            myButton.onClick.AddListener(levelSelector.ChangeToLevelSelector);
        }
        else
        {
            Debug.LogError("No se ha asignado un bot�n o LevelSelector no est� presente en la escena.");
        }
    }
}
