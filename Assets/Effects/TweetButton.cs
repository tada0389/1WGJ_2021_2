﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using naichilab;

public class TweetButton : MonoBehaviour
{
    public void TweetWithHashtags()
    {
#if UNITY_IOS
#else
        UnityRoomTweet.Tweet("wanderlust", "「wanderlust ball」で" + PlayerPrefs.GetFloat("highscore", 0).ToString() + "mの旅をした！", "unityroom", "unity1week");
#endif
    }
}
