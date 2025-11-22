using UnityEngine;
using UnityEngine.Tilemaps;

public class Interaction : MonoBehaviour
{
    private Vector2 _mouseWorldPos;
    private RaycastHit2D _hit;

    [SerializeField] private Tilemap _tilemap;

    public void OnInteraction()
    {
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _hit = Physics2D.Raycast(_mouseWorldPos, Vector2.zero);

        if (_hit.collider != null && _hit.collider.gameObject.layer == 8)
        {
            Vector3 worldPos = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));

            GameManager.instance.uiManager.ActiveUIMonsterSummon(worldPos);
        }
    }
}
