using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//
using TechTweaking.Bluetooth;
public class TerminalController : MonoBehaviour {
	// Bluetooth
	private BluetoothDevice device;
	// InputField
	public InputField dataToSend;
	// Camera for movement
	public GameObject _camera;
	// Canvas for game entity
	public GameObject canvas; //coordinacion bimanual
	public GameObject canvas1_1; //test de anticipacion R
	public GameObject canvas1_2; //test de anticipacion L
	public GameObject canvas2; //test auditivo
	public GameObject canvas3; //test reactimetria
	public GameObject canvas4; //test encandilamiento
	public GameObject canvas5; //test agudeza visual
	public GameObject canvas6; //test colores P
	public GameObject canvas7; //test colores Ishihara
	public GameObject canvas8; //test foria*
	// Content variables
	private string content;
	private string contentRight;
	private string contentLeft;
	private string contentCenter;
	// Coordinacion bimanual game objects
	public GameObject carRight;
	public GameObject carLeft;
	// Coordinacion bimanual variables
	private bool bimanual;
	private int bimanualRightValue;
	private int bimanualLeftValue;
	private float bimanualTimer; //camera movement
	private float bimanualTimeRight; //car right movement
	private float bimanualTimeLeft;	//car left movement
	private bool rigth;
	private bool left;
	/* investigar!
	private float exitCount; //on exit way counter
	private string bimanualResults; //save exitCount and send the data to the arduino
	*/
	// Test de anticipacion object
	public GameObject ball;
	// Test de anticipacion variables
	private bool anticipationRightControl; //controlling right camera movement
	private bool anticipationLeftControl; //controlling left camera movement
	private float anticipationRightCount; //time counter for the right
	private float anticipationLeftCount; //time counter for the left
	/*
	 * in anticipation test, we have a variable with how long time take the ball to complete the way, and this variable substract the time variable (anticipationCount)
	 * and send the result
	*/
	private float anticipationTotalTime; //for example, 10.5 seconds
	private float anticipationRightResult;
	private float anticipationLeftResult; /*example, anticipationTotalTime - anticipationCount = anticipationResult, if anticipationCount take 9 seconds
	*substract 10.5 seconds, is equal to 1.5 seconds, this is the result and send to arduino
	*/
	private float anticipationResult; //results
	// Test auditivo game objects
	public GameObject rightSound;
	public GameObject leftSound;
	// Audio
	private AudioSource rightAudioSource;
	private AudioSource leftAudioSource;
	// Test auditivo variables
	private bool auditive;
	private float auditiveTimer; //for the test time
	private bool leftAuditive;
	private float leftAuditiveTime; //on leftAuditive turn true, how long it did take to touch the arduino button to turn false the bool
	private bool rightAuditive;
	private float rightAuditiveTime; //on rightAuditive turn true, how long it did take to touch the arduino button to turn false the bool
	private string auditiveLeftResults; //save left results into this string and send to arduino
	private string auditiveRightResults; //save rights results to send
	// Test de reactimetria game objects
	public GameObject reactimetryGreen;
	public GameObject reactimetryRed;
	// Test de reactimetria variables
	private bool reactimetry;
	private float globalReactimetryTimer;
	private bool reactimetryGreenBool;
	private float reactimetryGreenTimer;
	private bool reactimetryRedBool;
	private float reactimetryRedTimer;
	private string reactimetryGreenResults; //save the total time user takes to turn false the bool for green light
	private string reactimetryRedResults; //save the total time user takes to turn false the bool for red light
	// Foria, investigar
	// Test de encandilamiento game object
	public GameObject dazzling;
	// Test de encandilamiento variables
	private bool _dazzling; //global bool for test
	private float dazzlingTimer;
	private bool dazzlingBool;
	// Test de agudeza visual game object
	public GameObject visual;
	// Test de agudeza visual materials
	private Material visualMaterial;
	public Material visual0;
	public Material visual1;
	public Material visual2;
	public Material visual3;
	// Test de agudeza visual variables
	private bool _visual;
	private int visualControl;
	// Test de colores primarios game object
	public GameObject colors;
	// Test de colores primarios materials
	private Material colorMaterial;
	public Material color0;
	public Material color1;
	public Material color2;
	// Test de colores primarios variables
	private bool _colors;
	private int colorsControl;
	// Test de colores Ishihara game object
	public GameObject IshiharaColors;
	// Test de colores Ishihara materials
	private Material IshiharaMaterial;
	public Material IshiharaColor0;
	public Material IshiharaColor1;
	public Material IshiharaColor2;
	// Test de colores Ishihara variables
	private bool Ishihara;
	private int IshiharaColorsControl;
	private void Awake () {
		BluetoothAdapter.askEnableBluetooth ();
		BluetoothAdapter.OnDeviceOFF += HandleOnDeviceOff;
		BluetoothAdapter.OnDevicePicked += HandleOnDevicePicked;
	}
	private void Start() {
		Reset ();
	}
	private void Reset() {
		bimanualRightValue = 0;
		bimanualLeftValue = 0;
		bimanualTimer = 0;
		bimanualTimeRight = 0;
		bimanualTimeLeft = 0;
		left = false;
		rigth = false;
		canvas1_1.SetActive (false);
		//
		anticipationTotalTime = 15.0f;
		anticipationRightControl = false;
		anticipationLeftControl = false;
		anticipationRightCount = 0.0f;
		anticipationLeftCount = 0.0f;
		anticipationResult = 0.0f;
		//
		auditive = false;
		auditiveTimer = 0.0f;
		leftAuditive = false;
		leftAuditiveTime = 0.0f;
		rightAuditive = false;
		rightAuditiveTime = 0.0f;
		auditiveLeftResults = "";
		auditiveRightResults = "";
		//
		reactimetry = false;
		globalReactimetryTimer = 0.0f;
		reactimetryGreenBool = false;
		reactimetryGreenTimer = 0.0f;
		reactimetryRedBool = false;
		reactimetryRedTimer = 0.0f;
		reactimetryGreenResults = "";
		reactimetryRedResults = "";
		//
		_dazzling = false;
		dazzlingTimer = 0.0f;
		dazzlingBool = false;
		//
		_visual = false;
		visualControl = 0;
		//
		_colors = false;
		colorsControl = 0;
		//
		Ishihara = false;
		IshiharaColorsControl = 0;
	}
	public void HandleOnDeviceOff (BluetoothDevice dev) {
		if (!string.IsNullOrEmpty (dev.Name))
			//status.text = "No se puede conectar con " + dev.Name + ", bt OFF";
			Debug.Log ("No connection!");
		else if (!string.IsNullOrEmpty (dev.Name)) {
			//status.text = "No se puede conectar con " + dev.MacAddress + ", Mac error";
			Debug.Log ("No MAC connection!");
		}
	}
	public void HandleOnDevicePicked (BluetoothDevice device) {
		this.device = device;
		device.setEndByte (10);
		//
		device.ReadingCoroutine = ManageConnection;
	}
	public void showDevices () {
		BluetoothAdapter.showDevices ();
	}
	public void connect () {
		if (device != null) {
			device.connect ();
			//status.text = "";
		}
	}
	public void disconnect () {
		if (device != null) {
			device.close ();
		}
	}
	public void send () {
		if (device != null && !string.IsNullOrEmpty (dataToSend.text)) {
			device.send (System.Text.Encoding.ASCII.GetBytes (dataToSend.text + (char)10));
		}
	}
	public IEnumerator ManageConnection (BluetoothDevice device) {
		while (device.IsReading) {
			if (device.IsDataAvailable) {
				yield return new WaitForSeconds (3);
				rigth = true;
				left = true;
			}
			yield return null;
		}
	}
	private void FixedUpdate() {
		byte [] msg = device.read ();
		if (msg != null && msg.Length > 0) {
			content = System.Text.ASCIIEncoding.ASCII.GetString (msg);
			// stop everything
			if (content != "HAS STOPPED") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = false;
				_dazzling = false;
				_visual = false;
				_colors = false;
				Ishihara = false;
			}
			// coordinacion bimanual
			if (content == "FirstGame") {
				bimanual = true;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = false;
				_dazzling = false;
				_visual = false;
				_colors = false;
				Ishihara = false;
			}
			if (content == "SecondGameR") {
				bimanual = false;
				anticipationRightControl = true;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = false;
				_dazzling = false;
				_visual = false;
				_colors = false;
				Ishihara = false;
			}
			if (content == "SecondGameL") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = true;
				auditive = false;
				reactimetry = false;
				_dazzling = false;
				_visual = false;
				_colors = false;
				Ishihara = false;
			}
			if (content == "ThirdGame") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = true;
				reactimetry = false;
				_dazzling = false;
				_visual = false;
				_colors = false;
				Ishihara = false;
				//
				leftAuditiveTime = 0;
				rightAuditiveTime = 0;
				rightAudioSource = rightSound.gameObject.GetComponent<AudioSource> ();
				leftAudioSource = leftSound.gameObject.GetComponent<AudioSource> ();
			}
			if (content == "FourGame") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = true;
				_dazzling = false;
				_visual = false;
				_colors = false;
				Ishihara = false;
				//
				reactimetryGreen.SetActive (false);
				reactimetryRed.SetActive (false);
			}
			if (content == "FiveGame") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = false;
				_dazzling = true;
				_visual = false;
				_colors = false;
				Ishihara = false;
			}
			if (content == "SixGame") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = false;
				_dazzling = false;
				_visual = true;
				_colors = false;
				Ishihara = false;
				//
				visualMaterial = visual.gameObject.GetComponent <Renderer> ().material;
			}
			if (content == "SevenGame") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = false;
				_dazzling = false;
				_visual = false;
				_colors = true;
				Ishihara = false;
				//
				colorMaterial = colors.gameObject.GetComponent <Renderer> ().material;
			}
			if (content == "EightGame") {
				bimanual = false;
				anticipationRightControl = false;
				anticipationLeftControl = false;
				auditive = false;
				reactimetry = false;
				_dazzling = false;
				_visual = false;
				_colors = false;
				Ishihara = true;
				//
				IshiharaMaterial = IshiharaColors.gameObject.GetComponent <Renderer> ().material;
			}
			/*
			if (content == "NineGame") {
				// test de foria
			}
			*/
			// *****
			// *****
			// *****
			// Start
			// *****
			// *****
			// *****
			if (bimanual == true) {
				if (content.Substring (0, 1) == "R") {
					contentRight = content.Substring (1, content.Length - 1);
					bimanualRightValue = int.Parse (contentRight);
				}
				if (content.Substring (0, 1) == "L") {
					contentLeft = content.Substring (1, content.Length - 1);
					bimanualLeftValue = int.Parse (contentLeft);
				}
				if ((rigth == true) && (left == true)) {
					bimanualTimer = bimanualTimer + 0.5f * Time.deltaTime;
					_camera.transform.localPosition = new Vector3 (0, bimanualTimer, 0);
				}
				if (rigth == true) {
					bimanualTimeRight = bimanualTimeRight + 0.5f * Time.deltaTime;
					if (bimanualRightValue > 767) {
						carRight.transform.Translate ((Vector3.left * 2) * Time.deltaTime);
					}
					if (bimanualRightValue > 611) {
						carRight.transform.Translate (Vector3.left * Time.deltaTime);
					}
					if (bimanualRightValue < 411) {
						carRight.transform.Translate (Vector3.right * Time.deltaTime);
					}
					if (bimanualRightValue < 255) {
						carRight.transform.Translate ((Vector3.right * 2) * Time.deltaTime);
					}
				}
				if (left == true) {
					bimanualTimeLeft = bimanualTimeLeft + 0.5f * Time.deltaTime;
					if (bimanualLeftValue > 767) {
						carLeft.transform.Translate ((Vector3.left * 2) * Time.deltaTime);
					}
					if (bimanualLeftValue > 611) {
						carLeft.transform.Translate (Vector3.left * Time.deltaTime);
					}
					if (bimanualLeftValue < 411) {
						carLeft.transform.Translate (Vector3.right * Time.deltaTime);
					}
					if (bimanualLeftValue < 255) {
						carLeft.transform.Translate ((Vector3.right * 2) * Time.deltaTime);
					}
				}
			}
			// test de anticipacion, derecha
			if (anticipationRightControl == true) {
				_camera.transform.localPosition = (Vector3.right);
				//
				anticipationRightCount = anticipationRightCount + 1 * Time.deltaTime;
			}
			if (content == "buttonRightPressed") {
				anticipationRightControl = false;
				anticipationRightResult = anticipationTotalTime - anticipationRightCount;
			}
			if (anticipationRightCount > anticipationTotalTime) {
				anticipationRightControl = false;
			}
			// test de anticipacion, izquierda
			if (anticipationLeftControl == true) {
				_camera.transform.localPosition = (Vector3.left);
				//
				anticipationLeftCount = anticipationLeftCount + 1 * Time.deltaTime;
			}
			if (content == "buttonLeftPressed") {
				anticipationLeftControl = false;
				anticipationLeftResult = anticipationTotalTime - anticipationLeftCount;
			}
			if (anticipationLeftCount > anticipationTotalTime) {
				anticipationLeftControl = false;
			}
			// test auditivo
			if (auditive == true) {
				auditiveTimer = auditiveTimer + 1 * Time.deltaTime;
				if (auditiveTimer > 3.0f) {
					rightAuditive = true;
				}
				if (auditiveTimer > 6.0f) {
					leftAuditive = true;
				}
				if (auditiveTimer > 10.0f) {
					rightAuditive = true;
				}
				if (auditiveTimer > 12.0f) {
					leftAuditive = true;
					rightAuditive = true;
				}
				if (auditiveTimer > 15.0f) {
					rightAuditive = true;
				}
				if (auditiveTimer > 17.0f) {
					leftAuditive = true;
					rightAuditive = true;
				}
				if (auditiveTimer > 20.0f) {
					auditive = false;
					leftAuditive = false;
					rightAuditive = false;
				}
				//
				if (rightAuditive == true) {
					rightAuditiveTime = rightAuditiveTime + 1 * Time.deltaTime;
					rightAudioSource.Play ();
				}
				if (leftAuditive == true) {
					leftAuditiveTime = leftAuditiveTime + 1 * Time.deltaTime;
					leftAudioSource.Play ();
				}
				//
				if (content == "buttonAuditiveRight") {
					rightAuditive = false;
				}
				if (content == "buttonAuditiveLeft") {
					leftAuditive = false;
				}
			}
			if (auditive == false) {
				auditiveRightResults = rightAuditiveTime.ToString ();
				auditiveLeftResults = leftAuditiveTime.ToString ();
			}
			// test de reactimetria
			if (reactimetry == true) {
				globalReactimetryTimer = globalReactimetryTimer + 1 * Time.deltaTime;
				if (globalReactimetryTimer > 5.0f) {
					reactimetryGreenBool = true;
				}
				if (globalReactimetryTimer > 7.0f) {
					reactimetryRedBool = true;
				}
				if (globalReactimetryTimer > 9.0f) {
					reactimetryGreenBool = true;
				}
				if (globalReactimetryTimer > 1.0f) {
					reactimetryRedBool = true;
				}
				if (globalReactimetryTimer > 15.0f) {
					reactimetryGreenBool = false;
					reactimetryRedBool = false;
				}
				if (reactimetryGreenBool == true) {
					reactimetryGreenTimer = reactimetryGreenTimer + 1 * Time.deltaTime;
					reactimetryGreen.SetActive (true);
				}
				if (reactimetryRedBool == true) {
					reactimetryRedTimer = reactimetryRedTimer + 1 * Time.deltaTime;
					reactimetryRed.SetActive (true);
				}
				if (content == "buttonReactimetryRight") {
					reactimetryGreenBool = false;
					reactimetryGreen.SetActive (false);
				}
				if (content == "buttonReactimetryLeft") {
					reactimetryRedBool = false;
					reactimetryRed.SetActive (false);
				}
			}
			// test de foria
			// ***** investigar ***** //
			// test de encandilamientio
			if (_dazzling == true) {
				dazzlingBool = true;
				if (dazzlingBool == true) {
					dazzlingTimer = dazzlingTimer + 1 * Time.deltaTime;
					float dazzlingColor = dazzling.GetComponent<Material> ().color.a;
					dazzlingColor = dazzlingTimer;
					if (dazzlingTimer > 7.0f) {
						dazzlingColor = 0.0f;
					}
				}
			}
			// test de agudeza visual
			if (_visual == true) {
				if (content == "visualButtonPressed") {
					visualControl++;
				}
				if ((visualControl > 0) && (visualControl < 2)) {
					visualMaterial = visual0;
				}
				if ((visualControl > 1) && (visualControl < 3)) {
					visualMaterial = visual1;
				}
				if ((visualControl > 2) && (visualControl < 4)) {
					visualMaterial = visual2;
				}
				if ((visualControl > 3) && (visualControl < 5)) {
					visualMaterial = visual3;
				}
			}
			// test de colores primarios
			if (_colors == true) {
				if (content == "colorsButtonPressed") {
					colorsControl++;
				}
				if ((colorsControl > 0) && (colorsControl < 2)) {
					colorMaterial = color0;
				}
				if ((colorsControl > 1) && (colorsControl < 3)) {
					colorMaterial = color1;
				}
				if ((colorsControl > 2) && (colorsControl < 4)) {
					colorMaterial = color2;
				}
			}
			// test de colores Ishihara
			if (Ishihara == true) {
				if (content == "IshiharaButtonPressed") {
					IshiharaColorsControl++;
				}
				if ((IshiharaColorsControl > 0) && (IshiharaColorsControl < 2)) {
					IshiharaMaterial = IshiharaColor0;
				}
				if ((IshiharaColorsControl > 1) && (IshiharaColorsControl < 3)) {
					IshiharaMaterial = IshiharaColor1;
				}
				if ((IshiharaColorsControl > 2) && (IshiharaColorsControl < 4)) {
					IshiharaMaterial = IshiharaColor2;
				}
			}
		}
	}
	public void Deactivation() {
		// Use this function with arduino Uno, desactivate this with using Mega
		/*
		if (dataToSend.text == "1") {
			Reset ();
			content = "FirstGame";
			canvas.SetActive (true);
			canvas1_1.SetActive (false);
			canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			canvas8.SetActive (false);
		}
		*/
		if (dataToSend.text == "2") { //2R
			Reset ();
			content = "SecondGameR";
			canvas.SetActive (false);
			canvas1_1.SetActive (true);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			//canvas8.SetActive (false);
		}/*
		if (dataToSend.text == "2L") {
			Reset ();
			content = "SecondGameL";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			canvas1_2.SetActive (true);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			canvas8.SetActive (false);
		}*/
		if (dataToSend.text == "3") {
			Reset ();
			content = "ThirdGame";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (true);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			//canvas8.SetActive (false);
		}
		if (dataToSend.text == "4") {
			Reset ();
			content = "FourGame";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (true);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			//canvas8.SetActive (false);
		}
		if (dataToSend.text == "5") {
			Reset ();
			content = "FiveGame";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (true);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			//canvas8.SetActive (false);
		}
		if (dataToSend.text == "6") {
			Reset ();
			content = "SixGame";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (true);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			//canvas8.SetActive (false);
		}
		if (dataToSend.text == "7") {
			Reset ();
			content = "SevenGame";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (true);
			canvas7.SetActive (false);
			//canvas8.SetActive (false);
		}
		if (dataToSend.text == "8") {
			Reset ();
			content = "EightGame";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (true);
			//canvas8.SetActive (false);
		}
		/*
		if (dataToSend.text == "9") {
			Reset ();
			content = "NineGame";
			canvas.SetActive (false);
			canvas1_1.SetActive (false);
			//canvas1_2.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			canvas5.SetActive (false);
			canvas6.SetActive (false);
			canvas7.SetActive (false);
			//canvas8.SetActive (true);
		}
		*/
	}
	private void OnDestroy () {
		BluetoothAdapter.OnDevicePicked -= HandleOnDevicePicked; 
		BluetoothAdapter.OnDeviceOFF -= HandleOnDeviceOff;
	}
}