namespace InventoryManagementCore.Application.DTOs
{
    public class CreateDocumentDTO
    {
        public string TemplateName { get; set; }
        public List<StringData> DocData { get; set; }
        public List<DocTable> DocTables { get; set; }
        public Dictionary<string, string> GetDataDictionary()
        {
            return DocData.ToDictionary(x => x.BookMark, y => y.Data);
        }
    }
}
