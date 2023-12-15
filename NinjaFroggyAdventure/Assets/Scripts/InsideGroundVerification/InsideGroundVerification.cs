using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InsideGroundVerification : MonoBehaviour
{
    public float separation = 0.1f;
    private Tilemap tilemap;
    private float cellSize = 0.165f;
    public bool isPositionFound;
    public Vector2 finishPosition;
    private int count = 1;
    private GameObject player;
    private float playerWidth;
    private float playerHeight;
    private float playerHeigthVariation;

    private void Awake()
    {
        tilemap = GameObject.Find("TerrainTilemap").GetComponent<Tilemap>();
        player = GameObject.Find("StandarInterface").GetComponent<Initialization>().player;
        playerWidth = player.GetComponent<BoxCollider2D>().size.x;
        playerHeight = player.GetComponent<BoxCollider2D>().size.y;
        playerHeigthVariation = player.GetComponent<BoxCollider2D>().offset.y;
    }

    public void GetPositionOffGround(Vector2 startPosition)
    {
        finishPosition = startPosition;

        do
        {
            if (count == 1)
            {
                if (!CheckIsGroundPosition(finishPosition))
                {
                    isPositionFound = true;
                    return;
                }
            }
            else
            {
                finishPosition = new Vector2(startPosition.x + count * separation, startPosition.y);

                if (!CheckIsGroundPosition(finishPosition))
                {
                    isPositionFound = true;
                    return;
                }

                finishPosition = new Vector2(startPosition.x - count * separation, startPosition.y);

                if (!CheckIsGroundPosition(finishPosition))
                {
                    isPositionFound = true;
                    return;
                }

                finishPosition = new Vector2(startPosition.x, startPosition.y + count * separation);

                if (!CheckIsGroundPosition(finishPosition))
                {
                    isPositionFound = true;
                    return;
                }

                finishPosition = new Vector2(startPosition.x, startPosition.y - count * separation);

                if (!CheckIsGroundPosition(finishPosition))
                {
                    isPositionFound = true;
                    return;
                }
            }

            count++;
        } while (count < 20);
    }

    public void RestartValues()
    {
        count = 1;
        isPositionFound = false;
        finishPosition = new Vector2();
    }

    public bool CheckIsGroundPosition(Vector2 verificationPosition)
    {
        if (tilemap != null)
        {
            TileBase[] tiles = tilemap.GetTilesBlock(tilemap.cellBounds);

            foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
            {
                Vector3 cellPos = tilemap.CellToLocal(position);
                int index = position.x - tilemap.cellBounds.x + (position.y - tilemap.cellBounds.y) * tilemap.cellBounds.size.x;

                if (index >= 0 && index < tiles.Length && tiles[index] != null)
                {
                    //if (((verificationPosition.x >= (cellPos.x - cellSize / 2)) && (verificationPosition.x <= (cellPos.x + cellSize / 2)))
                    //    && ((verificationPosition.y >= (cellPos.y - cellSize / 2)) && (verificationPosition.y <= (cellPos.y + cellSize / 2))))
                    //{
                    //    Debug.Log("posicion local " + cellPos);
                    //    Debug.Log("posicion a verificar " + verificationPosition + " coincide con la posicion del tilemap " + cellPos);
                    //    return true;
                    //}

                    Rect innerRectangle = new Rect(new Vector2(verificationPosition.x, verificationPosition.y + playerHeigthVariation), new Vector2(playerWidth, playerHeight));
                    Rect outerRectangle = new Rect(cellPos, new Vector2(cellSize, cellSize));

                    if (innerRectangle.Overlaps(outerRectangle))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        else
        {
            Debug.Log("No se ha asignado el Tilemap en el inspector.");
            return false;
        }
    }
}
