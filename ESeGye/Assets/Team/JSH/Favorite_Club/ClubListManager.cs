using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClubListManager : MonoBehaviour
{
    [Header("Prefab & Container")]
    [SerializeField] private GameObject clubItemPrefab; // Assign the prefab in Inspector
    [SerializeField] private Transform contentTransform; // ScrollView/Viewport/Content

    /// Call this to dynamically add a club item.
    public void AddClubItem(string title, string description, Sprite image)
    {
        GameObject newItem = Instantiate(clubItemPrefab, contentTransform);

        // Set title
        TMP_Text titleText = newItem.transform.Find("TextGroup/TitleText").GetComponent<TMP_Text>();
        titleText.text = title;

        // Set description
        TMP_Text descText = newItem.transform.Find("TextGroup/DescText").GetComponent<TMP_Text>();
        descText.text = description;

        // Set image
        Image icon = newItem.transform.Find("Image").GetComponent<Image>();
        icon.sprite = image;

        // Hook up delete button
        Button deleteButton = newItem.transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(() => Destroy(newItem));
    }


    /// Sample call (can be used with UI button)
    public void OnClick_AddSampleClub()
    {
        Sprite dummyIcon = Resources.Load<Sprite>("ClubIcons/sample_icon");
        AddClubItem("동아리 A", "동아리 A의 설명", dummyIcon);
    }
}
