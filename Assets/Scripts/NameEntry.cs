using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameEntry : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject playerNameInUI;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Button submitButton;

    public void SubmitName()
    {
        FusionConnection.instance.ConnectToLobby(nameInput.text);
        playerNameInUI.SetActive(false);
    }

    public void ActivateButton()
    {
        submitButton.interactable = true;
    }

}
