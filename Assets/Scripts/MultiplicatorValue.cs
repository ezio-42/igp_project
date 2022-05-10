using TMPro;
using UnityEngine;

public class MultiplicatorValue : MonoBehaviour {
    public int value = 1;

    private void Start() {
        gameObject.GetComponentInChildren<TMP_Text>().SetText("x" + value);
    }
}