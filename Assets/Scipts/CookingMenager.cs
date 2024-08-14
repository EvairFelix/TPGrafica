using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CookingManager : MonoBehaviour
{
    public GameObject fryingPan;
    public GameObject pot;
    public GameObject egg;
    public GameObject meat;
    public GameObject pasta;
    public GameObject water;
    public GameObject oil;

    private float cookingTime = 0f;
    private bool isCooking = false;

    void Update()
    {
        if (isCooking)
        {
            cookingTime -= Time.deltaTime;
            if (cookingTime <= 0f)
            {
                isCooking = false;
                Debug.Log("Food is ready!");
                // Handle food completion logic
            }
        }
    }

    public void StartCooking(float time)
    {
        cookingTime = time;
        isCooking = true;
    }

    // Método para adicionar alimento à panela
    public void AddFoodToPan(GameObject foodItem, GameObject pan)
    {
        Transform foodPosition = pan.transform.Find("FoodPosition"); // Encontre o GameObject "FoodPosition" dentro da panela
        
        if (foodPosition != null)
        {
            // Mova o alimento para a posição dentro da panela
            foodItem.transform.position = foodPosition.position;
            foodItem.transform.parent = pan.transform; // Torna a panela o pai do alimento
            Debug.Log($"Adicionou {foodItem.name} à panela {pan.name} na posição {foodPosition.position}");
        }
        else
        {
            Debug.LogError("O GameObject 'FoodPosition' não encontrado na panela.");
        }
    }

}