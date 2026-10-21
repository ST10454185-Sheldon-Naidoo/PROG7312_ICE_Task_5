using System;
using System.Collections.Generic;
using System.Globalization;

namespace SmartXSensors;

internal class Program
{
    private static void Main(string[] args)
    {
        // Dictionary Initialization with 3 sample sensors
        Dictionary<string, Sensor> sensorRegistry = new Dictionary<string, Sensor>(StringComparer.OrdinalIgnoreCase)
        {
            { "SN-101", new Sensor("SN-101", "Server Room", 18.5) },
            { "SN-102", new Sensor("SN-102", "Warehouse B", 22.0) },
            { "SN-103", new Sensor("SN-103", "Main Office", 24.3) }
        };

        // Looks up sensor based on DeviceId. Uses TryGetValue
        Console.Write("Enter Device ID to look up (e.g., SN-101): ");
        string? lookupInput = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(lookupInput))
        {
            if (sensorRegistry.TryGetValue(lookupInput, out Sensor? foundSensor))
            {
                Console.WriteLine($"\n[Found] Sensor Details:\n  {foundSensor}\n");
            }
            else
            {
                Console.WriteLine($"\n[Not Found] No sensor found with Device ID: \"{lookupInput}\"\n");
            }
        }

        // Updates temperature by DeviceId. Handles unknown IDs as well
        Console.Write("Enter Device ID to update: ");
        string? updateInput = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(updateInput))
        {
            if (sensorRegistry.TryGetValue(updateInput, out Sensor? sensorToUpdate))
            {
                Console.WriteLine($"Current Details: {sensorToUpdate}");
                Console.Write("Enter new temperature (°C): ");
                string? tempInput = Console.ReadLine()?.Trim();

                if (double.TryParse(tempInput?.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double newTemperature))
                {
                    double previousTemp = sensorToUpdate.Temperature;
                    sensorToUpdate.Temperature = newTemperature;
                    Console.WriteLine($"\n[Updated] Sensor '{sensorToUpdate.DeviceId}' temperature updated from {previousTemp:F1}°C to {newTemperature:F1}°C.\n");
                }
                else
                {
                    Console.WriteLine("\n[Error] Invalid temperature format. Update cancelled.\n");
                }
            }
            else
            {
                Console.WriteLine($"\n[Not Found] Cannot update temperature. Unknown Device ID: \"{updateInput}\"\n");
            }
        }

        // 5. Display all registered sensors
        DisplayAllSensors(sensorRegistry);
    }

    private static void DisplayAllSensors(Dictionary<string, Sensor> registry)
    {
        Console.WriteLine("=================================================");
        Console.WriteLine("            All Registered Sensors               ");
        Console.WriteLine("=================================================");
        Console.WriteLine($"{"Device ID",-12} | {"Location",-15} | {"Temperature",-12}");
        Console.WriteLine(new string('-', 45));

        foreach (KeyValuePair<string, Sensor> entry in registry)
        {
            Sensor s = entry.Value;
            Console.WriteLine($"{s.DeviceId,-12} | {s.Location,-15} | {s.Temperature,6:F1} °C");
        }

        Console.WriteLine(new string('-', 45));
        Console.WriteLine($"Total Registered Sensors: {registry.Count}");
    }
}
