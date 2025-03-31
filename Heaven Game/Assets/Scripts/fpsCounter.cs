using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class fpsCounter : MonoBehaviour
{

    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }

    void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(calculateFPS()).ToString();
    }

    private float calculateFPS()
    {
        float total = 0f;
        foreach(float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }
}
