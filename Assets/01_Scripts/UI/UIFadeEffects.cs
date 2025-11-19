using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeEffects : MonoBehaviour
{
    [Header("Effects Durations")] [SerializeField]
    private float flashDuration = 1f;

    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float fadeInDuration = 1f;

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

        Destroy(tempImage);
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
        Destroy(tempImage);
    }

    public IEnumerator DoFadeOut(Image fadeImage, Color fadeColor)
    {
        var tempImage = TemporaryVariableMaker(fadeImage, fadeColor);

        Color startcolor = tempImage.color;
        Color endcolor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 1);

        float t = 0.0f;

        while (tempImage.color.a > 0)
        {
            tempImage.color = Color.Lerp(startcolor, endcolor, t);
            if (t < 1)
            {
                t += Time.deltaTime / fadeOutDuration;
            }

            yield return null;
        }

        Destroy(tempImage);
        yield return null;
    }

    public IEnumerator DoFadeIn(Image fadeImage, Color fadeColor)
    {
        var tempImage = TemporaryVariableMaker(fadeImage, fadeColor);

        Color startcolor = tempImage.color;
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

        Destroy(tempImage);
        yield return null;
    }
}