using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    [Header ("Game Objects")]
    List<GameObject> playerList = new List<GameObject>();
    List<GameObject> nodeList = new List<GameObject>();
    public GameObject mainCamera;
    public GameObject topdownCamera;

    [Header ("UI"), SerializeField]
    private GameObject MainMenu;
    [SerializeField] private GameObject GameInfoUI;
    [SerializeField] private GameObject DiceUI;
    [SerializeField] private GameObject StatusUI;
    [SerializeField] private GameObject TargetControllerUI;
    [SerializeField] private GameObject ToMainUI;
    [SerializeField] private GameObject MapUI;
    [SerializeField] private GameObject CheckUI;
    [SerializeField] private GameObject NodeTargeter;
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject EquidUI;


    [Header ("Turs Sys"), SerializeField]
    private int playerCount = -1;
    [SerializeField] private int targetPlayer = -1;
    [SerializeField] private int totalturn = -1;

    public enum TurnProgress
    {
        Main,
        RollDice,
        Move,
        NodeEvent,
        TurnEnd,
        Status,
        Inventory,
        Topview_Free,
        Topview_Target
    }
    [Header ("Turn Progress")]
    public TurnProgress turnProgress = TurnProgress.Main;

    /// <UsedToMakeDelay>
    float timer = 0f;
    int turnStep = 0;
    /// </UsedToMakeDelay>

    /// <DiceAndMove>
    int dice = 0;
    int diceCnt = 0;
    int isAnotherRoad = -1;
    bool pressCheckbutton = false;
    /// </DiceAndMove> 

    void Start()
    {
        GetPlayerData();
        GetNodeData();
        GetCameraData();

        ChangeToMainCamera();
    }

    // Update is called once per frame
    void Update()
    {
        //first
        if(totalturn == -1 && playerCount == -1)
        {
            totalturn = 1;
            playerCount = 0;
            playerList[playerCount].GetComponent<Player>().isturn = true;
            turnStep = 0;
        }

        GameInfoUI.GetComponent<GameInfo>().turn = totalturn;

        /// <Main>
        if (turnProgress == TurnProgress.Main)
        {
            ChangeToMainCamera(playerCount);
            targetPlayer = playerCount;

            if(turnStep == 0)
            {
                OffMenuUI();
                MainMenu.SetActive(true);
                turnStep = 1;
            }
            
        }
        if (turnProgress == TurnProgress.RollDice)
        {   
            if(turnStep == 0)
            {
                dice = Random.Range(1, 7);
                OffMenuUI();
                DiceUI.SetActive(true);
                turnStep++;
                timer = 0;
            }
            if(turnStep == 1)
            {
                timer += Time.deltaTime;
                DiceUI.GetComponent<DiceUI>().num = Random.Range(1, 9);
                if (timer > 1f)
                {
                    timer = 0;
                    turnStep++;
                }
            }
            if(turnStep == 2)
            {
                DiceUI.GetComponent<DiceUI>().num = dice;
                timer += Time.deltaTime;               
                if (timer > 1f)
                {
                    timer = 0;
                    turnStep++;
                }
            }
            if(turnStep == 3)
            {
                ChangeToTopdownView(playerCount);
                timer = 0.5f;
                turnProgress = TurnProgress.Move;
                OffMenuUI();
                turnStep = 0;
            }
        }
        if (turnProgress == TurnProgress.Inventory)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                InventoryUI.SetActive(true);
                InventoryUI.GetComponent<Inventory>().target = targetPlayer;
                InventoryUI.GetComponent<Inventory>().isShowInfo = false;
                InventoryUI.GetComponent<Inventory>().ReloadInventory();
                ToMainUI.SetActive(true);
                turnStep = 1;
            }
        }
        if (turnProgress == TurnProgress.Status)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                StatusUI.SetActive(true);
                EquidUI.SetActive(true);
                EquidUI.GetComponent<EquidUI>().target = targetPlayer;
                EquidUI.GetComponent<EquidUI>().ReloadEquid();
                TargetControllerUI.SetActive(true);
                ToMainUI.SetActive(true);
                turnStep = 1;
            }

            ChangeToMainCamera(targetPlayer);

            StatusUI.GetComponent<StatusUI>().hp = playerList[targetPlayer].GetComponent<Player>().hp;
            StatusUI.GetComponent<StatusUI>().hpMax = playerList[targetPlayer].GetComponent<Player>().hpMax;
            StatusUI.GetComponent<StatusUI>().mp = playerList[targetPlayer].GetComponent<Player>().mp;
            StatusUI.GetComponent<StatusUI>().mpMax = playerList[targetPlayer].GetComponent<Player>().mpMax;

            StatusUI.GetComponent<StatusUI>().lv = playerList[targetPlayer].GetComponent<Player>().lv;
            StatusUI.GetComponent<StatusUI>().exp = playerList[targetPlayer].GetComponent<Player>().exp;
            StatusUI.GetComponent<StatusUI>().expMax = playerList[targetPlayer].GetComponent<Player>().expMax;

            StatusUI.GetComponent<StatusUI>().str_player = playerList[targetPlayer].GetComponent<Player>().str_player;
            StatusUI.GetComponent<StatusUI>().dex_player = playerList[targetPlayer].GetComponent<Player>().dex_player;
            StatusUI.GetComponent<StatusUI>().int_player = playerList[targetPlayer].GetComponent<Player>().int_player;
            StatusUI.GetComponent<StatusUI>().luk_player = playerList[targetPlayer].GetComponent<Player>().luk_player;

            StatusUI.GetComponent<StatusUI>().str_up = playerList[targetPlayer].GetComponent<Player>().str_up;
            StatusUI.GetComponent<StatusUI>().dex_up = playerList[targetPlayer].GetComponent<Player>().dex_up;
            StatusUI.GetComponent<StatusUI>().int_up = playerList[targetPlayer].GetComponent<Player>().int_up;
            StatusUI.GetComponent<StatusUI>().luk_up = playerList[targetPlayer].GetComponent<Player>().luk_up;

            StatusUI.GetComponent<StatusUI>().money = playerList[targetPlayer].GetComponent<Player>().money;
            StatusUI.GetComponent<StatusUI>().nickname = playerList[targetPlayer].GetComponent<Player>().nickname;           
        }
        if (turnProgress == TurnProgress.Topview_Free)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                turnStep = 1;

                ToMainUI.SetActive(true);
                MapUI.SetActive(true);
            }
            ChangeToTopdownView();
            MapUI.GetComponent<MapUI>().nickname.text = "";
        }
        if (turnProgress == TurnProgress.Topview_Target)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                turnStep = 1;

                TargetControllerUI.SetActive(true);
                ToMainUI.SetActive(true);
                MapUI.SetActive(true);
            }
            ChangeToTopdownView(targetPlayer);
            MapUI.GetComponent<MapUI>().nickname.text = playerList[targetPlayer].GetComponent<Player>().nickname;
        }
        /// </Main>

        /// <DiceAndMove>
        if (playerList[playerCount].GetComponent<Player>().nowNode.GetComponent<Node>().isCrossroad && turnProgress == TurnProgress.Move && diceCnt < dice)
        {
            ChoseCrossroad(playerList[playerCount]);
        }
        else if (!playerList[playerCount].GetComponent<Player>().nowNode.GetComponent<Node>().isCrossroad&&turnProgress == TurnProgress.Move && diceCnt < dice)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                MovePlayer(playerList[playerCount]);
                timer = 0;
                diceCnt++;
            }
        }
        else if (turnProgress == TurnProgress.Move && diceCnt >= dice)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                turnProgress = TurnProgress.NodeEvent;
                timer = 0;
                diceCnt = 0;
            }
        }
        else if (turnProgress == TurnProgress.NodeEvent&&turnStep==0)
        {
            NodeEvent(playerList[playerCount]);
            turnStep++;
        }
        else if (turnProgress == TurnProgress.TurnEnd)
        {
            turnStep = 0;
            playerList[playerCount].GetComponent<Player>().isturn = false;
            turnProgress = TurnProgress.Main;
            timer = 0;
            playerCount++;
            if (playerCount > 3)
            {
                playerCount = 0;
                totalturn++;
                ChangeToTopdownView();
            }
            playerList[playerCount].GetComponent<Player>().isturn = true;
        }
        /// </DiceAndMove>
    }

    void OffMenuUI()
    {
        MainMenu.SetActive(false);
        DiceUI.SetActive(false);
        StatusUI.SetActive(false);
        TargetControllerUI.SetActive(false);
        ToMainUI.SetActive(false);
        MapUI.SetActive(false);
        InventoryUI.SetActive(false);
        EquidUI.SetActive(false);
    }

    /// <GetData>
    void GetPlayerData()
    {
        playerList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().playerList;
    }
    void GetNodeData()
    {
        nodeList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().nodeList;
    }
    void GetCameraData()
    {
        mainCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().mainCamera;
        topdownCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().topdownCamera;
    }
    /// </GetData>
 
    /// <Camera>
    void ChangeToTopdownView(int num = -1)
    {
        mainCamera.SetActive(false);
        topdownCamera.SetActive(true);

        topdownCamera.GetComponent<TopdownCameraMovement>().target = num;
    }
    void ChangeToMainCamera(int num = -1)
    {
        mainCamera.SetActive(true);
        topdownCamera.SetActive(false);

        mainCamera.GetComponent<MainCameraMovement>().target = num;
    }
    ///</Camera>
    
    /// <DiceAndMove>
    void MovePlayer(GameObject player, int isAnotherroad = 0)
    {
        if(isAnotherroad == 0) player.GetComponent<Player>().nowNode = player.GetComponent<Player>().nowNode.GetComponent<Node>().nextNode;
        else player.GetComponent<Player>().nowNode = player.GetComponent<Player>().nowNode.GetComponent<Node>().anotherNode;
    }
    void NodeEvent(GameObject player)
    {
        GameObject node = player.GetComponent<Player>().nowNode;
        Node.NodeType type = node.GetComponent<Node>().nodeType;

        switch (type)
        {
            case Node.NodeType.TownNode:
                turnProgress = TurnProgress.TurnEnd;
                break;
            case Node.NodeType.MonsterNode:
                turnProgress = TurnProgress.TurnEnd;
                break;
            case Node.NodeType.ChanceNode:
                GetComponent<NodeEvnet_Chance>().Chance(player);
                break;
            default:
                turnProgress = TurnProgress.TurnEnd;
                break;
        }

    }
    void ChoseCrossroad(GameObject player)
    {
       if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray touchray = topdownCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(touchray, out hit);

            if (hit.collider != null)
            {
                if(hit.collider.gameObject == playerList[playerCount].GetComponent<Player>().nowNode.GetComponent<Node>().anotherNode)
                {
                    isAnotherRoad = 1;
                    ShowNodeTarget(hit.collider.gameObject);
                }
                else if(hit.collider.gameObject == playerList[playerCount].GetComponent<Player>().nowNode.GetComponent<Node>().nextNode)
                {
                    isAnotherRoad = 0;
                    ShowNodeTarget(hit.collider.gameObject);
                }
            }
            else
            {
                Debug.Log("?");
            }
        }
        if(isAnotherRoad != -1)
        {
            if(turnStep == 0)
            {
                CheckUI.SetActive(true);
                turnStep = 1;
            }
            else if (turnStep == 1&&pressCheckbutton)
            {
                timer = 0;
                diceCnt++;
                pressCheckbutton = false;
                CheckUI.SetActive(false);
                ShowNodeTarget();
                MovePlayer(player, isAnotherRoad);
                isAnotherRoad = -1;
                turnStep = 0;
            }
        }
    }
    void ShowNodeTarget(GameObject node = null)
    {
        if(node == null) NodeTargeter.SetActive(false);
        else
        {
            if (!NodeTargeter.activeSelf)
            {
                NodeTargeter.SetActive(true);
            }
            NodeTargeter.GetComponent<Transform>().position = new Vector3(node.GetComponent<Transform>().position.x, node.GetComponent<Transform>().position.y+0.2f, node.GetComponent<Transform>().position.z);
        }
    }
    /// </DiceAndMove>

    /// <ButtonFunction>
    public void ButtonRollDice()
    {
        turnProgress = TurnProgress.RollDice;
        turnStep = 0;
        timer = 0;
    }
    public void ButtonInventory()
    {
        turnProgress = TurnProgress.Inventory;
        targetPlayer = playerCount;
        turnStep = 0;
    }
    public void ButtonStatus()
    {
        turnProgress = TurnProgress.Status;
        targetPlayer = playerCount;
        turnStep = 0;
    }
    public void ButtonTargetLeft()
    {
        if (targetPlayer < 1) targetPlayer = 3;
        else targetPlayer--;
        turnStep = 0;
    }
    public void ButtonTargetRight()
    {
        if (targetPlayer > 2) targetPlayer = 0;
        else targetPlayer++;
        turnStep = 0;
    }
    public void ButtonToMain()
    {
        turnProgress = TurnProgress.Main;
        targetPlayer = playerCount;
        turnStep = 0;
    }
    public void ButtonMap()
    {
        turnProgress = TurnProgress.Topview_Free;
        targetPlayer = -1;
        turnStep = 0;
        MapUI.GetComponent<MapUI>().nickname.text = "";
    }
    public void ButtonMapSwitch()
    {
        if(turnProgress == TurnProgress.Topview_Free)
        {
            turnProgress = TurnProgress.Topview_Target;
            targetPlayer = playerCount;
            turnStep = 0;
        }
        else if(turnProgress == TurnProgress.Topview_Target)
        {
            turnProgress = TurnProgress.Topview_Free;
            targetPlayer = playerCount;
            turnStep = 0;
        }
    }
    public void ButtonCheck()
    {
        pressCheckbutton = true;
    }
    /// </ButtonFunction>
}
