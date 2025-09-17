using UnityEngine;
using UnityEngine.UI;

public class ClothingManager : MonoBehaviour
{
    [SerializeField] private ItemSO[] itemSOs;
    [SerializeField] private GameObject[] containers;

    //private GameObject[] item;

    void Start()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            Transform item = containers[i].transform.GetChild(0);
            item.GetComponent<Image>().sprite = itemSOs[i].image;
            
        }
    }
}
