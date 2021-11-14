using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class UserTransition : MonoBehaviour
{
    public Canvas TransitionCanvas;
    public GameObject Crosshair;
    public static bool TransitionMade = false;

    PointerControls rayCaster;
    Animator transition;

    void Start()
    {
        rayCaster = Crosshair.GetComponent<PointerControls>();
        transition = TransitionCanvas.GetComponent<Animator>();
    }

    void Update()
    {
        IEnumerator startTransition = MakeTransition();
        if (rayCaster.GetCurrentObject().Equals("Login") || rayCaster.GetCurrentObject().Equals("Register"))
        {
            if (TransitionMade)
            {
                StopAllCoroutines();
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ExitTransition();
                }
            }
            else
            {
                StartCoroutine(startTransition);
            }   
        }
    }

    public IEnumerator MakeTransition()
    {
        transition.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        Crosshair.SetActive(false);
        TransitionMade = true;
        Debug.Log("Transition Made");
    }

    public void ExitTransition()
    {
        Time.timeScale = 1f;
        transition.SetTrigger("FinishTransition");
        TransitionMade = false;
        Crosshair.SetActive(true);
        rayCaster.TransitionFinished();
        Debug.Log("Transition Exited");
    }
}
