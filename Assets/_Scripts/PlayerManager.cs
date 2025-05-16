using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] keyIcons; // Imágenes de llaves en la UI
    public GameObject amuletIcon; // Imágen de amuleto en la UI
    private int keyNumber = 0; // Cont de llaves
    public bool HaveAmulet = false; // Bandera para verificar si el jugador tiene el amuleto
    public bool HaveKeys = false; // Bandera para verificar si el jugador tiene llaves


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Amulet"))
        {
            GiveAmulet(); // Llama a la función para dar el amuleto
            Destroy(collision.gameObject); // Destruye el objeto amuleto recogido
            Debug.Log("Amuleto recogido"); // Mensaje de depuración
            HaveAmulet = true; // Cambia la bandera a verdadero
        }

        if (collision.gameObject.CompareTag("Statue") && HaveAmulet)
        {
            amuletIcon.SetActive(false); //Oculta el amuleto en la UI
            GiveKey(); // Llama a la función para dar la llave
            Debug.Log("Llave obtenida"); // Mensaje de depuración
            HaveAmulet=false; // Cambia la bandera a falso
        }

        if (collision.gameObject.CompareTag("Wizard") && HaveKeys)
        {
            for (; keyNumber >= 0; keyNumber--)
            {
                keyIcons[keyNumber].SetActive(false); // Desactiva todas las llaves
            }
            Debug.Log("Llaves borradas"); // Mensaje de depuración
            SceneManager.LoadScene(2); //Cambia la escena al final
        }
    }
    public void GiveKey()
    {
        keyIcons[keyNumber].SetActive(true); // Activa solo la primera llave
        keyNumber++; // Incrementa el número de llaves
        if (keyNumber >= keyIcons.Length)
        {
            HaveKeys = true; // Cambia la bandera a verdadero
        }
    }


    void GiveAmulet()
    {
        amuletIcon.SetActive(true); //Muestra el amuleto en la UI
    }
}
