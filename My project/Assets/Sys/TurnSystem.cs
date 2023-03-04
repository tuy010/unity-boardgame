using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject mainMenu;
    [SerializeField] private GameObject gameInfoUI;
    GameInfo gameInfoUI_GameInfo;
    [SerializeField] private GameObject diceUI;
    DiceUI diceUI_DiceUI;
    [SerializeField] private GameObject statusUI;
    StatusUI statusUI_StatusUI;
    [SerializeField] private GameObject targetControllerUI;
    [SerializeField] private GameObject toMainUI;
    [SerializeField] private GameObject mapUI;
    MapUI mapUI_MapUI;
    [SerializeField] private GameObject checkUI;
    [SerializeField] private GameObject nodeTargeter;
    [SerializeField] private GameObject inventoryUI;
    Inventory inventoryUI_Inventory;
    [SerializeField] private GameObject equidUI;
    EquidUI equidUI_EquidUI;


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
        Topview_Target,
        TownNode
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
        GetUIScript();

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

        gameInfoUI_GameInfo.turn = totalturn;

        /// <Main>
        if (turnProgress == TurnProgress.Main)
        {
            ChangeToMainCamera(playerCount);
            targetPlayer = playerCount;

            if(turnStep == 0)
            {
                OffMenuUI();
                mainMenu.SetActive(true);
                turnStep = 1;
            }
            
        }
        else if (turnProgress == TurnProgress.RollDice)
        {   
            if(turnStep == 0)
            {
                dice = Random.Range(1, 7);
                OffMenuUI();
                diceUI.SetActive(true);
                turnStep++;
                timer = 0;
            }
            if(turnStep == 1)
            {
                timer += Time.deltaTime;
                diceUI_DiceUI.num = Random.Range(1, 9);
                if (timer > 1f)
                {
                    timer = 0;
                    turnStep++;
                }
            }
            if(turnStep == 2)
            {
                diceUI_DiceUI.num = dice;
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
        else if (turnProgress == TurnProgress.Inventory)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                inventoryUI.SetActive(true);
                inventoryUI_Inventory.target = targetPlayer;
                inventoryUI_Inventory.isShowInfo = false;
                inventoryUI_Inventory.ReloadInventory();
                toMainUI.SetActive(true);
                turnStep = 1;
            }
        }
        else if (turnProgress == TurnProgress.Status)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                statusUI.SetActive(true);
                equidUI.SetActive(true);
                equidUI_EquidUI.target = targetPlayer;
                equidUI_EquidUI.ReloadEquid();
                targetControllerUI.SetActive(true);
                toMainUI.SetActive(true);
                turnStep = 1;
            }

            ChangeToMainCamera(targetPlayer);
            Player target_Player = playerList[targetPlayer].GetComponent<Player>();

            statusUI_StatusUI.hp = target_Player.hp;
            statusUI_StatusUI.hpMax = target_Player.hpMax;
            statusUI_StatusUI.mp = target_Player.mp;
            statusUI_StatusUI.mpMax = target_Player.mpMax;

            statusUI_StatusUI.lv = target_Player.lv;
            statusUI_StatusUI.exp = target_Player.exp;
            statusUI_StatusUI.expMax = target_Player.expMax;

            statusUI_StatusUI.str_player = target_Player.str_player;
            statusUI_StatusUI.dex_player = target_Player.dex_player;
            statusUI_StatusUI.int_player = target_Player.int_player;
            statusUI_StatusUI.luk_player = target_Player.luk_player;

            statusUI_StatusUI.str_up = target_Player.str_up;
            statusUI_StatusUI.dex_up = target_Player.dex_up;
            statusUI_StatusUI.int_up = target_Player.int_up;
            statusUI_StatusUI.luk_up = target_Player.luk_up;

            statusUI_StatusUI.money = target_Player.money;
            statusUI_StatusUI.nickname = target_Player.nickname;           
        }
        else if (turnProgress == TurnProgress.Topview_Free)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                turnStep = 1;

                toMainUI.SetActive(true);
                mapUI.SetActive(true);
            }
            ChangeToTopdownView();
            mapUI_MapUI.nickname.text = "";
        }
        else if (turnProgress == TurnProgress.Topview_Target)
        {
            if (turnStep == 0)
            {
                OffMenuUI();
                turnStep = 1;

                targetControllerUI.SetActive(true);
                toMainUI.SetActive(true);
                mapUI.SetActive(true);
            }
            ChangeToTopdownView(targetPlayer);
            mapUI_MapUI.nickname.text = playerList[targetPlayer].GetComponent<Player>().nickname;
        }
        /// </Main>

        /// <DiceAndMove>
        else if(turnProgress == TurnProgress.Move)
        {
            Player player_Player = playerList[playerCount].GetComponent<Player>();
            Node nownode_Node = player_Player.nowNode.GetComponent<Node>();
            if(nownode_Node.isCrossroad)
            {
                if (diceCnt < dice)
                {
                    ChoseCrossroad(playerList[playerCount]);
                }
            }
            else
            {
                if (player_Player.nowNode == player_Player.townNode && diceCnt != 0)
                {
                    turnProgress = TurnProgress.TownNode;
                }
                else if (nownode_Node.nodeType == Node.NodeType.PortalNode)
                {
                    timer += Time.deltaTime;
                    if (timer > 0.5f)
                    {
                        player_Player.nowNode = player_Player.townNode;
                        timer = 0;
                        turnProgress = TurnProgress.TownNode;
                    }
                }
                else if (diceCnt < dice)
                {
                    timer += Time.deltaTime;
                    if (timer > 0.5f)
                    {
                        MovePlayer(playerList[playerCount]);
                        timer = 0;
                        diceCnt++;
                    }
                }
                else if (diceCnt >= dice)
                {
                    timer += Time.deltaTime;
                    if (timer > 1)
                    {
                        turnProgress = TurnProgress.NodeEvent;
                        timer = 0;
                        diceCnt = 0;
                    }
                }
            }
            
        }      
        else if (turnProgress == TurnProgress.NodeEvent)
        {
            if(turnStep == 0)
            {
                NodeEvent(playerList[playerCount]);
                turnStep++;
            }
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
        mainMenu.SetActive(false);
        diceUI.SetActive(false);
        statusUI.SetActive(false);
        targetControllerUI.SetActive(false);
        toMainUI.SetActive(false);
        mapUI.SetActive(false);
        inventoryUI.SetActive(false);
        equidUI.SetActive(false);
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
    void GetUIScript()
    {
        gameInfoUI_GameInfo = gameInfoUI.GetComponent<GameInfo>();
        diceUI_DiceUI = diceUI.GetComponent<DiceUI>();
        statusUI_StatusUI = statusUI.GetComponent<StatusUI>();
        mapUI_MapUI = mapUI.GetComponent<MapUI>();
        inventoryUI_Inventory = inventoryUI.GetComponent<Inventory>();
        equidUI_EquidUI = equidUI.GetComponent<EquidUI>();
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
        Player player_Player = player.GetComponent<Player>();
        Node.NodeType type = player_Player.nowNode.GetComponent<Node>().nodeType;
        if (player_Player.nowNode == player_Player.townNode)
        {
            
        }
        else
        {
            switch (type)
            {
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
                checkUI.SetActive(true);
                turnStep = 1;
            }
            else if (turnStep == 1&&pressCheckbutton)
            {
                timer = 0;
                diceCnt++;
                pressCheckbutton = false;
                checkUI.SetActive(false);
                ShowNodeTarget();
                MovePlayer(player, isAnotherRoad);
                isAnotherRoad = -1;
                turnStep = 0;
                Debug.Log("Chose Road");
            }
        }
    }
    void ShowNodeTarget(GameObject node = null)
    {
        if(node == null) nodeTargeter.SetActive(false);
        else
        {
            if (!nodeTargeter.activeSelf)
            {
                nodeTargeter.SetActive(true);
            }
            Transform tmp = node.GetComponent<Transform>();
            nodeTargeter.GetComponent<Transform>().position = new Vector3(tmp.position.x, tmp.position.y+0.2f, tmp.position.z);
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
        mapUI_MapUI.nickname.text = "";
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
