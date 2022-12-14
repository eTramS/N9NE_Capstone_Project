using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class CharacterManager : MonoBehaviour
{

    public List<CharacterData> charList = new List<CharacterData>();
    public Transform characterselectionContent;
    public GameObject characterItem;
    public CharSlot charSlot;

    private void Start()
    {

        ListCharacters();

    }
    public void ListCharacters()
    {
        foreach (var item in charList)
        {
            charSlot._id = item.id;
            charSlot.CharacterPickingSFX = item.VoicePicking;
            charSlot.characterName = item.CharacterName;
            charSlot.characterDetails = item.CharacterDetails;
            GameObject gameObject = Instantiate(characterItem, characterselectionContent);


            var characterName = gameObject.transform.Find("charName").GetComponent<TextMeshProUGUI>();
            var characterID = gameObject.transform.Find("charID").GetComponent<TextMeshProUGUI>();
            var characterIcon = gameObject.transform.Find("charIco").GetComponent<Image>();
            var characterUnlock = gameObject.GetComponent<Button>();

            characterName.text = item.CharacterName;
            characterIcon.sprite = item.CharacterIcon;
            characterID.text = item.id;


            if (item.isUnlocked == true) { characterUnlock.interactable = true; }
            else { characterUnlock.interactable = false; }




        }



    }
}
