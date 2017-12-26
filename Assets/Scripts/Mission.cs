using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		theStateManager = GameObject.FindObjectOfType<StateManager>();
        modalPanel = ModalPanel.Instance();
    }

    private StateManager theStateManager;
    private ModalPanel modalPanel;

    public void MissionWindow () {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {
            text = "This is the mission ..."
        };
        modalPanelDetails.button1Details = new EventButtonDetails {
            buttonTitle = "Success",
            action = missionSuccess
        };
        modalPanelDetails.button2Details = new EventButtonDetails {
            buttonTitle = "Failed",
            action = missionFailed
        };

        modalPanel.TextWindow(modalPanelDetails);
    }

    public void missionSuccess() {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_MISSION_COMPLETED) {
            theStateManager.missionStatus = true;
            theStateManager.currentPhase = StateManager.TurnPhase.MISSION_COMPLETED;
            Debug.Log("Mission Success");
        }
    }

    public void missionFailed() {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_MISSION_COMPLETED) {
            theStateManager.missionStatus = false;
            theStateManager.currentPhase = StateManager.TurnPhase.MISSION_COMPLETED;
            Debug.Log("Mission Failed");
        }
    }
}
