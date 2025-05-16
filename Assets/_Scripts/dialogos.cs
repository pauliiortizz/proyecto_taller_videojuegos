using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogos : MonoBehaviour
{
    public GameObject canvas;
    public BurbujaText[] dialogosArray;
    public Button nextButton;
    public TextMeshProUGUI textoDialogo;
    public TextMeshProUGUI textoNombre;

    private int indiceDialogoActual = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        nextButton.onClick.AddListener(SiguienteDialogo);
        MostrarDialogoActual();
    }

    void SiguienteDialogo()
    {
        indiceDialogoActual++;
        if (indiceDialogoActual < dialogosArray.Length)
        {
            MostrarDialogoActual();
        }
        else
        {
            Debug.Log("Fin de los diálogos");
            canvas.SetActive(false);
        }
    }

    void MostrarDialogoActual()
    {
        textoDialogo.text = dialogosArray[indiceDialogoActual].texto;
        textoNombre.text = dialogosArray[indiceDialogoActual].nombre;
    }

   // En dialogos.cs, modifica el OnTriggerEnter2D
void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("El jugador ha entrado en el trigger");

    if (other.CompareTag("Player"))
    {
        // Mostrar diálogo
        indiceDialogoActual = 0;
        canvas.SetActive(true);
        MostrarDialogoActual();
        
        // Notificar al PlayerManager sobre la interacción con la estatua
        PlayerManager player = other.GetComponent<PlayerManager>();
        if (player != null && player.HaveAmulet)
        {
            player.GiveKey();
            player.HaveAmulet = false;
            player.amuletIcon.SetActive(false);
        }
    }
}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }

    void Start()
    {
        // Verificar que el collider está configurado como trigger
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Debug.Log("Collider encontrado, Is Trigger: " + collider.isTrigger);
        }
        else
        {
            Debug.LogError("No se encontró un Collider2D en este objeto");
        }
    }
}
[System.Serializable]
public class BurbujaText
{
    [TextArea(0, 100)]
    public string texto;
    public string nombre;

}