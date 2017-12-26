using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumber : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        modalPanel = ModalPanel.Instance();
    }

    private StateManager theStateManager;
    public Transform[] player;
    public Dropdown dropdown;
    private ModalPanel modalPanel;

    public void PlayerNumberWindow() {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {
            text = "Please choose the number of players:"
        };
        modalPanelDetails.dropdown = true;
        modalPanelDetails.button1Details = new EventButtonDetails {
            buttonTitle = "Play",
            action = instantiatePlayers
        };

        modalPanel.TextWindow(modalPanelDetails);
    }

    void instantiatePlayers() {
        if (theStateManager.currentPhase == StateManager.TurnPhase.WAITING_FOR_PLAYER_NUMBER) {
            int n = dropdown.value + 1;
            Debug.Log(n + " Player(s) joined.");
            if (n > 0 && n <= 12) {
                for (int i = n - 1; i >= 0; i--) {
                    // Instantiate players
                    Instantiate(player[i], new Vector3(0, 0, 0), Quaternion.identity);
                }
            }
            theStateManager.currentPhase = StateManager.TurnPhase.DECIDE_PLAYER_NUMBER;
        }
    }
}
