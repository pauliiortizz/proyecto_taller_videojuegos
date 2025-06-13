namespace Dialogue
{
    using UnityEngine;

    public class ObjectWithDialogue : MonoBehaviour //Agregar Tag DialogueObject al objeto
    {
        [SerializeField] private string _Id;
        [SerializeField] private string _KeyToSetWhenFinishDialogue;

        public string Id => _Id; //encapsular para proteger de modificaciones
        public string KeyWhenFinish => _KeyToSetWhenFinishDialogue; // una propiedad que podemos usar si queremos ejecutar una key al finalizar un dialogo (esto tambien puede ser una lista).
    }
}