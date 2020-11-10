using UnityEngine;
using Mirror;

public class EmoteManager : NetworkBehaviour
{
    [SerializeField] private BillboardManager emoteBillboard;

    [Header("Input")]
    [SerializeField] private KeyCode smileKey;
    [SerializeField] private KeyCode frownKey;
    [SerializeField] private KeyCode surprisedKey;
    [SerializeField] private KeyCode unsureKey;

    private float visibleTimer;
    private float visibleTime = 3f;

    [SyncVar(hook = "SetFrame")] private Vector2Int frame;
    [SyncVar(hook = "SetVisibility")] private bool visibility;

    private void Update()
    {
        if (!isLocalPlayer) return;
        TimeVisibility();
        PollForInput();
    }

    private void PollForInput()
    {
        if (Input.GetKeyDown(smileKey))
        {
            CmdSetFrame(new Vector2Int(0, 0));
            CmdSetVisibility(true);
            visibleTimer = visibleTime;
        }
        if (Input.GetKeyDown(frownKey))
        {
            CmdSetFrame(new Vector2Int(1, 0));
            CmdSetVisibility(true);
            visibleTimer = visibleTime;
        }
        if (Input.GetKeyDown(surprisedKey))
        {
            CmdSetFrame(new Vector2Int(0, 1));
            CmdSetVisibility(true);
            visibleTimer = visibleTime;
        }
        if (Input.GetKeyDown(unsureKey))
        {
            CmdSetFrame(new Vector2Int(1, 1));
            CmdSetVisibility(true);
            visibleTimer = visibleTime;
        }
    }

    private void TimeVisibility()
    {
        if (visibleTimer > 0)
        {
            visibleTimer -= Time.deltaTime;

            if (visibleTimer < 0)
            {
                CmdSetVisibility(false);
            }
        }
    }

    [Command] public void CmdSetFrame(Vector2Int frame) => this.frame = frame;
    [Command] public void CmdSetVisibility(bool visibility) => this.visibility = visibility;

    public void SetFrame(Vector2Int oldFrame, Vector2Int newFrame)
    {
        emoteBillboard.SetFrame(newFrame.x, newFrame.y);
    }

    public void SetVisibility(bool old, bool visibility)
    {
        emoteBillboard.SetVisiblity(visibility);
    }
}