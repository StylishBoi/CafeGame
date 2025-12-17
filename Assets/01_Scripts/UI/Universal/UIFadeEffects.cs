using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeEffects : MonoBehaviour
{
    [Header("Effects Durations")] [SerializeField]
    private float flashDuration = 1f;

    public float fadeOutDuration = 1f;
    public float fadeInDuration = 1f;

    private TextMeshProUGUI TemporaryVariableMaker(TextMeshProUGUI textSample)
    {
        TextMeshProUGUI tempText = Instantiate(textSample, textSample.transform.parent);
        tempText.gameObject.SetActive(true);
        return tempText;
    }

    private Image TemporaryVariableMaker(Image imageSample, Color colorSample)
    {
        Image tempImage = Instantiate(imageSample, imageSample.transform.parent);

        tempImage.gameObject.SetActive(true);
        return tempImage;
    }

    public IEnumerator ImageFlashEvent(Image fadeImage, Color fadeColor)
    {
        var tempImage = TemporaryVariableMaker(fadeImage, fadeColor);
        tempImage.color = fadeColor;

        Color startcolor = tempImage.color;
        Color endcolor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 0);

        float t = 0.0f;

        while (tempImage.color.a > 0)
        {
            tempImage.color = Color.Lerp(startcolor, endcolor, t);
            if (t < 1)
            {
                t += Time.deltaTime / flashDuration;
            }

            yield return null;
        }

        Destroy(tempImage.gameObject);
    }

    public IEnumerator ImageFlashEvent(Image fadeImage, Color fadeColor, GameObject flashObject)
    {
        var tempImage = TemporaryVariableMaker(fadeImage, fadeColor);
        flashObject.SetActive(true);
        tempImage.color = fadeColor;

        Color startcolor = tempImage.color;
        Color endcolor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 0);

        float t = 0.0f;

        while (tempImage.color.a > 0)
        {
            tempImage.color = Color.Lerp(startcolor, endcolor, t);
            if (t < 1)
            {
                t += Time.deltaTime / (flashDuration * 2);
            }

            yield return null;
        }

        flashObject.SetActive(false);
        Destroy(tempImage.gameObject);
    }

    public IEnumerator DoFadeOut(Image fadeImage, Color fadeColor)
    {
        Image tempImage = TemporaryVariableMaker(fadeImage, fadeColor);

        Color startColor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 0);;
        Color endColor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 1); // fully opaque

        float t = 0f;

        while (t < 1f)
        {
            tempImage.color = Color.Lerp(startColor, endColor, t);
            t += Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        Destroy(tempImage.gameObject);
    }

    public IEnumerator DoFadeIn(Image fadeImage, Color fadeColor)
    {
        var tempImage = TemporaryVariableMaker(fadeImage, fadeColor);

        Color startcolor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 1);;
        Color endcolor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 0);

        float t = 0.0f;

        while (tempImage.color.a > 0)
        {
            tempImage.color = Color.Lerp(startcolor, endcolor, t);
            if (t < 1)
            {
                t += Time.deltaTime / fadeInDuration;
            }

            yield return null;
        }

        Destroy(tempImage.gameObject);
        yield return null;
    }

    public IEnumerator DoTextFadeMoveDown(TextMeshProUGUI fadeText)
    {
        var tempText = TemporaryVariableMaker(fadeText);
        
        float t = 0.0f;
        Color startcolor = tempText.color;
        Color endcolor = new Color(tempText.color.r, tempText.color.g, tempText.color.b, 0);

        while (tempText.color.a > 0)
        {
            tempText.rectTransform.anchoredPosition += new Vector2(0f, -25f) * Time.deltaTime;
            tempText.color = Color.Lerp(startcolor, endcolor, t);
            
            if (t < 1)
            {
                t += Time.deltaTime / fadeOutDuration;
            }

            yield return null;
        }

        Destroy(tempText.gameObject);
        yield return null;
    }
}