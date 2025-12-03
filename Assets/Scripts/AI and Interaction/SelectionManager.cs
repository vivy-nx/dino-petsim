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
    public UIManager uiManager;  // if we change the dino name

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
                // if the player clicks on this object, interact with it
                if (Input.GetMouseButtonDown(0))  // left click to interact
                {
                    InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();
                    if (interactable != null)
                    {
                        HandleInteraction(interactable);  // call a method to handle the interaction
                    }
                }
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
    private void HandleInteraction(InteractableObject interactable)
    {
        // if this interactable object is a name-changing item or interactable dinosaur
        if (interactable.ItemName == "NameChangeItem")
        {
           // uiManager.ChangeDinoName();  // trigger name change UI in UIManager   
        }
    }

}