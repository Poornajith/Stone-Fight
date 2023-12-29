using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomNameLogic : MonoBehaviour
{
    [SerializeField] GameObject roomNameInUI;
    [SerializeField] TMP_InputField roomNameInput;

    public void SubmitNewRoomName()
    {
        FusionConnection.instance.CreateSessionWithNewName(roomNameInput.text);
        roomNameInUI.SetActive(false);
    }

    public void SubmitExistingRoomName()
    {
        FusionConnection.instance.ConnectToSession(roomNameInput.text);
        roomNameInUI.SetActive(false);
    }
}
