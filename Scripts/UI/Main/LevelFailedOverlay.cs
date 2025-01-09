using UnityEngine;

public class LevelFailedOverlay : MainOverlay
{
    float counter;
    public override void OnAppeared()
    {
        base.OnAppeared();
        counter = 4f;
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
