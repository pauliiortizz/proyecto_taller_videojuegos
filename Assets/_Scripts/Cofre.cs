using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChestController : MonoBehaviour
{
    public GameObject codeCanvas;
    public TMP_InputField codeInput;
    public TextMeshProUGUI errorMessage;
    public string correctCode = "1234";

    private bool isCanvasActive = false;

    private void Start()
    {
        codeCanvas.SetActive(false);
        errorMessage.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isCanvasActive)
        {
            codeCanvas.SetActive(true);
            isCanvasActive = true;
            errorMessage.gameObject.SetActive(false);
            codeInput.text = "";  // Limpia el campo de entrada
            codeInput.ActivateInputField();  // Garantiza el foco al abrir
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
            codeInput.text = "";  // Limpia el campo

            // 🔄 Actualiza el foco para poder escribir de nuevo
            codeInput.DeactivateInputField();  // Quita el foco
            codeInput.ActivateInputField();    // Lo vuelve a activar
        }
    }

    public void CloseCanvas()
    {
        codeCanvas.SetActive(false);
        isCanvasActive = false;
    }
}
