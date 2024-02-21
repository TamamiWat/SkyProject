using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowkeysDisplayer : MonoBehaviour
{
    [SerializeField] Image[] images;
    [SerializeField, Range(0f, 5f)] private float interval = 2.0f;
    private int currentIndex = 0;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }

        if(images.Length > 0)
        {
            images[currentIndex].gameObject.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= interval)
        {
            images[currentIndex].gameObject.SetActive(false);
            currentIndex = (currentIndex + 1) % images.Length;
            images[currentIndex].gameObject.SetActive(true);
            timer = 0f;
        }
        
    }
}
