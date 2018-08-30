using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam
{
    public class SunnyBeamCsvReader
    {
        private readonly ICsvFileSource source;

        public SunnyBeamCsvReader(ICsvFileSource source)
        {
            this.source = source;
        }

        public IEnumerable<Reading> ReadAll()
        {
            foreach (var file in source.Enumerate())
            {
                using (var stream = file.Open())
                {
                    foreach (var reading in this.ReadFile(stream))
                    {
                        yield return reading;
                    }
                }
            }
        }

        private IEnumerable<Reading> ReadFile(Stream stream)
        {
            var config = new CsvHelper.Configuration.CsvConfiguration
            {
                SkipEmptyRecords = false,
                Delimiter = ";",
                HasExcelSeparator = true,
                HasHeaderRecord = false,
                IgnoreBlankLines = false
            };

            using (var textReader = new StreamReader(stream, Encoding.Default, true, 1024 * 64, true))
            using (var csvReader = new CsvReader(textReader, config))
            {
                DateTime date = default(DateTime);

                // Header rows
                for (var i = 0; i < 7; i++)
                {
                    if (!csvReader.Read())
                    {
                        throw new FormatException("File contains insufficient number of rows");
                    }

                    if (i == 0)
                    {
                        var field = csvReader.GetField(0);
                        string[] pipeSeparatedValues;
                        if (string.IsNullOrEmpty(field) || (pipeSeparatedValues = field.Split('|')).Count() < 10)
                        {
                            throw new FormatException($"Invalid value on row 2: {field}.");
                        }

                        if (!DateTime.TryParse(pipeSeparatedValues[9], out date))
                        {
                            throw new FormatException($"Invalid date value on row 2: {pipeSeparatedValues[9]}");
                        }
                    }
                }

                while (csvReader.Read())
                {
                    if (csvReader.IsRecordEmpty())
                    {
                        yield break;
                    }

                    TimeSpan time;
                    decimal kilowatts;

                    if (!csvReader.TryGetField(0, out time))
                    {
                        throw new FormatException($"Invalid time value on row {csvReader.Row}: {csvReader.GetField(0)}");
                    }

                    if (!csvReader.TryGetField(1, out kilowatts))
                    {
                        throw new FormatException($"Invalid kilowatts value on row {csvReader.Row}: {csvReader.GetField(1)}");
                    }

                    yield return new Reading(date.Add(time), kilowatts);
                }
            }
            
        }

    }
}
