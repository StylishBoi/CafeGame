using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public SpriteRenderer backdrop;
    
    void Start()
    {
        backdrop=GetComponent<SpriteRenderer>();
    }

    public void Clicked()
    {
        backdrop.color = new Color32(100, 255, 50, 255);
    }
}
