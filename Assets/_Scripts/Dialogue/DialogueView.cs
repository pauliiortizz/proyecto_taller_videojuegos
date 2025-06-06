namespace Dialogue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using TMPro;

    public class DialogueView : MonoBehaviour, IPointerClickHandler //Detectar clicks en render
    {
        [SerializeField] private TextMeshProUGUI _dialogueLabel;
        [SerializeField] private TextMeshProUGUI _speakerLabel;
        [SerializeField] private float _wordsTypingSpeedPerMinute = 50f;

        private Coroutine ShowDialogueCoroutine;
        private Coroutine AnimatedDialogueCoroutine;
        private bool clickToNextDialogue = false;

        //Para probar en Unity (click derecho en el componente y elegir Test Animated Dialogue para probar el funcionamiento fuera de contexto.
        [ContextMenu("TestAnimatedDialogue")] 
        public void TestAnimatedDialogue()
        {
            var testDialogue1 = new DialogueData(Dialogue: "Hola, soy un dialogo", Speaker: "Alguien");
            var testDialogue2 = new DialogueData(Dialogue: "Ahora soy otro dialogo", Speaker: "Alguien");

            List <DialogueData> testDialogues = new List<DialogueData>() { testDialogue1, testDialogue2 };

            ShowDialog(testDialogues, "");
        }

        public void ShowDialog(List<DialogueData> data, string keyWhenFinishDialogue)
        {
            if (ShowDialogueCoroutine != null) return; //no disparar dialogos si hay uno en ejecucion

            StopAllCoroutines(); // Detener cualquier dialogo en ejecucion.

            gameObject.SetActive(true);

            ShowDialogueCoroutine = StartCoroutine(EnqueueDialogues(data, keyWhenFinishDialogue));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            clickToNextDialogue = true;
        }

        public void HideDialog()
        {
            ShowDialogueCoroutine = null;
            gameObject.SetActive(false);
        }

        IEnumerator EnqueueDialogues(List<DialogueData> data, string keyWhenFinishDialogue)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (AnimatedDialogueCoroutine != null)
                    StopCoroutine(AnimatedDialogueCoroutine); //detener corutina de anterior dialogo.

                clickToNextDialogue = false; //reseteaar click

                _dialogueLabel.text = "";
                _speakerLabel.text = data[i].Speaker;

                float typingSpeed = GetTypingSpeedPerLetterPerSeccond();

                AnimatedDialogueCoroutine = StartCoroutine(AnimatedDialogue(data[i].Dialogue.ToCharArray(), typingSpeed));

                float GetTypingSpeedPerLetterPerSeccond()
                {
                    int wordCount = data[i].Dialogue.Split(' ').Length;

                    return (wordCount / _wordsTypingSpeedPerMinute) / 60;
                }

                yield return new WaitUntil(() => clickToNextDialogue == true); //esperar a un nuevo click
            }

            clickToNextDialogue = false;

            //Una vez que terminan todos los dialogos esperar al click y cerrar el cuadro de dialogo.

            yield return new WaitUntil(() => clickToNextDialogue = true);

             Debug.Log("Cerrar");

            if(!string.IsNullOrEmpty(keyWhenFinishDialogue)) //si tenemos key para activar al finalizar un dialogo..
            {
                NarrativeManager.SetKeyStatus(keyWhenFinishDialogue, true); //seteamos el status nuevo
                Debug.Log("Key set to true: " + keyWhenFinishDialogue);
            }

            HideDialog();
        }

        IEnumerator AnimatedDialogue(char[] dialogue, float typingSpeed)
        {
            for (int i = 0; i < dialogue.Length; i++)
            {
                _dialogueLabel.text += dialogue[i];
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
        }
    }

    [Serializable]
    public struct DialogueData
    {
        public string Speaker;
        public string Dialogue;

        public DialogueData(string Dialogue, string Speaker)
        {
            this.Dialogue = Dialogue;
            this.Speaker = Speaker;
        }
    }
}