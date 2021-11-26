using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public TimelineAsset timelineAsset;

    public void Play()
    {
        playableDirector.Play ();
    }

    public void PlayFromTimeLine()
    {
        playableDirector.Play(timelineAsset);
    }
}
