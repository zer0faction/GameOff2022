using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float speed = -3f;
    [SerializeField] private Rigidbody2D rb2d;
    private float width;
    private float originalStartPos;

    // Start is called before the first frame update
    void Start()
    {
        width = boxCollider2D.size.x;
        rb2d.velocity = new Vector2(speed, 0);
        originalStartPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -width + originalStartPos)
        {
            OnBgRepeater();
        }
    }

    private void OnBgRepeater()
    {
        Vector2 vector2 = new Vector2(width * 2, 0);
        transform.position = (Vector2)transform.position + vector2;
    }
}
