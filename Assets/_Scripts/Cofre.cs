using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChestController : MonoBehaviour
{
    public GameObject codeCanvasPrefab;
    private GameObject codeCanvasInstance;
    private TMP_InputField codeInput;
    private TextMeshProUGUI errorMessage;
    private TextMeshProUGUI instructionText;
    public string correctCode = "5";

    private bool isCanvasActive = false;

    private void OnMouseDown()
    {
        // Solo instancia el canvas si no está activo
        if (!isCanvasActive)
        {
            // Instancia el prefab si no existe ya
            if (codeCanvasInstance == null)
            {
                codeCanvasInstance = Instantiate(codeCanvasPrefab);
                
                // Busca los elementos del canvas
                codeInput = codeCanvasInstance.transform.Find("Panel/InputField (TMP)").GetComponent<TMP_InputField>();
                errorMessage = codeCanvasInstance.transform.Find("Panel/ErrorMessage").GetComponent<TextMeshProUGUI>();
                instructionText = codeCanvasInstance.transform.Find("Panel/Text (TMP)").GetComponent<TextMeshProUGUI>();

                // Oculta el mensaje de error al principio
                errorMessage.gameObject.SetActive(false);
                
                // Asigna el botón de cerrar
                var closeButton = codeCanvasInstance.transform.Find("Panel/CloseButton").GetComponent<UnityEngine.UI.Button>();
                closeButton.onClick.AddListener(CloseCanvas);

                // Asigna el botón de aceptar
                var confirmButton = codeCanvasInstance.transform.Find("Panel/ConfirmButton").GetComponent<UnityEngine.UI.Button>();
                confirmButton.onClick.AddListener(CheckCode);
            }

            codeCanvasInstance.SetActive(true);
            isCanvasActive = true;
            errorMessage.gameObject.SetActive(false);
            instructionText.text = "Ingresa el código";  // Muestra el mensaje de bienvenida
            codeInput.text = "";  // Limpia el campo de entrada
            codeInput.ActivateInputField();  // Activa el campo para escritura
        }
    }

    public void CheckCode()
    {
        if (codeInput.text == correctCode)
        {
            CloseCanvas();
        }
        else
        {
            errorMessage.text = "Código incorrecto. Intenta de nuevo.";
            errorMessage.gameObject.SetActive(true);

            // Limpia el campo de texto y fuerza el foco de nuevo
            codeInput.text = "";
            codeInput.ActivateInputField();  // Reactiva el campo para permitir nueva escritura
        }
    }

    public void CloseCanvas()
    {
        codeCanvasInstance.SetActive(false);
        isCanvasActive = false;
    }
}
