using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIController : MonoBehaviour
{ 
		//Flags
	
		public static bool halt = false;

		private bool stopTime;
		private bool timeStopped;
		private bool playedFreezeSound;
		private bool playedRevertSound;

		//Posicionadores
		public int energyElementStartingXPos;
		public int energyElementStartingYPos;
		public int abilitiesElementStartingYPos;
		public int energyElementXSpacing;
		public int energyElementYSpacing;
		private float abilitiesAreaHiddenX;
		private float moveXValue;
	
		//Texturas de objetos da GUI
		public Texture computerImage;
		public Texture computerScriptImage;
		public Texture energyFull;
		public Texture energyHalf;
		public Texture energyEmpty;
		public Texture abilityWeapon;
		public Texture abilityPower;
		public Texture abilitySpecial;
		public Texture abilityWeaponHighlighted;
		public Texture abilityPowerHighlighted;
		public Texture abilitySpecialHighlighted;
		public Texture abilityWeaponClicked;
		public Texture abilityPowerClicked;
		public Texture abilitySpecialClicked;


		public Texture scriptHud;
		public AudioClip sciptGetSound;
		public int boxWidth;
		public int boxHeight;
		public int boxStartPositionX;
		public int boxStartPositionY;
		public int boxEndPositionX;
		public int boxEndPositionY;
		public int boxMoveScaleX;
		public int boxMoveScaleY;
		public int offsetTextX;
		public int offsetTextY;
		private float boxPositionX;
		private float boxPositionY;
		private bool showBox;
		private int coinsToShow;

		public GUISkin skin;

		//Sons da GUI
		public AudioClip freezeSound;
		public AudioClip revertSound;

		//LifeManager
		LifeManager lifeManager;
		
		//AbilitiesManager
		AbilitiesManager abilitiesManager;

		//CoinManager
		CoinManager coinManager;

		private int previousNumberOfCoins;

		/**
	 * Nome: Start
	 * Funçao: Metodo executado quando o objeto Unity associado eh inicializado
	 * Parametros: Nao tem
	 * Retorno: Nenhum
	 * Chamada: Nao se aplica
	 **/
		void Start ()
		{
				GameObject gameManager = GameObject.Find ("GameManager");
				lifeManager = gameManager.GetComponent<LifeManager> ();
				abilitiesManager = gameManager.GetComponent<AbilitiesManager> ();
				coinManager = gameManager.GetComponent<CoinManager> ();

				//Inicializando valores
				stopTime = false;
				timeStopped = false;
				playedFreezeSound = false;
				playedRevertSound = true;
				abilitiesAreaHiddenX = -110;
				moveXValue = abilitiesAreaHiddenX;

				boxStartPositionX = Screen.width - boxStartPositionX;
				boxStartPositionY = Screen.height - boxStartPositionY;
				boxEndPositionX = Screen.width - boxEndPositionX;
				boxEndPositionY = Screen.height - boxEndPositionY;
				boxPositionX = boxStartPositionX;
				boxPositionY = boxStartPositionY;

				coinsToShow = 0;
				previousNumberOfCoins = coinManager.numberOfCoins;
		}

		/**
	 * Nome: Update
	 * Funçao: Metodo executado a cada frame da cena em que o objeto que contem o
	 * 	script esta presente
	 * Parametros: Nao tem
	 * Retorno: Nenhum
	 * Chamada: Nao se aplica
	 **/
		void Update ()
		{
				if (!halt) {
						if (previousNumberOfCoins != coinManager.numberOfCoins) {
								coinsToShow++;
								previousNumberOfCoins = coinManager.numberOfCoins;
								StartCoroutine (ShowCoinGUI ());
						}
				}
		}

		/**
	 * Nome: OnGUI
	 * Funçao: Metodo executado em cada atualizacao de HUD, geralmente entre frames
	 * Parametros: Nao tem
	 * Retorno: Nenhum
	 * Chamada: Nao se aplica
	 **/
		void OnGUI ()
		{
				if (!halt) {
						GUI.skin = skin;

						//Controla a exibiçao de energia na tela
						this.GerenciarEnergia ();
		
						//Gerencia a exibiçao ou retraçao do menu de habilidades e a parada do tempo
						this.GerenciarMenuHabilidades ();

						this.ShowCoins ();
		
						//Se foram definidas imagens de menu e script padrao, exibe-as
						if (computerImage != null && computerScriptImage != null) {			
								//Se o tempo deve ser parado, exibe o icone de "scripts"
								//Ambos os icones possuem prioridade ("camada") maior em
								//relaçao ao "menu" de habilidades
								if (stopTime) {
										GUI.DrawTexture (new Rect (5, 5, 96, 96), computerScriptImage);
								}
			//Caso contrario, exibe o icone "normal"
			else {
										GUI.DrawTexture (new Rect (5, 5, 96, 96), computerImage);
								}
						}
				}
		}

		/**
	 * Nome: GerenciarEnergia
	 * Funçao: Exibe o indicador de energia de acordo com a energia do jogador
	 * Parametros: Nao tem
	 * Retorno: Nenhum
	 * Chamada: Deve ser chamado no metodo gerenciador de HUD (em "OnGUI")
	 **/
		private void GerenciarEnergia ()
		{
				//Verifica se as texturas necessarias foram definidas
				if (energyFull != null && energyHalf != null && energyEmpty != null) {
						//Inicializa os valores
						int energyElementsToDraw = lifeManager.maxLife / 2;
						int energyElementXPos = energyElementStartingXPos;
						int energyElementYPos = energyElementStartingYPos;
						int energyElement = 0;
						int layer = 0;
						int energiaTotal = 2;
						int energiaCorrente = lifeManager.PlayerLife;

						//Repete a exibiçao de energia enquanto houver elementos de energia a serem exibidos.
						//Referente a energia total do personagem (definida em "Constants" e "Player")
						do {
								//A energia total sera incrementada para cada "elemento de energia". Se, nesta "posiçao"
								//for menor ou igual a energia corrente, desenha-se um icone "cheio". Caso seja menor e
								//menor que a posiçao anterior, um vazio. Se estiver entre as duas, um icone "pela metade"
								if (energiaCorrente >= energiaTotal) {
										GUI.DrawTexture (new Rect (energyElementXPos, energyElementYPos, 49, 44), energyFull);
								} else {
										if (energiaCorrente == energiaTotal - 1) {
												GUI.DrawTexture (new Rect (energyElementXPos, energyElementYPos, 49, 44), energyHalf);
										} else {
												
												GUI.DrawTexture (new Rect (energyElementXPos, energyElementYPos, 49, 44), energyEmpty);
												 
										}
								}
								if (energiaTotal < lifeManager.maxLife) {
										energiaTotal += 2;
								}

								//Alinha as posiçoes de desenho para linhas e colunas, com base no espaçamento definido no
								//objeto Unity associado e na quantidade de elementos por linha, definida em "Constants"
								energyElementXPos += energyElementXSpacing;
								energyElement++;
								if (energyElement >= Constants.ENERGIA_NUMERO_MAXIMO_INDICADORES_LINHA) {
										layer++;
										energyElementXPos = 105;
										energyElement = 0;
										energyElementYPos += energyElementYSpacing;
								}
								//Diminui a quantidade de elementos que precisam ser renderizados, ja que
								//acabou-se de renderizar um deles
								energyElementsToDraw --;
						} while (energyElementsToDraw > 0);
				}
		}

		/**
	 * Nome: GerenciarMenuHabilidades
	 * Funçao: Para ou continua o tempo e exibe o menu de habilidades quando o
	 * 	botao "Habilidades" for pressionado
	 * Parametros: Nao tem
	 * Retorno: Nenhum
	 * Chamada: Deve ser chamado no metodo gerenciador de HUD (em "OnGUI")
	 **/
		private void GerenciarMenuHabilidades ()
		{
				//Verifica se o botao de "habilidades" (shift por padrao) foi pressionado
				if (Input.GetButton ("Abilities")) {
						//Caso afirmativo, indica a "parada" no tempo
						stopTime = true;
						//Verifica se o som de "parada" ja foi tocado. Caso negativo, toca-o
						if (!playedFreezeSound) {
								/** E necessario "reinicializar" o tempo antes de reproduzir o som,
				 * caso contrario o mesmo nao eh reproduzido. Para evitar
				 * interferencia com a "parada" de tempo, armazena-se o valor
				 * numa variavel, volta-se o tempo ao normal e, apos tocar o som,
				 * volta-se o tempo para o valor anterior, a fim de prosseguir com a
				 * "parada"
				**/
								float t = Time.timeScale;
								Time.timeScale = 1;
								TocarSomNumPonto (freezeSound, transform.position, 0.30f, 1.20f);
								Time.timeScale = t;
						}
						//Registra que o som de "parada" foi tocado e que o de "volta" ainda nao
						playedFreezeSound = true;
						playedRevertSound = false;
				} else {
						/** Se o botao de "habilidades" nao foi pressionado, indica a volta do tempo
			 *  ao normal e toca-se o som de "volta" da mesma forma como o de "parada"
			**/
						stopTime = false;
						if (!playedRevertSound) {
								float t = Time.timeScale;
								Time.timeScale = 1;
								TocarSomNumPonto (revertSound, transform.position, 0.30f, 1.20f);
								Time.timeScale = t;
						}
						//Registra que o som de "volta" foi tocado e reabilita o som de "parada"
						playedFreezeSound = false;
						playedRevertSound = true;
				}
		
				//Se e necessario parar o tempo e o mesmo ainda nao parou,
				//diminui-se a escala de tempo em 0.025f, ate que a mesma
				//chegue a 0.001f.
				if (stopTime && !timeStopped) {
						if (Time.timeScale - 0.025f > 0f) {
								Time.timeScale = Time.timeScale - 0.025f;
						} else {
								Time.timeScale = 0.001f;
						}
				}
		//Se o tempo nao deve ser parado, mas ainda o esta
		//incrementa-se a escala de tempo ate, no maximo, 1f
		else if (!stopTime && timeStopped) {
						if (Time.timeScale + 0.025f < 1f) {
								Time.timeScale = Time.timeScale + 0.025f;
						} else {
								Time.timeScale = 1f;
						}
				} 
		//Se nao parou o tempo nem ele esta parado, volta a
		//escala de tempo ao normal. Evita bugs de pressionamento
		//Rapido do botao de habilidades
		else if (!stopTime && !timeStopped) {
						Time.timeScale = 1f;
				}
		
				//Se a escala de tempo chegou a "1" ou mais, indica
				//que o tempo nao esta parado
				if (Time.timeScale >= 1f) {
						timeStopped = false;
				}
		//Caso contrario, indica que o tempo parou
		else if (Time.timeScale <= 0.001f) {
						timeStopped = true;
				}
		//Se o tempo nao esta normal ou parado, significa que o mesmo
		//esta em processo de parada
		else {
						timeStopped = false;			
				}
		
				//Se e necessario parar o tempo, aumenta a posicao horizontal (direita)
				///da barra de habilidades, ate que ela fique completamente visivel
				if (stopTime) {
						if (moveXValue <= -10) {
								moveXValue += 10;
						}
				}
		//Caso contrario, diminui a posicao horizontal (esquerda) ate que a barra suma
		else {
						if (moveXValue >= -100) {
								moveXValue -= 10;
						}
				}

				//Separa as habilidades em tipos
				List<Ability> listaArmas = new List<Ability> ();
				List<Ability> listaPoderes = new List<Ability> ();
				List<Ability> listaEspeciais = new List<Ability> ();

				foreach (Ability ability in abilitiesManager.abilitiesList) {
						if (ability.type == Ability.AbilityType.WeaponAbility) {
								listaArmas.Add (ability);
						} else if (ability.type == Ability.AbilityType.PowerAbility) {
								listaPoderes.Add (ability);
						} else if (ability.type == Ability.AbilityType.SpecialAbility) {
								listaEspeciais.Add (ability);
						}
				}
				//Inicializa os valores
				int weaponAbilitiesToDraw = listaArmas.Count;
				int powerAbilitiesToDraw = listaPoderes.Count;
				int specialAbilitiesToDraw = listaEspeciais.Count;
				int abilitiesElementYPos = abilitiesElementStartingYPos;

				//Se o tempo foi completamente parado ou esta em processo de parada,
				//exibe o menu de habilidades. Caso contrario, o mesmo nao e renderizado
				if (moveXValue > abilitiesAreaHiddenX) {
						GUI.Box (new Rect (moveXValue, -2, 107, Screen.height + 6), "");						

						//Verifica se as texturas necessarias foram definidas
						if (abilityWeapon != null && abilityPower != null && abilitySpecial != null) {
			
								GUIStyle style = new GUIStyle (GUI.skin.GetStyle ("label"));
								style.fontSize = 22;
								style.fontStyle = FontStyle.Bold;

								//Repete a exibiçao de habilidades de armas enquanto houver elementos de habilidade a serem exibidos.
								//Referente a quantidade de habilidades do personagem
								while (weaponAbilitiesToDraw > 0) {
										Rect weaponButton = new Rect (moveXValue + 17, abilitiesElementYPos, 72, 72);	

										if (weaponButton.Contains (Event.current.mousePosition)) {
												GUI.color = Color.white;
												GUI.contentColor = Color.white;
												GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 12, 500, 72), listaArmas [listaArmas.Count - weaponAbilitiesToDraw].name, style);
												style.fontSize = 12;
												GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 37, 500, 72), listaArmas [listaArmas.Count - weaponAbilitiesToDraw].description, style);

												if (Input.GetMouseButton (0)) {
														GUI.DrawTexture (weaponButton, abilityWeaponClicked);

												} else {
														GUI.DrawTexture (weaponButton, abilityWeaponHighlighted);
												}
										} else {
												GUI.DrawTexture (weaponButton, abilityWeapon);
										}

										abilitiesElementYPos += 85;
										weaponAbilitiesToDraw--;
								}

								//Insere um espaço extra entre as armas e as demais habilidades
								abilitiesElementYPos += 20;

								//Repete a exibiçao de habilidades de armas enquanto houver elementos de habilidade a serem exibidos.
								//Referente a quantidade de habilidades do personagem
								while (powerAbilitiesToDraw > 0) {
										Rect powerButton = new Rect (moveXValue + 17, abilitiesElementYPos, 72, 72);	

										if (!listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active && !listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].cooldown) {
												if (powerButton.Contains (Event.current.mousePosition)) {
														GUI.color = Color.white;
														GUI.contentColor = Color.white;
														GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 12, 500, 72), listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].name, style);
														style.fontSize = 12;
														GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 37, 500, 72), listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].description, style);
						
														if (Input.GetMouseButton (0) || listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active) {
																GUI.DrawTexture (powerButton, abilityPowerClicked);
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active = true;
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeRunning = listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].totalTimeActive;
														} else if (!listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active) {
																GUI.DrawTexture (powerButton, abilityPowerHighlighted);
														}
												} else {
														GUI.DrawTexture (powerButton, abilityPower);
												}
										} else {
												style.fontSize = 22;
												style.fontStyle = FontStyle.Bold;
												if (listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active) {					
							
														GUI.color = Color.white;
														GUI.contentColor = Color.white;
														GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 12, 500, 72), listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].name + " (running)", style);
							
														int totalBarra = listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeRunning * 100 / listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].totalTimeActive;
							
														Vector2 size = new Vector3 (100, 15); 
							
														// Constrain all drawing to be within a pixel area .
														GUI.BeginGroup (new Rect (moveXValue + 100, abilitiesElementYPos + 40, totalBarra, 72));
							
														// Define progress bar texture within customStyle under Normal > Background
														GUI.Box (new Rect (0, 0, size.x, size.y), "");
							
														// Always match BeginGroup calls with an EndGroup call 
														GUI.EndGroup (); 
							
														if (Time.timeScale > 0.75) {
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeRunning--;
														}
							
														GUI.DrawTexture (powerButton, abilityPowerHighlighted);
							
														if (totalBarra <= 0) {
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active = false;
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].cooldown = true;
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeCooldown = listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].totalTimeCooldown;
								
														} 
												}
						
												if (listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].cooldown) {			
							
														GUI.color = Color.white;
														GUI.contentColor = Color.white;
														GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 12, 500, 72), listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].name + " (cooldown)", style);
							
														int totalBarra = listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeCooldown * 100 / listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].totalTimeCooldown - 100;
														totalBarra *= -1;
							
														Vector2 size = new Vector3 (100, 15);
							
														// Constrain all drawing to be within a pixel area .
														GUI.BeginGroup (new Rect (moveXValue + 100, abilitiesElementYPos + 40, totalBarra, 72));
							
														// Define progress bar texture within customStyle under Normal > Background
														GUI.Box (new Rect (0, 0, size.x, size.y), "");
							
														// Always match BeginGroup calls with an EndGroup call 
														GUI.EndGroup (); 
							
														if (Time.timeScale > 0.75) {
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeCooldown--;
														}
							
														if (totalBarra > 100) { 
																listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].cooldown = false;							
														}
							
														GUI.DrawTexture (powerButton, abilityPowerClicked); 
												}
										}

										abilitiesElementYPos += 85;
										powerAbilitiesToDraw--;
								} 

								//Repete a exibiçao de habilidades de armas enquanto houver elementos de habilidade a serem exibidos.
								//Referente a quantidade de habilidades do personagem
								while (specialAbilitiesToDraw > 0) {
										Rect specialButton = new Rect (moveXValue + 17, abilitiesElementYPos, 72, 72);				
				
										if (specialButton.Contains (Event.current.mousePosition)) {
												GUI.color = Color.white;
												GUI.contentColor = Color.white;
												GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 12, 500, 72), listaEspeciais [listaEspeciais.Count - specialAbilitiesToDraw].name, style);
												style.fontSize = 12;
												GUI.Label (new Rect (moveXValue + 100, abilitiesElementYPos + 37, 500, 72), listaEspeciais [listaEspeciais.Count - specialAbilitiesToDraw].description, style);
						
												if (Input.GetMouseButton (0)) {
														GUI.DrawTexture (specialButton, abilitySpecialClicked);
												} else {
														GUI.DrawTexture (specialButton, abilitySpecialHighlighted);
												}
										} else { 
												GUI.DrawTexture (specialButton, abilitySpecial);
										}

										abilitiesElementYPos += 85;
										specialAbilitiesToDraw--;
								}
						}
				} else {
						//Verifica se as texturas necessarias foram definidas
						if (abilityWeapon != null && abilityPower != null && abilitySpecial != null) {

								float moveXValueActive = moveXValue + 60f; 
					
								GUIStyle style = new GUIStyle (GUI.skin.GetStyle ("label"));
								style.fontSize = 22;
								style.fontStyle = FontStyle.Bold;
				
								//Repete a exibiçao de habilidades de armas enquanto houver elementos de habilidade a serem exibidos.
								//Referente a quantidade de habilidades do personagem
								while (weaponAbilitiesToDraw > 0) {			
										abilitiesElementYPos += 85;  
										weaponAbilitiesToDraw--; 
								} 
				
								//Insere um espaço extra entre as armas e as demais habilidades
								abilitiesElementYPos += 20;
				
								//Repete a exibiçao de habilidades de armas enquanto houver elementos de habilidade a serem exibidos.
								//Referente a quantidade de habilidades do personagem
								while (powerAbilitiesToDraw > 0) {
										if (listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active) {					
												Rect powerButton = new Rect (moveXValueActive + 17, abilitiesElementYPos, 72, 72);	

												GUI.color = Color.white;
												GUI.contentColor = Color.white;
												GUI.Label (new Rect (moveXValueActive + 100, abilitiesElementYPos + 12, 500, 72), listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].name + " (running)", style);
												
												int totalBarra = listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeRunning * 100 / listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].totalTimeActive;

												Vector2 size = new Vector3 (100, 15); 
												
												// Constrain all drawing to be within a pixel area .
												GUI.BeginGroup (new Rect (moveXValueActive + 100, abilitiesElementYPos + 40, totalBarra, 72));
						
												// Define progress bar texture within customStyle under Normal > Background
												GUI.Box (new Rect (0, 0, size.x, size.y), "");
						
												// Always match BeginGroup calls with an EndGroup call 
												GUI.EndGroup (); 

												if (Time.timeScale > 0.75) {
														listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeRunning--;
												}
																								
												GUI.DrawTexture (powerButton, abilityPowerHighlighted);

												if (totalBarra <= 0) {
														listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].active = false;
														listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].cooldown = true;
														listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeCooldown = listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].totalTimeCooldown;

												}
										}

										if (listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].cooldown) {					
												Rect powerButton = new Rect (moveXValueActive + 17, abilitiesElementYPos, 72, 72);	
						
												GUI.color = Color.white;
												GUI.contentColor = Color.white;
												GUI.Label (new Rect (moveXValueActive + 100, abilitiesElementYPos + 12, 500, 72), listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].name + " (cooldown)", style);
						
												int totalBarra = listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeCooldown * 100 / listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].totalTimeCooldown - 100;
												totalBarra *= -1;

												Vector2 size = new Vector3 (100, 15);
						 
												// Constrain all drawing to be within a pixel area .
												GUI.BeginGroup (new Rect (moveXValueActive + 100, abilitiesElementYPos + 40, totalBarra, 72));
						
												// Define progress bar texture within customStyle under Normal > Background
												GUI.Box (new Rect (0, 0, size.x, size.y), "");
						
												// Always match BeginGroup calls with an EndGroup call 
												GUI.EndGroup (); 

												if (Time.timeScale > 0.75) {
														listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].timeCooldown--;
												}

												if (totalBarra > 100) { 
														listaPoderes [listaPoderes.Count - powerAbilitiesToDraw].cooldown = false;							
												}
						
												GUI.DrawTexture (powerButton, abilityPowerClicked); 
										}
										
					
										abilitiesElementYPos += 85;
										powerAbilitiesToDraw--;
								}
						}
				}
		}

		private void ShowCoins ()
		{
				if (showBox) {
						if (boxPositionX > boxEndPositionX) {
								boxPositionX -= boxMoveScaleX * Time.deltaTime * 50;
						}
			
						if (boxPositionY > boxEndPositionY) {
								boxPositionY -= boxMoveScaleY * Time.deltaTime * 50;
						}
						if (!(boxPositionX > boxEndPositionX) && !(boxPositionY > boxEndPositionY)) {
						}
				} else {
						if (boxPositionX < boxStartPositionX) {
								boxPositionY += boxMoveScaleY * Time.deltaTime * 50;
						}
			
						if (boxPositionY < boxStartPositionY) {
								boxPositionY += boxMoveScaleY * Time.deltaTime * 50;
						}
						if (!(boxPositionY > boxEndPositionY) && !(boxPositionY > boxEndPositionY)) {
						}		
				}

				if (boxPositionX < Screen.width && boxPositionY < Screen.height) {
						GUIStyle style = new GUIStyle (GUI.skin.GetStyle ("label"));
						style.fontSize = 26;
						style.fontStyle = FontStyle.Bold;
						Rect weaponButton = new Rect (boxPositionX, boxPositionY, boxWidth, boxHeight);
						GUI.DrawTexture (weaponButton, scriptHud);
						GUI.color = Color.black;
						GUI.contentColor = Color.white;
						GUI.Label (new Rect (boxPositionX + offsetTextX, boxPositionY + offsetTextY, boxWidth, boxHeight), coinManager.numberOfCoins + " / " + coinManager.maximumCoins, style);
						GUI.color = Color.white;
				}
		}


		IEnumerator ShowCoinGUI ()
		{			
				TocarSomNumPonto (sciptGetSound, this.transform.position, 1f, 1.00f);
				showBox = true;
		
				yield return new WaitForSeconds (2);
				coinsToShow--;

				if (coinsToShow <= 0) {
						showBox = false;
				}
		}

		/**
	 * Nome: TocarSomNumPonto
	 * Funçao: Cria um objeto Unity, associa um som ao mesmo e o reproduz numa determinada
	 * 	posiçao
	 * Parametros: 	AudioClip clip - O som a ser reproduzido
	 * 				Vector3 position - A posiçao onde o som sera reproduzido
	 * 				float volume - O volume em que o som sera reproduzido
	 * 				float pitch - A "altura" (ou "tom") em que o som sera reproduzido
	 * Retorno: Nenhum
	 * Chamada: Pode ser chamado em qualquer local
	 **/
		private void TocarSomNumPonto (AudioClip clip, Vector3 position, float volume, float pitch)
		{
				GameObject obj = new GameObject ();
				obj.name = "Sound Clip";
				obj.transform.position = position;
				obj.AddComponent<AudioSource> ();
				obj.audio.pitch = pitch;
				obj.audio.PlayOneShot (clip, volume);
				Destroy (obj, clip.length / pitch);
		}
}
