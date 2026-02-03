using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;
    private float startPosition, lenght;



    void Awake()
    {
        startPosition = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x; 
    }

    void Update()
    {
        MoveBackground();
        InfiniteScroll();
    }

    private void MoveBackground()
    {
        
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
    }
    
    private void InfiniteScroll()
    {
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        if (movement > startPosition + lenght)
        {
            startPosition += lenght;
        }
        else if (movement < startPosition - lenght)
        {
            startPosition -= lenght;
        }
    }
}
