namespace Dialogue
{
        using System;
        using System.Linq;
        using System.Collections.Generic;
        using UnityEngine;

    public static class DialoguesJSONParser //No es monobehaviour, no necesita estar instanciado en escena
    {
        public static List<DialogueJSONObject> LoadDialoguesFromJSON(string json)
        {
            DialogueListWrapper wrapper = JsonUtility.FromJson<DialogueListWrapper>(json);
            return wrapper.Items.ToList();
        }
    }

    //Basicamente el JSON es una lista o array de un tipo de dato (DialogueJSONOBject)
    //Ese objeto esta dise�ado a partir de una clase que tiene una Id (del objeto que ejecuta el dialogo) y un conjunto de KeyDialogue
    //El  KeyDialogue contiene la key que escucha, el estado con el que se activa y la lista de dialogos de esa key.

    [Serializable]
    public class DialogueListWrapper //Simula el formato del JSON para poder deserializar el mismo en objetos.
    {
        public DialogueJSONObject[] Items;
    }

    [Serializable]
    public struct DialogueJSONObject // Item dentro del JSON
    {
        public string Id;
        public KeyDialogue[] Dialogues;
    }

    [Serializable]
    public struct KeyDialogue //La plantilla del dato dentro del JSON
    {
        public string KeyToEvaluate;
        public bool KeyStatus;
        public string[] Dialogues;
    }
}