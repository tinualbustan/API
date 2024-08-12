using InventoryManagementCore.Application.DTOs;

namespace InventoryManagementCore.Application.Interfaces
{
    public interface IDocManager
    {
        byte[] CreateDocument(CreateDocumentDTO dto);
        byte[] GetTemplate(string templateName);
    }
}
