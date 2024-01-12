using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public struct Character
{
    public Sprite sprite;
    public string name;

    /*
    //Constructor (not necessary, but helpful)
    public InventorySlot(bool isFree, string name)
    {
        this.IsFree = isFree;
        this.Name = name;
    }
    */
}
public class CharacterSelect : MonoBehaviour
{
    public Sprite[] characters;
    public Image selectedCharacter;
    private int selectedCharacterIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(InputAction.CallbackContext context)
    {
        Debug.Log("click. context:" + context);
    }
    public void Navigate(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            float xVal = context.ReadValue<Vector2>().x;

            if (xVal > 0)
            {
                selectedCharacterIndex++;
            }
            if (xVal < 0)
            {
                selectedCharacterIndex--;
            }

            if (selectedCharacterIndex < 0)
            {
                selectedCharacterIndex = characters.Length - 1;
            }

            if (selectedCharacterIndex == characters.Length)
            {
                selectedCharacterIndex = 0;
            }

            selectedCharacter.sprite = characters[selectedCharacterIndex];
        }
    }

}
