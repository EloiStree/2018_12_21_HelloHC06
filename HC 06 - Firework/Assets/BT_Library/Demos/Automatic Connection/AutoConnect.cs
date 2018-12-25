using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TechTweaking.Bluetooth;
using UnityEngine.UI;
using System;

public class AutoConnect : MonoBehaviour
{

	private  BluetoothDevice device;
	//public Text statusText;
    public string macAddress;

	public void StartConnectionWithMicroController (string macAddress)
	{
        this.macAddress = macAddress;
        device = new BluetoothDevice ();

		if (BluetoothAdapter.isBluetoothEnabled ()) {
			connect ();
		} else {

            //BluetoothAdapter.enableBluetooth(); //you can by this force enabling Bluetooth without asking the user
            //	statusText.text = "Status : Please enable your Bluetooth";

            BluetoothAdapter.OnBluetoothStateChanged += HandleOnBluetoothStateChanged;
			BluetoothAdapter.listenToBluetoothState (); // if you want to listen to the following two events  OnBluetoothOFF or OnBluetoothON

			BluetoothAdapter.askEnableBluetooth ();//Ask user to enable Bluetooth

        }
        BluetoothAdapter.OnDeviceOFF += HandleOnDeviceOff;//This would mean a failure in connection! the reason might be that your remote device is OFF
        BluetoothAdapter.OnDeviceNotFound += HandleOnDeviceNotFound; //Because connecting using the 'Name' property is just searching, the Plugin might not find it!(only for 'Name').

    }

    public void SetPin(bool isOn, int pinIndex) {

        if (device != null)
        {
            string sendTxt = "";
            if (pinIndex == 0)
                sendTxt = isOn ? "a" : "A";
            if (pinIndex == 1)
                sendTxt = isOn ? "b" : "B";
            if (pinIndex == 2)
                sendTxt = isOn ? "c" : "C";
            if (pinIndex == 3)
                sendTxt = isOn ? "d" : "D";
            if (pinIndex == 4)
                sendTxt = isOn ? "e" : "E";
            if (pinIndex == 5)
                sendTxt = isOn ? "f" : "F";
            if (pinIndex == 6)
                sendTxt = isOn ? "g" : "G";
            if (pinIndex == 7)
                sendTxt = isOn ? "h" : "H";
            if (pinIndex == 8)
                sendTxt = isOn ? "i" : "I";
            if (pinIndex == 9)
                sendTxt = isOn ? "j" : "J";
            device.send(System.Text.Encoding.ASCII.GetBytes(sendTxt));
        }
    }
    public void OnDisable()
    {
        device.close();
    }


    private void connect ()
	{


        // statusText.text = "Status : Trying To Connect";


        /* The Property device.MacAdress doesn't require pairing. 
		 * Also Mac Adress in this library is Case sensitive,  all chars must be capital letters
		 */
        //device.MacAddress = "XX:XX:XX:XX:XX:XX";
        device.MacAddress = macAddress;

        /* device.Name = "My_Device";
		* 
		* Trying to identefy a device by its name using the Property device.Name require the remote device to be paired
		* but you can try to alter the parameter 'allowDiscovery' of the Connect(int attempts, int time, bool allowDiscovery) method.
		* allowDiscovery will try to locate the unpaired device, but this is a heavy and undesirable feature, and connection will take a longer time
		*/

        /*
		 * The ManageConnection Coroutine will start when the device is ready for reading.
		 */
        device.ReadingCoroutine = ManageConnection;



        // statusText.text = "Status : trying to connect";

        device.connect ();
        Invoke("Blink", 0.5f);

	}
    public void Blink() {


        StartCoroutine(BlinkBlinkBaby());
    }

   

    private IEnumerator BlinkBlinkBaby()
    {

        for (int j = 1; j < 3; j++) 
        {
            for (int i = 0; i < 10; i++)
            {
                SetPin(true, i);
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 0; i < 10; i++)
            {
                SetPin(false, i);
                yield return new WaitForSeconds(0.1f );
            }

            yield return new WaitForEndOfFrame();

        }
        

    }
    private String Number2String(int number, bool isCaps)

    {

        Char c = (Char)((isCaps ? 64 : 96) + number);

        return c.ToString();

    }

    //internal void SetPin(bool isOn, int pinIndex, float delay)
    //{

    //}


    //############### Handlers/Recievers #####################
    void HandleOnBluetoothStateChanged (bool isBtEnabled)
	{
		if (isBtEnabled) {
			connect ();
			//We now don't need our recievers
			BluetoothAdapter.OnBluetoothStateChanged -= HandleOnBluetoothStateChanged;
			BluetoothAdapter.stopListenToBluetoothState ();
		}
	}

	//This would mean a failure in connection! the reason might be that your remote device is OFF
	void HandleOnDeviceOff (BluetoothDevice dev)
	{
		//if (!string.IsNullOrEmpty (dev.Name)) {
		//	statusText.text = "Status : can't connect to '" + dev.Name + "', device is OFF ";
		//} else if (!string.IsNullOrEmpty (dev.MacAddress)) {
		//	statusText.text = "Status : can't connect to '" + dev.MacAddress + "', device is OFF ";
		//}
	}

	//Because connecting using the 'Name' property is just searching, the Plugin might not find it!.
	void HandleOnDeviceNotFound (BluetoothDevice dev)
	{
		//if (!string.IsNullOrEmpty (dev.Name)) {
		//	statusText.text = "Status : Can't find a device with the name '" + dev.Name + "', device might be OFF or not paird yet ";

		//} 
	}
	
	public void disconnect ()
	{
		if (device != null)
			device.close ();
        Destroy(this.gameObject);
	}
	
	//############### Reading Data  #####################
	//Please note that you don't have to use this Couroutienes/IEnumerator, you can just put your code in the Update() method.
	IEnumerator  ManageConnection (BluetoothDevice device)
	{
		//statusText.text = "Status : Connected & Can read";

		while (device.IsReading) {

			byte [] msg = device.read ();
			if (msg != null) {

				
				string content = System.Text.ASCIIEncoding.ASCII.GetString (msg);
			//	statusText.text = "MSG : " + content;
			}
			yield return null;
		}

//		statusText.text = "Status : Done Reading";
	}
	
	
	//############### Deregister Events  #####################
	void OnDestroy ()
	{
		BluetoothAdapter.OnDeviceOFF -= HandleOnDeviceOff;
		BluetoothAdapter.OnDeviceNotFound -= HandleOnDeviceNotFound;
		
	}

    public void SetPinOnOff(int pinIndex, float offDelay) {

        SetPin(true, pinIndex);
        SetPinWithDelay(false, pinIndex, offDelay);
    }

    public void SetPinWithDelay(bool isOn, int pinIndex, float delay)
    {
        StartCoroutine(WaitToSetPin(isOn, pinIndex, delay));
    }
    IEnumerator WaitToSetPin(bool isOn, int pinIndex, float delay) {
        yield return new WaitForSeconds(delay);
        SetPin(isOn, pinIndex);
    }


}
