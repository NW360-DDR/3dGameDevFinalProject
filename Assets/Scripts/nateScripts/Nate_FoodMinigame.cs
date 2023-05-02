using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nate_FoodMinigame : MonoBehaviour
{
    /* Food Minigame (Rough Draft)
     * In this incarnation, regardless of the ingredients, the target will randomly pick between 0.5 to 0.8 of the
     * bar to initialize, rather than pick a range based on the intended recipe. That is a stretch goal.
     * 
     * NOTE: Not a stretch goal, ironically easier than the rest of the minigame was to get right lmao
     */

    [Header("Objects")]
    [SerializeField]GameObject target;
    [SerializeField]GameObject cursor;
    // [SerializeField]Inventory Manager Inven;
    // We're not actually doing this since I haven't implemented it yet.
    // The intention is for the minigame script to handle all of the food creation.

    [Header("Minigame Settings")]
    [SerializeField] int cursorSpeed;
    [SerializeField] Nate_RecipeManager Manager; // This really is just a list of every Recipe and compares our ingredients for us. Hella inefficient I know.
    [SerializeField]int minCook = 50; //Placeholder number
    [SerializeField]int maxCook = 80; //Placeholder number
    bool miniStarted = false;
    bool cookFinish = false;
    int clearWindow = 75; // it's halved for +/-
    [SerializeField] string[] temp;

    // Keeping just in case I need any Start reference stuff.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !miniStarted) // If we haven't started, start it!
        {
            ProperInit(temp);
            miniStarted = true;
        }
        else if (miniStarted && !cookFinish) // If we've started, we have a few checks to look for.
        {
            cursor.transform.Translate( Vector3.right * cursorSpeed * Time.deltaTime);

            // Also, don't forgor to auto-fail if we fuck up and wait TOO long
            if (cursor.GetComponent<RectTransform>().anchoredPosition.x > 750)
            {
                Debug.Log("Congrats, you burned it you fool!");
                cursor.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                miniStarted = false;
                cookFinish = false;
            }

            if (Input.GetKeyDown("space"))
            {
                cookFinish = true;
                // Now to calculate if the Minigame is successful or not.
                if (Mathf.Abs(cursor.transform.position.x - target.transform.position.x) < clearWindow)
                {
                    Debug.Log("You made the thing!");
                }
                else
                {
                    Debug.Log("THIS IS RAW! R A W RAW! GET OUT OF MY KITCHEN!");
                }
                // Then we reset our cursor.
                cursor.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                miniStarted= false;
                cookFinish= false;
            }
        }
    }

    Vector3 CalcPos(int min, int max)
    {
        RectTransform rect = GetComponent<RectTransform>();
        // the /100 is so it actually calculates it as a percentage
        float randMin = rect.rect.width * ((float)min / 100);
        float randMax = rect.rect.width * ((float)max / 100);
        Vector3 temp = new Vector3(Random.Range(randMin, randMax), 0, 0); //Sets to middle of screen (for now)
        return temp;
    }

    void ProperInit(string[] ingredients)
    {
        // TODO: When Ingredients and Recipes are fully implemented, grabs the info here and populates the data here.
        Nate_RecipeTemplate test = Manager.RecipeCheck(ingredients);
        minCook = test.minCook;
        maxCook = test.maxCook;
        target.GetComponent<RectTransform>().anchoredPosition = CalcPos(minCook, maxCook);
        Debug.Log(test.name);
    }

}

