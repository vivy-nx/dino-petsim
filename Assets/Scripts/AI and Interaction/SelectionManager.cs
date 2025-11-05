using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public GameObject interactionInfo; // public so we can reference it in the inspector
    TextMeshProUGUI interaction_text;                 // references the text component

    private void Start()
    {
        interaction_text = interactionInfo.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // creates a ray because we want to cast one out from the middle of the screen
        // we use mouse position because we locked it in the middle of the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // makes sure that it hits something
        {
            var selectionTransform = hit.transform; // place whatever is in the hit into the var

            if (selectionTransform.GetComponent<InteractableObject>()) // makes sure it only gives the info of interactable obj
            {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interactionInfo.SetActive(true);
            }
            else  // hit w/o interactable script
            {
                interactionInfo.SetActive(false);
            }

        } 
        else // no hit
        {
            interactionInfo.SetActive(false); // makes sure then if we go from obj to sky there is no bug keeping text on
        }                                     // since sky is not an obj
    }
}