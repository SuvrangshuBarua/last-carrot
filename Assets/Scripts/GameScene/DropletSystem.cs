using UnityEngine;

public class DropletSystem : MonoBehaviour
{
    public int dropletValue;

    void OnMouseDown()
    {
        GameObject.FindObjectOfType<GameManager>().AddDroplet(dropletValue);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnvironmentObstacles"))
        {
            Destroy(this.gameObject);
        }
    }

}
