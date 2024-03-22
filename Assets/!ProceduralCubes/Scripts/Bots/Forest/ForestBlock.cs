using UnityEngine;

public class ForestBlock : MonoBehaviour
{
    public GameObject[] OptionalDecoration;
    
    public void SetTree(int lowerTreeChance = -3)
    {
        int randomIndex = Random.Range(lowerTreeChance, OptionalDecoration.Length);
        if (randomIndex < 0) return; // no tree
        OptionalDecoration[randomIndex].SetActive(true);
    }
}
