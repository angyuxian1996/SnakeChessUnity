using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        modalPanel = ModalPanel.Instance();
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    StateManager theStateManager;
    public Dropdown dropdown;
    private ModalPanel modalPanel;

    public void MoveWindow() {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {
            text = "Please throw the dice and choose the total of the upper face of the dice:"
        };
        modalPanelDetails.dropdown = true;
        modalPanelDetails.button1Details = new EventButtonDetails {
            buttonTitle = "Move",
            action = MoveForward
        };

        modalPanel.TextWindow(modalPanelDetails);
    }

    void MoveForward () {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_ROLL) {
            Debug.Log("Move " + (dropdown.value + 1));
            theStateManager.stepNumber = dropdown.value + 1;
            theStateManager.currentPhase = StateManager.TurnPhase.START_MOVING;
        }
    }
}
