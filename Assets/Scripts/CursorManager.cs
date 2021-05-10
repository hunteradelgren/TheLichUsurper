using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    public int frameCount;
    public float frameRate;

    public List<CursorAnimation> cursorAnimationList;

    private CursorAnimation cursorAnimation;

    public int currentFrame;
    public float frameTimer;
    static CursorManager manager;
    public enum CursorType { 
        Idle,
        Clicked
    }

    private void Awake()
    {
        if (manager != null)
        {
            Destroy(this.gameObject);
            return;
        }
        manager = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetActiveCursorAnimation(cursorAnimationList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFrame + 1 == frameCount)
        {
            SetActiveCursorAnimation(cursorAnimationList[0]);
        }
        frameTimer -= Time.unscaledDeltaTime;
        if(frameTimer <= 0)
        {
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);
        }

        if (Input.GetMouseButtonDown(0))
        {
            SetActiveCursorAnimation(cursorAnimationList[1]);
        }
    }

    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;
    }


    [System.Serializable]
    public class CursorAnimation
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}


