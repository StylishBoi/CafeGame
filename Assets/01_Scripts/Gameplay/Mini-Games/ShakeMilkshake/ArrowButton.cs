using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public SpriteRenderer backdrop;
    
    void Start()
    {
        backdrop=GetComponent<SpriteRenderer>();
    }
    
    private void OnDisable()
    {
        backdrop.color = new Color(1f, 1f, 1f, 1f);
    }

    public void GoodClicked()
    {
        backdrop.color = new Color32(100, 255, 50, 255);
    }
    
    public void BadClicked()
    {
        backdrop.color = new Color32(255, 0, 0, 255);
    }
}
