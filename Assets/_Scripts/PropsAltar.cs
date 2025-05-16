using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;
        public string targetTag = "Box";  // Etiqueta que debe tener la caja

        private Color curColor;
        private Color targetColor;
        private PlayerManager player;

        private void Awake()
        {
            targetColor = runes[0].color;
            targetColor.a = 0.0f;  // Empieza invisible
            curColor = targetColor;

            // Buscar al jugador al inicio (asume que tiene el tag "Player")
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerManager>();
            }
            else
            {
                Debug.LogError("No se encontró un objeto con tag 'Player'");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Solo activa las runas si el objeto tiene la etiqueta correcta
            if (other.CompareTag(targetTag))
            {
                if (player != null) // Verificar que player no sea null
                {
                    player.GiveKey();
                }
                else
                {
                    Debug.LogError("PlayerManager no asignado en PropsAltar");
                }
                targetColor.a = 1.0f;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Solo desactiva las runas si el objeto que sale es la caja
            if (other.CompareTag(targetTag))
            {
                targetColor.a = 0.0f;
            }
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
}