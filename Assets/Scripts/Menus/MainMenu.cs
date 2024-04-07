using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Texture2D cursor;
    public Vector3 positionOffset = Vector3.zero;
    private void Start()
    {       
        Cursor.SetCursor(cursor, positionOffset, CursorMode.Auto);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
