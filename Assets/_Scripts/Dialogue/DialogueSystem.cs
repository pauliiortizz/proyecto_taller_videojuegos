namespace Dialogue
{

    using System.Linq; //extiende funciones de lista
    using System.Collections.Generic;

    public static class DialogueSystem //No es monobehaviour, no necesita estar instanciado en escena
    {
        private static List<DialogueJSONObject> _allDialogues;

        public static void Initialize(string json)
        {
            _allDialogues = DialoguesJSONParser.LoadDialoguesFromJSON(json);

            var allKeys = _allDialogues.SelectMany(dialogue => dialogue.Dialogues).Select(keyDialogue => keyDialogue.KeyToEvaluate); //selecciono de todos los dialogos, las keys que evaluan

            var allUniqueKeys = allKeys.ToHashSet(); //hashset permite 1 solo valor, hace keys unicas

            NarrativeManager.Initialize(allUniqueKeys);
        }

        public static List<DialogueData> EvaluateDialogueWith(string objectId)
        {
            List<DialogueData> dialoguesData = new();
            List<string> dialoguesToShow = new();

            var objectDialogues = _allDialogues.Find(dialogItem => dialogItem.Id == objectId);
            foreach (KeyDialogue keyDialogue in objectDialogues.Dialogues)
            {
                Status keyStatus = NarrativeManager.GetKeyStatus(keyDialogue.KeyToEvaluate);

                if(keyStatus == Status.NULL) continue;

                bool keyStatusValue = keyStatus == Status.TRUE ? true : false;


                if(keyStatusValue == keyDialogue.KeyStatus) //si la key tiene el mismo estado que la key a evaluar
                {
                    dialoguesToShow.AddRange(keyDialogue.Dialogues); //agrego todos los dialogos
                }
            }

            //Por cada dialogo a mostrar, creo una instancia para representar el dialogo del objeto con el que estoy interactuando
            foreach (var dialogueData in dialoguesToShow)
            {
                string speaker = dialogueData.Split('/')[0]; //parte del dialogo antes de la '/'
                string dialogue = dialogueData.Split('/')[1]; //parte del dialogo luego de la '/'

                DialogueData data = new(dialogue, speaker);

                dialoguesData.Add(data);
            }

            return dialoguesData;
        }
    }

    public static class NarrativeManager //No es monobehaviour, no necesita estar instanciado en escena
    {
        private static Dictionary<string, Status> KeyToStatus = new(); //inicializo sino queda nulo

        public static void Initialize(HashSet<string> _allKeys)
        {
            foreach (string key in _allKeys)
            {
                KeyToStatus.Add(key, Status.FALSE); //agrego todas las keys del json y las inicializo en false.
            }
        }

        public static void DeleteKey(string key)
        {
            if (KeyToStatus.ContainsKey(key))
            {
                KeyToStatus.Remove(key);
            }
        }

        public static void SetKeyStatus(string key, bool newStatus)
        {
            Status statusToShow = newStatus ? Status.TRUE : Status.FALSE;

            if (KeyToStatus.ContainsKey(key))
            {
                KeyToStatus[key] = statusToShow;
            }
        }

        public static Status GetKeyStatus(string key)
        {
            if (KeyToStatus.TryGetValue(key, out Status status))
            {
                return status;
            }
            else
            {
                //throw new System.Exception($"No se encuentra una key con el nombre {key} en Dialogues.json");
                return Status.NULL;
            }
        }
    }

    public enum Status
    {
        TRUE,
        FALSE,
        NULL
    }
}

