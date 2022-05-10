using UnityEngine;

public class HorizontalMover : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private Transform cachedTransform;
    [SerializeField] private float moveFromCenter = 2f;
    [SerializeField] private Player player;

    private void Awake() {
        if (cachedTransform == null) cachedTransform = transform;
    }


    public void Update() {
        if (player.gameState == GameState.Started) MoveHorizontally(slider.GetHorizontalPosition());
    }

    private void MoveHorizontally(float position) {
        position = Mathf.Clamp(position, -1, 1);
        var nextPos = cachedTransform.localPosition;
        nextPos.x = position * moveFromCenter;
        cachedTransform.localPosition = nextPos;
    }
}