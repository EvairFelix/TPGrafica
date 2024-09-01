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

    private Dictionary<GameObject, float> foodStartTime = new Dictionary<GameObject, float>(); // Dicionário para armazenar o tempo de início do alimento

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

        // Verifica se algum alimento está pronto após 2 segundos
        foreach (var entry in foodStartTime)
        {
            if (Time.time - entry.Value >= 2f)
            {
                Debug.Log($"{entry.Key.name} está pronto!");
                // Opcional: Remova o alimento do dicionário após o aviso
                foodStartTime.Remove(entry.Key);
                break; // É importante sair do loop para evitar erros de modificação do dicionário durante a iteração
            }
        }
    }

    public void StartCooking(float time)
    {
        cookingTime = time;
        isCooking = true;
    }

    public void AddFoodToPan(GameObject foodItem, GameObject pan)
    {
        Transform foodPosition = pan.transform.Find("FoodPosition");

        if (foodPosition != null)
        {
            foodItem.transform.position = foodPosition.position;
            foodItem.transform.SetParent(foodPosition); // Torna o "FoodPosition" o pai do alimento
            foodStartTime[foodItem] = Time.time; // Registra o tempo de adição do alimento

            Debug.Log($"Adicionou {foodItem.name} à panela {pan.name} na posição {foodPosition.position}");
        }
        else
        {
            Debug.LogError($"O GameObject 'FoodPosition' não encontrado na panela {pan.name}.");
        }
    }

    public void AddFoodToFryPan(GameObject foodItem, GameObject fryingPan)
    {
        Transform foodPosition = fryingPan.transform.Find("FoodPosition");

        if (foodPosition != null)
        {
            foodItem.transform.position = foodPosition.position;
            foodItem.transform.SetParent(foodPosition); // Torna a frigideira o pai do alimento
            foodStartTime[foodItem] = Time.time; // Registra o tempo de adição do alimento

            Debug.Log($"Adicionou {foodItem.name} à frigideira {fryingPan.name} na posição {foodPosition.position}");
        }
        else
        {
            Debug.LogError($"O GameObject 'FoodPosition' não encontrado na frigideira {fryingPan.name}.");
        }
    }

    public void RemoveFoodFromPan(GameObject pan)
    {
        Transform foodPosition = pan.transform.Find("FoodPosition");

        if (foodPosition != null)
        {
            if (foodPosition.childCount > 0)
            {
                GameObject foodItem = foodPosition.GetChild(0).gameObject;
                foodItem.transform.SetParent(null); // Remove o alimento da panela
                foodItem.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2; // Move o alimento para frente da câmera

                Debug.Log($"Removeu {foodItem.name} da panela {pan.name}");
                // Remove o alimento do dicionário
                foodStartTime.Remove(foodItem);
            }
            else
            {
                Debug.Log("Não há alimento na panela para remover.");
            }
        }
        else
        {
            Debug.LogError($"O GameObject 'FoodPosition' não encontrado na panela {pan.name}.");
        }
    }

    public void RemoveFoodFromFryPan(GameObject fryingPan)
    {
        Transform foodPosition = fryingPan.transform.Find("FoodPosition");

        if (foodPosition != null)
        {
            if (foodPosition.childCount > 0)
            {
                GameObject foodItem = foodPosition.GetChild(0).gameObject;
                foodItem.transform.SetParent(null); // Remove o alimento da frigideira
                foodItem.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2; // Move o alimento para frente da câmera

                Debug.Log($"Removeu {foodItem.name} da frigideira {fryingPan.name}");
                // Remove o alimento do dicionário
                foodStartTime.Remove(foodItem);
            }
            else
            {
                Debug.Log("Não há alimento na frigideira para remover.");
            }
        }
        else
        {
            Debug.LogError($"O GameObject 'FoodPosition' não encontrado na frigideira {fryingPan.name}.");
        }
    }
}
