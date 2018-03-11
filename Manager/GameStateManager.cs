using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    [HideInInspector]
    public GameFocus gameFocus;
    [HideInInspector]
    public GameObject cameraObj;
    [SerializeField]
    public GameObject statiscsController;

    private SecretConsole secretConsole;

    //States
    private InitState initState;
    private StatisticsState statisticsState;
    private ConfigState configState;
    private GameState gameState;
    private SeriousGameState seriousGameState;
    private HoloState holoState;
    private CreditsState creditState;

    //Strategy
    private FSM fsm;
    public State currentState { get; private set; } //Holds current state
    
    public static GameStateManager instance { get; private set; }

    private void Awake()
    {
        //Cria a primeira instância
        instance = this;
        //Não deixa esse objeto ser destruino no carregamento de uma cena
        DontDestroyOnLoad(gameObject);
        //Cria a maquina de estados 
        fsm = gameObject.AddComponent<FSM>();
        //Cria a comunicação com o console
        secretConsole = gameObject.AddComponent<SecretConsole>();
        //Get camera
        cameraObj = GameObject.Find("Main Camera");
        //Get statistics controller
        statiscsController = GameObject.Find("Statistics");//.GetComponent<StatisticsController>();
        //Define os estados
        initState = gameObject.AddComponent<InitState>(); //Init
        configState = gameObject.AddComponent<ConfigState>(); //Config
        statisticsState = gameObject.AddComponent<StatisticsState>(); //StatisticsState
        //gameState = gameObject.AddComponent<GameState>();
        //seriousGameState = gameObject.AddComponent<SeriousGameState>();
        //holoState = gameObject.AddComponent<HoloState>();
        //creditState = gameObject.AddComponent<CreditsState>();
        //Inicializa a maquina de estado passando o primeiro estado
        fsm.Initialize(initState);
    }
    

    private void OnApplicationFocus(bool focus){
        //Cursor.visible = false;
    }


    //Método que executa a troca de estados
    public void ChangeState(State newState)
    {
        //Verifica se o estado para que está trocando é o mesmo que o atual
        if (currentState == newState)
            return; //Se for, entao retorna

        Debug.Log("Changing State To: " + (State)newState);

        switch (newState)
        {
            case State.Init:
                StartCoroutine(fsm.ChangeState(initState));
                break;
            case State.StatisticsState:
                StartCoroutine(fsm.ChangeState(statisticsState));
                break;
            case State.ConfigState:
                StartCoroutine(fsm.ChangeState(configState));
                break;
                //case State.Game:
                //    StartCoroutine(fsm.ChangeState(gameState));
                //    break;
                //case State.SeriousGame:
                //    StartCoroutine(fsm.ChangeState(seriousGameState));
                //    break;
                //case State.Holo:
                //    StartCoroutine(fsm.ChangeState(holoState));
                //    break;
                //case State.Credits:
                //    StartCoroutine(fsm.ChangeState(creditState));
                //    break;
        }
        currentState = newState;
    }
}
