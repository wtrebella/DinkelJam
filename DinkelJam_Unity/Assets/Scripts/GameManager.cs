using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleDelegate();

public class GameManager : Singleton<GameManager> 
{
   public SimpleDelegate SignalGameStart;
   public SimpleDelegate SignalGameOver;
   
	[SerializeField] private ModelSpawner _modelSpawner;
   [SerializeField] private UIManager _uiManager;
   
   private float _gameTimer = 0;
   private bool _gameTimerRunning = false;
   
	public ModelSpawner Spawner { get { return _modelSpawner; }}
   public UIManager UIManager {get { return _uiManager; }}

    void Awake()
    {
		MakePersistent();  
    }

	void Start() {
		GameAudio.PlayOneShot("Music");
	}
   
   public void StartGame()
   {
      StartGameTimer();
      
      if (SignalGameStart != null) SignalGameStart();
   }
   
   void StartGameTimer()
   {
      _gameTimer = 120.0f;
      _gameTimerRunning = true;
   }
   
   void Update()
   {
      if (_gameTimerRunning)
      {
         _gameTimer -= Time.deltaTime;
         if (_gameTimer < 0)
         {
            _gameTimer = 0;
            _gameTimerRunning = false;
            if (SignalGameOver != null) SignalGameOver();
         }
      }
   }
   
   public float GetGameTimer()
   {
      return _gameTimer;  
   }
}
