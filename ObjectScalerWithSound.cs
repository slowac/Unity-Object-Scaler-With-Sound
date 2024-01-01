using System.Linq;
using UnityEngine;
//Comment your tips and i will post them with credits.

public class ObjectScalerWithSound : MonoBehaviour
{
    public AudioSource audioFactor;
    public float objectScale = 1.0f; //Default value is 1.0f, you can find the best value for you by trying different values in your project.
    public int dataLength = 1024;
    private Vector3 _Scale;
    private float[] audioData;

    private void Start()
    {
        if (audioFactor == null)
        {
            audioFactor = GetComponent<AudioSource>();
        }

        audioData = new float[dataLength];
        _Scale = transform.localScale;
    }

    private void Update()         // Scale the object based on sound frequency
    {
        audioFactor.GetSpectrumData(audioData, 0, FFTWindow.BlackmanHarris);
        var currentAverage = CalcAvgF();

        var scale = currentAverage * objectScale;
        transform.localScale = new Vector3(_Scale.x + scale, _Scale.y + scale, _Scale.z + scale);
    }

    private float CalcAvgF() //Calculates the average of audio spectrum data and returns this value.
    {
        var sum = audioData.Sum();
        return sum / dataLength;
    }
}