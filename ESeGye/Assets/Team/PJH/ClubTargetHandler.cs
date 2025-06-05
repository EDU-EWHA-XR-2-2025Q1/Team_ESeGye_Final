using UnityEngine;
using Vuforia;

public class ClubTargetHandler : MonoBehaviour
{
    public ClubData clubData;
    private bool hasScanned = false;

    void Start()
    {
        var handler = GetComponent<DefaultObserverEventHandler>();
        if (handler != null)
        {
            handler.OnTargetFound.AddListener(OnTargetFound);
        }
    }

    void OnTargetFound()
    {
        if (hasScanned) return;
        hasScanned = true;

        var manager = FindObjectOfType<ScannedClubManager>();
        if (manager != null)
        {
            manager.OnScanClub(clubData);
        }
    }
}
