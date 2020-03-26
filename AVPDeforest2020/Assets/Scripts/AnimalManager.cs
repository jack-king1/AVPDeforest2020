using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager instance;

    public List<GameObject> animalList;
    bool animalsAdded = false;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {

    }

    public void RemoveAllAnimals()
    {
        if(animalList != null)
        {
            foreach (var go in animalList)
            {
                go.GetComponent<ShrinkAnimals>().Shrink();
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            RemoveAllAnimals();
        }

        if(!animalsAdded)
        {
            animalsAdded = true;
            foreach (var trans in GetComponentsInChildren<Transform>())
            {
                if (trans.CompareTag("Animal"))
                {
                    animalList.Add(trans.gameObject);
                }
            }
        }
    }
}
