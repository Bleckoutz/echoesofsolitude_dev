using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;

public class SimpleCutscene : MonoBehaviour
{
    public GameObject cutscenePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 10)]
    public string[] sentences;
    public float typingSpeed = 0.03f;

    private int index = 0;
    public TextMeshProUGUI Textpule; // opcional para mostrar que F pula

    void Start()
    {
        cutscenePanel.SetActive(true);
        dialogueText.text = "";
        Textpule.text = "Press F to skip"; // opcional

        // pausa o tempo do jogo
        Time.timeScale = 0f;

        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        while (index < sentences.Length)
        {
            yield return StartCoroutine(TypeSentence(sentences[index]));
            yield return new WaitForSecondsRealtime(0.5f); // pequena pausa após a frase ser digitada

            // espera input do jogador
            yield return new WaitUntil(() =>
                Input.GetKeyDown(KeyCode.Mouse0) ||
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetMouseButtonDown(0)
            );

            index++;
            dialogueText.text = ""; // limpa a linha antiga
            Textpule.text = "Press F to skip"; // opcional
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

    void Update()
    {
        // opcional: permitir pular cutscene
        if (Input.GetKeyDown(KeyCode.F))
        {
            StopAllCoroutines();
            cutscenePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
