using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionMarker : MonoBehaviour
{
    [SerializeField]
    private GameObject NextTarget;
    [SerializeField]
    private string Text;
    [SerializeField]
    private string Name = "Info";
    [SerializeField]
    private bool FinalCheckPoint;
    [SerializeField]
    private string FinalScene = "Success";
    [SerializeField]
    private int Delay = 10;
    [SerializeField]
    private GameObject ActivateObject;
    [SerializeField]
    private string NewObjective;

    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private float fadeDuration = 2f;

    private MissionWaypoint _waypoint;

    private void Start()
    {
        _waypoint = GameObject.FindGameObjectWithTag("Player").GetComponent<MissionWaypoint>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }

        Dialog dg = new Dialog(Name, Text);
        DialogSystem.ShowDialog(dg, 30000, ActivateObject);

        if(NextTarget != null)
            _waypoint.target = NextTarget.transform;

        if (FinalCheckPoint)
        {
            StartCoroutine(WaitAndLoadScene());
            return;
        }

        ObjectiveController.Instance?.OnObjectiveChangeTrigger(NewObjective);
        Destroy(gameObject);
    }

    IEnumerator WaitAndLoadScene()
    {

        yield return new WaitForSeconds(Delay);

        // Start fading to black
        yield return StartCoroutine(FadeToBlack());

        // Load the new scene
        SceneManager.LoadScene(FinalScene);
    }

    private IEnumerator FadeToBlack()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeDuration); // Fade alpha from 0 to 1
            fadeImage.color = color;
            yield return null; // Wait for the next frame
        }
    }
}
