using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Tooltip("El componente Animation que tiene tu clip")]
    public Animation targetAnimation;

    public Animator textAnimator;
    public Animator doorAnimator;
    public Animator bgAnimator;

    [Tooltip("El nombre exacto de tu clip")]
    public string clipName;

    void Update()
    {
       if(Input.GetMouseButtonDown(0))
       {
           textAnimator.SetTrigger("Play");
           doorAnimator.SetTrigger("Play");
           bgAnimator.SetTrigger("Play");

           
           //ejecutar corrutina
           StartCoroutine((WaitToAnimate()));   
            
       } 
    }

    //corrutina
    IEnumerator WaitToAnimate()
    {
        yield return new WaitForSeconds(1); //esperar 1 seg
        
        SceneManager.LoadScene(1); //ejecuta la accion
    }
}

