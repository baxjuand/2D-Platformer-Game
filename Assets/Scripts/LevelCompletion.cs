using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Level Clear Action
            Debug.Log("Level Cleared");
        }
    }
}
