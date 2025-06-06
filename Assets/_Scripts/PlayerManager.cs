using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] keyIcons; // Imágenes de llaves en la UI
    public GameObject amuletIcon; // Imágen de amuleto en la UI
    private int keyNumber = 0; // Contador de llaves
    public bool HaveAmulet = false; // Bandera para verificar si el jugador tiene el amuleto
    public AudioSource keySound; // Referencia al AudioSource del sonido de llave

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Amulet"))
        {
            GiveAmulet(); // Llama a la función para dar el amuleto
            Destroy(collision.gameObject); // Destruye el objeto amuleto recogido
            Debug.Log("Amuleto recogido"); // Mensaje de depuración
            HaveAmulet = true; // Cambia la bandera a verdadero
            Dialogue.NarrativeManager.SetKeyStatus("HasAmulet", true);
        }

        if (collision.gameObject.CompareTag("Statuekey") && HaveAmulet)
        {
            amuletIcon.SetActive(false); // Oculta el amuleto en la UI
            GiveKey(); // Llama a la función para dar la llave
            Debug.Log("Llave obtenida"); // Mensaje de depuración
            HaveAmulet = false; // Cambia la bandera a falso
        }
    }

    public void GiveKey()
    {
        keyIcons[keyNumber].SetActive(true); // Activa la llave correspondiente en la UI
        keyNumber++; // Incrementa el número de llaves

        // Reproduce el sonido de la llave (si está asignado)
        if (keySound != null)
        {
            keySound.Play();
        }
        else
        {
            Debug.LogWarning("No hay AudioSource asignado para el sonido de la llave.");
        }
    }

    void GiveAmulet()
    {
        amuletIcon.SetActive(true); // Muestra el amuleto en la UI
    }
}