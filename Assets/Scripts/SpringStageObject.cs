using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SpringStageObject : BaseStageObject
{
    [SerializeField]
    private ParticleSystem breakEffPrefab;

    [SerializeField]
    private AudioClip DestroySE;

    private AudioSource audioSource;

    [SerializeField]
    private float springJumpYPosThr = 8.0f;

    //Zakky
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "muteki")
        {
            // 吹き飛ぶ
            BlowedOff();
            var cols = transform.GetChild(0).GetComponents<Collider2D>(); // 無理やりだけど
            foreach (var col in cols)
            {
                Destroy(col);
                //col.enabled = false;
            }
            foreach (Transform child in transform)
            {
                //ふっとんだとき回転させる(Zakky)
                child.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 3600f), 1f, RotateMode.FastBeyond360);
            }
        }
        else
        {
            // もし一定以上、高さがあるなら上から来たということでばね発動
            // プレイヤーならばねジャンプさせる
            MainGame.Actor.PlayerController player = collision.GetComponent<MainGame.Actor.PlayerController>();
            if (player != null)
            {
                if (player.Position.y >= springJumpYPosThr)
                {
                    player.DoSpringJump();
                }
            }
        }
    }

    // 吹き飛ばされる
    private void BlowedOff()
    {
        Instantiate(breakEffPrefab, transform.GetChild(0).position, Quaternion.identity);
        // y軸方向に＋
        Velocity = new Vector2(-0.5f, 0.5f);

        audioSource.PlayOneShot(DestroySE);
    }
}
