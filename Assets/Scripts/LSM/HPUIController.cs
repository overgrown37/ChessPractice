using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HPUIController : MonoBehaviour
{
    [SerializeField] private GameObject Segment;
    [SerializeField] private List<GameObject> Segments;

    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        for(int i = 0; i < maxHealth; i++)
        {
            var currentSegment = Instantiate(Segment);
            currentSegment.transform.SetParent(this.gameObject.transform);
            Segments.Add(currentSegment);
        }
        for (int i = 0; i <= maxHealth - currentHealth; i++)
        {
            Segments.Last().GetComponent<Image>().color = Color.black;
        }
    }

    public void SetHealth(int health)
    {
        health = maxHealth;
    }
}
