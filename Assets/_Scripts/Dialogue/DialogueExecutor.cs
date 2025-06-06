namespace Dialogue
{
    using System.Collections.Generic;
    using UnityEngine;

    public class DialogueExecutor : MonoBehaviour //Solo un objeto con este componente (el player)
    {
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private TextAsset _jsonFile;

        private void Awake()
        {
            DialogueSystem.Initialize(_jsonFile.text);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Dialog") 
            {
                var objectWithDialogue = collision.GetComponent<ObjectWithDialogue>();

                string objectId = objectWithDialogue.Id;

            List<DialogueData> data = DialogueSystem.EvaluateDialogueWith(objectId);

                _dialogueView.ShowDialog(data, objectWithDialogue.KeyWhenFinish);
            }
        }
    }
}

