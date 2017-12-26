using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour {

    // Use this for initialization
    void Awake() {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        modalPanel = ModalPanel.Instance();
    }

    private StateManager theStateManager;
    private ModalPanel modalPanel;

    public void ChallengeWindow() {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {
            text = "This is the challenge ..."
        };
        modalPanelDetails.button1Details = new EventButtonDetails {
            buttonTitle = "Success",
            action = challengeSuccess
        };
        modalPanelDetails.button2Details = new EventButtonDetails {
            buttonTitle = "Failed",
            action = challengeFailed
        };

        modalPanel.TextWindow(modalPanelDetails);
    }

    public void challengeSuccess() {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_CHALLENGE_COMPLETED) {
            theStateManager.challengeStatus = true;
            theStateManager.currentPhase = StateManager.TurnPhase.CHALLENGE_COMPLETED;
            Debug.Log("Challenge Success");
        }
    }

    public void challengeFailed() {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_CHALLENGE_COMPLETED) {
            theStateManager.challengeStatus = false;
            theStateManager.currentPhase = StateManager.TurnPhase.CHALLENGE_COMPLETED;
            Debug.Log("Challenge Failed");
        }
    }
}
