using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private Texture2D gameCursor;
    [SerializeField] private Texture2D menuCursor;

    private Vector2 cursorHotspot;

    private void Start()
    {
        SetGameCursor();
    }

    public void SetGameCursor()
    {
        cursorHotspot = new Vector2(gameCursor.width / 2, gameCursor.height / 2);
        Cursor.SetCursor(gameCursor, cursorHotspot, CursorMode.Auto);
    }

    public void SetMenuCursor()
    {
        Cursor.SetCursor(menuCursor, Vector2.zero, CursorMode.Auto);
    }
}
