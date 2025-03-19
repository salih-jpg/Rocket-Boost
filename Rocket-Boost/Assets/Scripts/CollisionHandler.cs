using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("it is friendly");
                break;
            case "Fuel":
                Debug.Log("it is fuel");
                break;
            case "Finish":
                Debug.Log("it is finish");
                break;
        }
    }

}
