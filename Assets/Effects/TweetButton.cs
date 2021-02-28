using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using naichilab;

public class TweetButton : MonoBehaviour
{
    public void TweetWithHashtags()
    {
        UnityRoomTweet.Tweet("wanderlust", "「wanderlust」で" + PlayerPrefs.GetFloat("highscore", 0).ToString() + "mの旅をした！", "unityroom", "unity1week");
    }
}
