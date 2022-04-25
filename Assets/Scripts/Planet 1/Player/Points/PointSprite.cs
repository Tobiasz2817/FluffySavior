using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSprite : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _spriteRenderers;

    public Sprite GetRandomSprite(Sprite currentSprite)
    {
        int randomSprite = Random.Range(0, _spriteRenderers.Count);
        while (currentSprite == _spriteRenderers[randomSprite])
        {
            randomSprite = Random.Range(0, _spriteRenderers.Count);
        }

        return _spriteRenderers[randomSprite];
    }
}
