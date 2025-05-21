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

    void SiguienteDialogo()
    {
        /*indiceDialogoActual++;
        //parametro clave
        if (indiceDialogoActual < dialogosArray.Length)
        {
            MostrarDialogoActual();
        }
        else
        {
            Debug.Log("Fin de los diálogos");*/
            canvas.SetActive(false);
       // }
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

        if (other.CompareTag("Dialog"))
        {
            if(other.gameObject.name == "Cofre") //ojoal guardar escena y q no tenga el mismo nombr eel cofre
            {
                string keyToEvaluate = "Cofre";
                bool keyStatus = NarrativeManager.instance.GetPearl(keyToEvaluate);

                if(keyStatus)
                {
                    indiceDialogoActual = 1;

                    canvas.SetActive(true);
                    MostrarDialogoActual();
                }
                else
                {
                    indiceDialogoActual = 0;

                    canvas.SetActive(true);
                    MostrarDialogoActual();
                }
            }

            if(other.gameObject.name == "Estatua") //ojoal guardar escena y q no tenga el mismo nombr eel cofre
            {
              string keyToEvaluate = "Estatua";
                bool keyStatus = NarrativeManager.instance.GetPearl(keyToEvaluate);

                if(keyStatus)
                {
                    indiceDialogoActual = 3;

                    canvas.SetActive(true);
                    MostrarDialogoActual();
                }
                else
                {
                    indiceDialogoActual = 2;

                    canvas.SetActive(true);
                    MostrarDialogoActual();
                }
            }

             if(other.gameObject.name == "Runas") //ojoal guardar escena y q no tenga el mismo nombr eel cofre
            {
              string keyToEvaluate = "Runas";
                bool keyStatus = NarrativeManager.instance.GetPearl(keyToEvaluate);

                if(keyStatus)
                {
                    indiceDialogoActual = 5;

                    canvas.SetActive(true);
                    MostrarDialogoActual();
                }
                else
                {
                    indiceDialogoActual = 4;

                    canvas.SetActive(true);
                    MostrarDialogoActual();
                }
            }

            
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
}
[System.Serializable]
public class BurbujaText
{
    [TextArea(0, 100)]
    public string texto;
    public string nombre;
    public string keyToEvaluate;
    public bool keyStatus;
}