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
    private PlayerManager player;

    private void OnMouseDown()
    {
        // Obtener el PlayerManager si no está asignado
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerManager>();
            }
        }

        if (!isCanvasActive)
        {
            if (codeCanvasInstance == null)
            {
                codeCanvasInstance = Instantiate(codeCanvasPrefab);
                codeInput = codeCanvasInstance.transform.Find("Panel/InputField (TMP)").GetComponent<TMP_InputField>();
                errorMessage = codeCanvasInstance.transform.Find("Panel/ErrorMessage").GetComponent<TextMeshProUGUI>();
                instructionText = codeCanvasInstance.transform.Find("Panel/Text (TMP)").GetComponent<TextMeshProUGUI>();

                errorMessage.gameObject.SetActive(false);
                
                var closeButton = codeCanvasInstance.transform.Find("Panel/CloseButton").GetComponent<UnityEngine.UI.Button>();
                closeButton.onClick.AddListener(CloseCanvas);

                var confirmButton = codeCanvasInstance.transform.Find("Panel/ConfirmButton").GetComponent<UnityEngine.UI.Button>();
                confirmButton.onClick.AddListener(CheckCode);
            }

            codeCanvasInstance.SetActive(true);
            isCanvasActive = true;
            errorMessage.gameObject.SetActive(false);
            instructionText.text = "Ingresa el código";
            codeInput.text = "";
            codeInput.ActivateInputField();
        }
    }

    public void CheckCode()
    {
        if (codeInput.text == correctCode)
        {
            CloseCanvas();
            if (player != null) // Verificar que player no sea null
            {
                player.GiveKey();
            }
        }
        else
        {
            errorMessage.text = "Código incorrecto. Intenta de nuevo.";
            errorMessage.gameObject.SetActive(true);
            codeInput.text = "";
            codeInput.ActivateInputField();
        }
    }

    public void CloseCanvas()
    {
        codeCanvasInstance.SetActive(false);
        isCanvasActive = false;
    }
}