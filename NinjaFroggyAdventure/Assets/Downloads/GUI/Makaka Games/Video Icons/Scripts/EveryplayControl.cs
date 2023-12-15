using UnityEngine;
using UnityEngine.UI;

public class EveryplayControl : MonoBehaviour 
{
	private bool isEveryplayPanelShow = false;

	public bool isDebugLogOn = true;
	
	public CanvasGroup everyplayPanelCanvasGroup;

	public Toggle ToogleRecord;

	public Button ButtonWatchReplays;

	public Button ButtonFaceCam;

	public Button ButtonShare;
	
	private void DebugLog(string message)
	{
		if (isDebugLogOn) 
		{
			Debug.Log(message);
		}
	}

	private void Start()
	{
		DebugLog("Everplay Control Start()!");
	}

	private void OnDestroy()
	{
		DebugLog("Everyplay Destroy!");
	}

	public void ShowEveryplayPanel()
	{
		isEveryplayPanelShow = !isEveryplayPanelShow;

		everyplayPanelCanvasGroup.interactable = isEveryplayPanelShow;
		everyplayPanelCanvasGroup.blocksRaycasts = isEveryplayPanelShow;

        if (isEveryplayPanelShow)
        {
			DebugLog("Show Everyplay Panel!");

			everyplayPanelCanvasGroup.alpha = 1f;
		}
        else
        {
			DebugLog("Hide Everyplay Panel!");

			everyplayPanelCanvasGroup.alpha = 0f;
		}		
	}

	public void RecordToggle()
	{
		DebugLog("Record Toggle!");

		if (ToogleRecord.isOn)
		{
			StartRecording();
		}
		else
		{
			StopRecording();
		}
	}

	public void StartRecording()
	{
		DebugLog("StartRecording!");

		if (ButtonShare != null)
		{
			ButtonShare.interactable = false;
		}

		if (ButtonFaceCam != null)
		{
			ButtonFaceCam.interactable = false;
		}
	}

	public void StopRecording()
	{
		DebugLog("StopRecording!");

		if (ButtonShare != null)
		{
			ButtonShare.interactable = true;
		}

		if (ButtonFaceCam != null)
		{
			ButtonFaceCam.interactable = true;
		}
	}
	
	public void FaceCamToggle()
	{
		DebugLog("Face Cam Toggle!");
	}
	
	public void OpenEveryplay()
	{
		DebugLog("Everplay Show!");
	}
	
	public void ShareVideo()
	{
		DebugLog("Share Video!");
	}

}