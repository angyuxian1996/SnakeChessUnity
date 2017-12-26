using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    void Awake() {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        modalPanel = ModalPanel.Instance();
    }

    private StateManager theStateManager;
    private ModalPanel modalPanel;

    public void WinWindow() {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {
            text = "Player " + (theStateManager.currentPlayer + 1) + " Won!"
        };
        modalPanelDetails.button1Details = new EventButtonDetails {
            buttonTitle = "Close",
            action = DoNothing
        };

        modalPanel.TextWindow(modalPanelDetails);
    }

    void DoNothing() {
        Debug.Log("Game Over.");
    }
}
