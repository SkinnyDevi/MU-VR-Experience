using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControls : MonoBehaviour
{
    // Update is called once per frameººº
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 50f))
			{
				if (hit.transform)
				{
					PrintObj(hit.transform.gameObject);
				}
			}
		}
    }

	void PrintObj(GameObject go)
	{
		print(go.name);
	}
}
