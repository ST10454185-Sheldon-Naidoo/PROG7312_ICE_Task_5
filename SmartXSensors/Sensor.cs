using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartXSensors
{
    public class Sensor
    {
        public string DeviceId { get; set; }
        public string Location { get; set; }
        public double Temperature { get; set; }
        public Sensor(string deviceId, string location, double temperature)
        {
            DeviceId = deviceId;
            Location = location;
            Temperature = temperature;
        }
        public override string ToString()
        {
            return $"Device ID: {DeviceId,-10} | Location: {Location,-15} | Temperature: {Temperature,6:F1} °C";
        }
    }
}
