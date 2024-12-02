using UnityEngine;

public class TestScr : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Dialog dg = new Dialog("Battery", "JFJF");
        DialogSystem.ShowDialog(dg, 10);
    }
}
