using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class UserTransition : MonoBehaviour
{
    public Canvas TransitionCanvas;
    public GameObject Crosshair;
    public static bool TransitionMade = false;
    public GameObject LoginMenu;
    public GameObject RegisterMenu;

    PointerControls rayCaster;
    Animator transition;
	bool menuOption;

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
			menuOption = rayCaster.GetCurrentObject().Equals("Login");
            if (TransitionMade)
            {
                StopAllCoroutines();
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
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
        Crosshair.SetActive(false);
        if (menuOption) LoginMenu.SetActive(true);
		else RegisterMenu.SetActive(true);
        TransitionMade = true;
        Debug.Log("Transition Made");
    }

    public void ExitTransition()
    {
        Time.timeScale = 1f;
        transition.SetTrigger("FinishTransition");
        TransitionMade = false;
        Crosshair.SetActive(true);
        if (menuOption) LoginMenu.SetActive(false);
		else RegisterMenu.SetActive(false);
        rayCaster.TransitionFinished();
        Debug.Log("Transition Exited");
    }
}
