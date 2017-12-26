using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EventButtonDetails {
    public string buttonTitle;
    public UnityAction action;
}

public class ModalPanelDetails {
    public string title; // Not Implemented
    public string text;
    public EventButtonDetails button1Details;
    public EventButtonDetails button2Details;
    public bool dropdown;
}

public class ModalPanel : MonoBehaviour {
    public Text text;
    public Button button1;
    public Button button2;
    public Dropdown dropdown;

    public Text button1Text;
    public Text button2Text;

    public GameObject modalPanelObject;

    private static ModalPanel modalPanel;

    public static ModalPanel Instance () {
        if (!modalPanel) {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel) {
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
            }
        }

        return modalPanel;
    }

    public void TextWindow (ModalPanelDetails details) {
        modalPanelObject.SetActive(true);

        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        dropdown.gameObject.SetActive(false);

        this.text.text = details.text;

        if (details.dropdown) {
            dropdown.gameObject.SetActive(true);
        }

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(details.button1Details.action);
        if (details.dropdown) {
            button1.onClick.AddListener(ResetDropdownValue);
        }
        button1.onClick.AddListener(ClosePanel);
        button1Text.text = details.button1Details.buttonTitle;
        button1.gameObject.SetActive(true);

        if (details.button2Details != null) {
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(details.button2Details.action);
            button2.onClick.AddListener(ClosePanel);
            button2Text.text = details.button2Details.buttonTitle;
            button2.gameObject.SetActive(true);
        }
    }

    void ClosePanel () {
        modalPanelObject.SetActive(false);
    }

    void ResetDropdownValue () {
        dropdown.value = 0;
    }
}
