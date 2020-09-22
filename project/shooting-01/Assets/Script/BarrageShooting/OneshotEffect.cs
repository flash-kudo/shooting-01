using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OneshotEffect : MonoBehaviour
{
    public GameObject EffectObject;
    public PlayableDirector Director;

    public void ShowEffect()
    {
        EffectObject.SetActive(true);
        Director.time = 0;
        Director.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if ((EffectObject.activeSelf == true) && (Director.state == PlayState.Paused)) RemoveAction(Director);
    }

    private void RemoveAction(PlayableDirector obj = null)
    {
        EffectObject.SetActive(false);
        Director.Stop();
    }
}
