using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    public Button myButton; // El bot�n que cambiar� de color.
    public bool isActive;   // Estado que controla el color.
    private ButtonManager buttonManager; // Referencia al ButtonManager.

    private void Awake()
    {
        // Busca el objeto llamado "button_Manager" en la escena.
        GameObject managerObject = GameObject.Find("button_Manager");

        if (managerObject != null)
        {
            // Obt�n el componente ButtonManager del objeto encontrado.
            buttonManager = managerObject.GetComponent<ButtonManager>();

            if (buttonManager == null)
            {
                Debug.LogError("No se encontr� el componente ButtonManager en button_Manager.");
            }
            else
            {

            }
        }
        else
        {
            Debug.LogError("No se encontr� un objeto llamado button_Manager en la escena.");
        }
    }

    void Update()
    {
        if (buttonManager != null)
        {
            // Actualiza el estado desde el ButtonManager.
            isActive = buttonManager.attack;

            // Cambia el color seg�n el estado.
            myButton.image.color = isActive ? Color.red : Color.white;
        }
    }
}
