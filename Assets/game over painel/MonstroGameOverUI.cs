using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonstroGameOverUI : MonoBehaviour
{
    public Sprite[] surgindo;   // frames da animação de surgimento
    public Sprite[] idle;       // frames do idle em loop
    public float frameRate = 0.15f;

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(PlayAnim(surgindo, false, () =>
        {
            StartCoroutine(PlayAnim(idle, true));
        }));
    }

    IEnumerator PlayAnim(Sprite[] frames, bool loop, System.Action onFinish = null)
    {
        do
        {
            for (int i = 0; i < frames.Length; i++)
            {
                img.sprite = frames[i];
                yield return new WaitForSecondsRealtime(frameRate);
            }
        } while (loop);

        onFinish?.Invoke();
    }
}
