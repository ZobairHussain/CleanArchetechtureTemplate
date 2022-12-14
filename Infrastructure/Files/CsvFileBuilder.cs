using System.Globalization;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoLists.Queries.ExportTodos;
using CleanArchitecture.Infrastructure.Files.Maps;
using CsvHelper;

namespace CleanArchitecture.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            
            //old
            //csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            //csvWriter.WriteRecords(records);

            //new
            csvWriter.WriteHeader<TodoItemRecordMap>();
            csvWriter.NextRecord();
            foreach (var record in records)
            {
                csvWriter.WriteRecord(record);
                csvWriter.NextRecord();
            }
        }

        return memoryStream.ToArray();
    }
}
