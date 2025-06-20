using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridCardController : MonoBehaviour
{
    [Header("Card Setup")]
    public List<GameObject> cardPrefabs = new List<GameObject>();
    private List<GameObject> spawnedCards = new List<GameObject>();
    
    [Header("Grid Settings")]
    public int rows = 5;
    public int cols = 5;
    public float spacing = 1.5f;
    
    [Header("Game Settings")]
    public float flipDelay = 5;
    public GameObject cardSelected;
    public Transform cardToFindTransform;
    
    public void RunMemoryGame()
    {
        
        SpawnGrid();
    }

    void SpawnGrid()
    {
        if (cardPrefabs.Count < rows * cols)
        {
            Debug.LogError("Not enough unique card prefabs to fill the grid!");
            return;
        }
        
        List<GameObject> shuffledPrefabs = new List<GameObject>(cardPrefabs);
        Shuffle(shuffledPrefabs);

        int index = 0;
        
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector3 position = new Vector3(x * spacing, 0f, -y * spacing);
                GameObject card = Instantiate(shuffledPrefabs[index], position, Quaternion.identity, transform);
                card.name = $"Card_{x}_{y}";
                CardController cardController = card.GetComponent<CardController>();
                cardController.FaceUp();
                cardController.canFlip = false;
                card.GetComponent<CardData>().index = index;
                spawnedCards.Add(card);
                index++;
            }
        }
        StartCoroutine(FlipAllCardsDownAfterDelay());
    }

    private IEnumerator FlipAllCardsDownAfterDelay()
    {
        yield return new WaitForSeconds(flipDelay);
        foreach (GameObject card in spawnedCards)
        {
            CardController cardCon = card.GetComponent<CardController>();
            cardCon.FaceDown();
            cardCon.canFlip = true;
        }

        SetCardToFind();
    }

    private void SetCardToFind()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnedCards.Count);
        GameObject targetCard = spawnedCards[randomIndex];
        
        cardSelected = Instantiate(
            targetCard,
            cardToFindTransform.position,
            Quaternion.identity
        );
        
        cardSelected.GetComponent<CardController>().FaceUp();
    }
    
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public void CardSelected(GameObject card)
    {
        CardController cardController = card.GetComponent<CardController>();
        cardController.Flip(true);
        foreach (GameObject spawnedCard in spawnedCards)
        {
            spawnedCard.GetComponent<CardController>().canFlip = false;
        }
    }
}