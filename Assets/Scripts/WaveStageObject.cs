using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStageObject : BaseStageObject
{
    [SerializeField]
    private Vector2 yMoveRange = new Vector2(0.0f, 3.0f);

    [SerializeField]
    private float movePeriod = 1.0f;

    private float time = 0.0f;

    private bool blowed = false;

    [SerializeField]
    private ParticleSystem breakEffPrefab;

    [SerializeField]
    private AudioClip DestroySE;

    private AudioSource audioSource;

    //Zakky
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blowed) return;

        time += Time.deltaTime * StageObjectManager.staticRotateSpeed / 60.0f;

        Velocity = new Vector2(Velocity.x, Mathf.Sin(time * Mathf.PI / movePeriod) * yMoveRange.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "muteki")
        {
            // 吹き飛ぶ
            BlowedOff();
            transform.GetChild(0).GetComponent<Collider2D>().enabled = false; // 無理やりだけど
        }
    }

    // 吹き飛ばされる
    private void BlowedOff()
    {
        Instantiate(breakEffPrefab, transform.GetChild(0).position, Quaternion.identity);
        blowed = true;
        // y軸方向に＋
        Velocity = new Vector2(-0.5f, 0.5f);

        audioSource.PlayOneShot(DestroySE);
    }
}
