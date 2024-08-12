namespace InventoryManagementCore.Application.DTOs
{
    public class DocTable
    {
        public string TableBookMark { get; set; }
        public List<string> Headers { get; set; }
        public List<List<CellData>> Rows { get; set; }

        public List<List<object>> ConvertRowsToListRow()
        {
            return Rows.Select(row => row.Select(cell => cell.Data).ToList()).ToList();
        }
    }
}
