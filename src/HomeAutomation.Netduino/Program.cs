using System;
using System.Net; // hidden in the System.Http.dll!!
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace HomeAutomation.Netduino
{
    public class Program
    {
        private const string ServiceUrl = "http://home.one75.be/temperature";

        public static void Main()
        {
            MultipleDevices();
        }

        private static void MultipleDevices()
        {
            OneWire oneWire = new OneWire(new OutputPort(Pins.GPIO_PIN_D0, false));

            var devices = oneWire.FindAllDevices();
            var serialNumbers = new System.Collections.Hashtable();

            foreach (var device in devices)
            {
                var theIndex = devices.IndexOf(device);

                var theKey = devices[theIndex];
                var theValue = "someValue";
                serialNumbers.Add(theKey, theValue);
            }

            //double lastTemperature = -1;

            while (true)
            {
                if (devices.Count > 0)
                {
                    foreach (var device in devices)
                    {
                        devices.IndexOf(device);
                        oneWire.TouchReset();
                        oneWire.WriteByte(DS18B20.MatchROM); // Match ROM, we have multiple device
                        DS18B20.SetDevice(oneWire, device); // Set device
                        oneWire.WriteByte(DS18B20.ConvertT); // Start temperature conversion
                        while (oneWire.ReadByte() == 0)
                        {
                        }

                        oneWire.TouchReset();
                        oneWire.WriteByte(DS18B20.MatchROM); // Match ROM
                        DS18B20.SetDevice(oneWire, device); // Set device
                        oneWire.WriteByte(DS18B20.ReadScratchpad); // Read Scratchpad

                        double temp;
                        temp = DS18B20.GetTemperature((byte) oneWire.ReadByte(), (byte) oneWire.ReadByte());

                        // Read temperature
                        Debug.Print("Device: " + devices.IndexOf(device) + ", temperature: " + temp + " " +
                                    (DS18B20.TemperatureFormat == TemperatureFormat.Celcius ? "C" : "F"));

                        SendTempToService(temp, devices.IndexOf(device));
                    }
                }
                else
                {
                    Debug.Print("No device detected");
                }

                Thread.Sleep(60000);
            }
        }

        private static readonly OutputPort Led = new OutputPort(Pins.ONBOARD_LED, false);

        private static void Blink(int i)
        {
            Led.Write(true);
            Thread.Sleep(i);
            Led.Write(false);
        }

        private static void SendTempToService(double temp, int indexOf)
        {
            try
            {

                using (var myReq = (HttpWebRequest) WebRequest.Create(ServiceUrl))
                {
                    string dataAsJson = "{ CurrentTemperature : " + temp + "}";

                    byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(dataAsJson);

                    myReq.Method = "POST";
                    myReq.Timeout = 3000;
                    myReq.ReadWriteTimeout = 3000;
                    myReq.ContentType = "text/json";
                    myReq.ContentLength = dataBuffer.Length;

                    using (var reqStream = myReq.GetRequestStream())
                    {
                        reqStream.Write(dataBuffer, 0, dataBuffer.Length);
                        reqStream.Close();
                    }

                    var webResp = (HttpWebResponse) myReq.GetResponse();
                    if (webResp.StatusCode == HttpStatusCode.OK)
                        Blink(30);
                }
            }
            catch (Exception ex)
            {
                Debug.Print("Failed to send data...");
                Debug.Print(ex.Message);
                Blink(600);
            }
        }
    }

    public enum TemperatureFormat
    {
        Celcius,
        Fahrenheit
    }
}
