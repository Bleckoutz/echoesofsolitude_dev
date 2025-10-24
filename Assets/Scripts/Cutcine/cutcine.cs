using UnityEngine;
using TMPro;
using System.Collections;

public class SimpleCutscene : MonoBehaviour
{
    public GameObject cutscenePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 10)]
    public string[] sentences;
    public float typingSpeed = 0.03f;

    private int index = 0;

    void Start()
    {
        cutscenePanel.SetActive(true);
        dialogueText.text = "";

        // pausa o tempo do jogo
        Time.timeScale = 0f;

        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        while (index < sentences.Length)
        {
            yield return StartCoroutine(TypeSentence(sentences[index]));

            // espera input do jogador
            yield return new WaitUntil(() =>
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.Return) ||
                Input.GetMouseButtonDown(0)
            );

            index++;
            dialogueText.text = ""; // limpa a linha antiga
        }

        // termina cutscene
        cutscenePanel.SetActive(false);
        Time.timeScale = 1f; // volta o tempo normal
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed); // <- importante para Time.timeScale = 0
        }
    }
}
