using LINQtoCSV;

namespace Project1
{
    class Computer
    {
        [CsvColumn(FieldIndex = 1, CanBeNull = false)]
        public string Producer { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public string ScreenSize { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public string ScreenResolution { get; set; }

        [CsvColumn(FieldIndex = 4)]
        public string DisplayType { get; set; }

        //[CsvColumn(FieldIndex = 5)]
        //public string NoName { get; set; }

        [CsvColumn(FieldIndex = 6)]
        public string Processor { get; set; }

        [CsvColumn(FieldIndex = 7)]
        public string PhysicalCores { get; set; }

        [CsvColumn(FieldIndex = 8)]
        public string CpuClockSpeed { get; set; }

        [CsvColumn(FieldIndex = 9)]
        public string Ram { get; set; }

        [CsvColumn(FieldIndex = 10)]
        public string DiscSize { get; set; }

        [CsvColumn(FieldIndex = 11)]
        public string discType { get; set; }

        [CsvColumn(FieldIndex = 12)]
        public string Gpu { get; set; }

        [CsvColumn(FieldIndex = 13)]
        public string GpuMemory { get; set; }

        [CsvColumn(FieldIndex = 14)]
        public string OperatingSystem { get; set; }

        [CsvColumn(FieldIndex = 15)]
        public string OpticalDrive { get; set; }

        //public DiscTypeEnum DiscType { get; set; }

    }

    public enum DiscTypeEnum
    {
        HDD,
        SSD
    }
}
