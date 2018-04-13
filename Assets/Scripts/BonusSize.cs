using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSize : MonoBehaviour {

    private const float bonusDuration = 5f;

    IEnumerator IIncreaseSize()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Vector2 size = sprite.size;
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();


        for (float f = size.x; f < 1.6f; f += 0.05f)
        {
            size.x = f;

            sprite.size = size;
            capsule.size = size;

            GetComponent<Movement>().GetBounce();

            yield return null;
        }
        CancelInvoke("RestoreSize");
        Invoke("RestoreSize", bonusDuration);
    }

    IEnumerator IDecreaseSize()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Vector2 size = sprite.size;
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();


        for (float f = size.x; f > 0.4f; f -= 0.05f)
        {
            size.x = f;

            sprite.size = size;
            capsule.size = size;

            GetComponent<Movement>().GetBounce();

            yield return null;
        }
        CancelInvoke("RestoreSize");
        Invoke("RestoreSize", bonusDuration);

    }

    IEnumerator IRestoreSize()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Vector2 size = sprite.size;
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();

        if (size.x > 0.8f)
        {
            for (float f = size.x; f > 0.8f; f -= 0.05f)
            {
                size.x = f;

                sprite.size = size;
                capsule.size = size;

                GetComponent<Movement>().GetBounce();

                yield return null;
            }
        }
        else
        {
            for (float f = size.x; f < 0.8f; f += 0.05f)
            {
                size.x = f;

                sprite.size = size;
                capsule.size = size;

                GetComponent<Movement>().GetBounce();

                yield return null;
            }
        }
    }

    public void IncreaseSize()
    {
        StartCoroutine("IIncreaseSize");
    }

    public void DecreaseSize()
    {
        StartCoroutine("IDecreaseSize");
    }

    public void RestoreSize()
    {
        StartCoroutine("IRestoreSize");
    }

}
