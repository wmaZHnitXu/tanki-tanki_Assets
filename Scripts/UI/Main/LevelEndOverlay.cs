using UnityEngine;

public class LevelEndOverlay : MainOverlay
{
    float counter;
    public override void OnAppear()
    {
        base.OnAppear();
        counter = 2f;
    }

    void Update() {
        if (counter > 0.0f) {
            counter -= Time.deltaTime;
            if (counter <= 0.0f) {
                Disappear();
            }
        }
    }
}
