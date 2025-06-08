// ClubData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "ClubData", menuName = "ScriptableObjects/ClubData", order = 1)]
public class ClubData : ScriptableObject
{
    public string clubName;
    [TextArea]
    public string shortInfo;
    public string location;
    public string contidition;
    public string time;
    [TextArea]
    public string description;
    public string activiry;
    public string[] tags;
    public string applyLink;
    public bool isRecruiting;
}
