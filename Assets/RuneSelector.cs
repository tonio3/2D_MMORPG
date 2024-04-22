using UnityEngine;
using UnityEngine.UI;

public enum RuneType {None, LethalTempo, Conqueror };

public class RuneSelection : MonoBehaviour
{
    [SerializeField] Button lethalTempoButton;
    [SerializeField] Button conquerorButton;

    [SerializeField] RuneType selectedRune = RuneType.LethalTempo;

    private void Start()
    {
        lethalTempoButton.onClick.AddListener(SelectLethalTempo);
        conquerorButton.onClick.AddListener(SelectConqueror);

    }
 
    private void SelectLethalTempo()
    {
        selectedRune = RuneType.LethalTempo;
        Player.Instance.SetRune(selectedRune);

        lethalTempoButton.image.color = Color.white;
        conquerorButton.image.color = Color.gray;
    }

    private void SelectConqueror()
    {
        selectedRune = RuneType.Conqueror;
        Player.Instance.SetRune(selectedRune);
        lethalTempoButton.image.color = Color.gray;
        conquerorButton.image.color = Color.white;
    }
}
