using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] inventoryIcons; // Imágenes de llaves en la UI

    void Start()
    {
        // Restaura todas las vidas antes de reiniciar la escena
        for (int i = 0; i < 4; i++)
        {
            inventoryIcons[i].SetActive(false);
        }
    }
}
