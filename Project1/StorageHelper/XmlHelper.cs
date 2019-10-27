using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace StorageHelper
{
    public class XmlHelper
    {
        public List<Computer> LoadComputersFromXml(string path)
        {
            List<Computer> computers = new List<Computer>();
            XDocument xDocument = XDocument.Load(path);
            var laptops = xDocument.Elements().Elements();
            foreach (var laptop in laptops)
            {
                Computer computer = new Computer();
                computer.Producer = laptop.Element("manufacturer")?.Value;

                XElement screen = laptop.Element("screen");
                computer.ScreenSize = screen.Element("size")?.Value;
                computer.ScreenResolution = screen.Element("resolution")?.Value;
                computer.DisplayType = screen.Element("type")?.Value;
                computer.TouchScreen = screen.Element("touchscreen")?.Value;

                XElement processor = laptop.Element("processor");
                computer.Processor = processor.Element("name")?.Value;
                computer.CpuClockSpeed = processor.Element("clock_speed")?.Value;
                computer.PhysicalCores = processor.Element("physical_cores")?.Value;

                computer.Ram = laptop.Element("ram")?.Value;

                XElement disc = laptop.Element("disc");
                computer.DiscSize = disc.Element("storage")?.Value;
                computer.DiscType = disc.Element("type")?.Value;

                XElement graphic = laptop.Element("graphic_card");
                computer.Gpu = graphic.Element("name")?.Value;
                computer.GpuMemory = graphic.Element("memory")?.Value;

                computer.OperatingSystem = laptop.Element("os")?.Value;
                computer.OpticalDrive = laptop.Element("disc_reader")?.Value;

                computers.Add(computer);
            }

            return computers;
        }

        public void SaveDataToXml(string path, List<Computer> computers)
        {
            XElement root = new XElement("laptops");
            foreach(var computer in computers)
            {
                root.Add(
                        new XElement("laptop",
                            new XElement("manufacturer", computer.Producer),
                            new XElement("screen", 
                                new XElement("size", computer.ScreenSize),
                                new XElement("resolution", computer.ScreenResolution),
                                new XElement("type", computer.DiscType),
                                new XElement("touchscreen", computer.TouchScreen)),
                            new XElement("processor",
                                new XElement("name", computer.Processor),
                                new XElement("physical_cores", computer.PhysicalCores),
                                new XElement("clock_speed", computer.CpuClockSpeed)),
                            new XElement("ram", computer.Ram),
                            new XElement("disc",
                                new XElement("storage", computer.DiscSize),
                                new XElement("type", computer.DiscType)),
                            new XElement("graphic_card",
                                new XElement("name", computer.Gpu),
                                new XElement("memory", computer.GpuMemory)),
                            new XElement("os", computer.OperatingSystem),
                            new XElement("disc_reader", computer.OpticalDrive)
                    ));
            }

            XDocument xDoc = new XDocument(new XDeclaration("1.0", "UTF-16", null), root);
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(xDoc);
            }
        }
    }
}
