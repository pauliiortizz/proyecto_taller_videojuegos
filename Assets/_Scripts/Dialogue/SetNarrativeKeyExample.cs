namespace Dialogue
{

    using UnityEngine;

    public class SetNarrativeKeyExample : MonoBehaviour
    {
        [SerializeField] private string _key;

        public void SetKeyExample()
        {
            NarrativeManager.SetKeyStatus(_key, true);
        }
    }
}
