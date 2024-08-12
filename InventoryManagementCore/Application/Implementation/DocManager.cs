using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using InventoryManagementCore.Application.DTOs;
using InventoryManagementCore.Application.Interfaces;

namespace InventoryManagementCore.Application.Implementation
{
    public class DocManager : IDocManager
    {
        //    public byte[] CreateDocument(CreateDocumentDTO dto)
        //    {
        //        string templatePath = @"Templates/" + dto.TemplateName + ".docx";
        //        byte[] fileBytes = File.ReadAllBytes(templatePath);
        //        using (MemoryStream stream = new())
        //        {
        //            stream.Write(fileBytes, 0, fileBytes.Length);
        //            using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
        //            {
        //                var textBookmark = dto.GetDataDictionary();

        //                ReplaceBookmarksInDoc(doc, textBookmark);
        //                ReplaceFooterBookmarks(doc, textBookmark);
        //                ReplaceHeaderBookmarks(doc, textBookmark);
        //                if (dto?.DocTables != null)
        //                    foreach (var tb in dto?.DocTables)
        //                    {
        //                        if (tb != null)
        //                        {
        //                            if (tb.TableBookMark.Contains("SignatureTable")) continue;
        //                            if (tb.Headers == null)
        //                            {
        //                                AddRowsToTable(doc, tb.ConvertRowsToListRow(), tb.TableBookMark);
        //                                break;
        //                            }
        //                            var table = CreateNewTable(tb.Headers, tb.ConvertRowsToListRow());
        //                            ReplaceBookmarkWithTable(doc, tb.TableBookMark, table);
        //                        }
        //                    }
        //                doc.Save();
        //            }
        //            return stream.ToArray();
        //        }
        //        //throw new BusinessRuleValidationException("Something Went Wrong");
        //    }
        //    private static Table CreateNewTable(List<string> headers, List<List<object>> rowsData)
        //    {
        //        var table = new Table();
        //        if (headers == null || headers.Count < 1) return table;

        //        TableWidth tableWidth = new() { Width = "9526", Type = TableWidthUnitValues.Dxa };
        //        TableProperties tblProps = new(
        //            new TableBorders(
        //                new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //                new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //                new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //                new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //                new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
        //                new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
        //            ), tableWidth
        //        );
        //        table.Append(tblProps);

        //        var headerRow = new TableRow();

        //        foreach (var header in headers)
        //        {
        //            var cell = new TableCell();
        //            var cellProps = new TableCellProperties(
        //                new Shading { Val = ShadingPatternValues.Clear, Fill = "D3D3D3" },
        //                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center });
        //            cell.Append(cellProps);

        //            var textElement = new Text(header);

        //            var runProps = new RunProperties();
        //            runProps.Append(new Bold());
        //            var run = new Run(runProps, textElement);

        //            var paraProps = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });
        //            var para = new Paragraph(paraProps, run);

        //            cell.Append(para);
        //            headerRow.Append(cell);
        //        }

        //        table.Append(headerRow);

        //        foreach (var rowData in rowsData)
        //        {
        //            var dataRow = new TableRow();
        //            for (int i = 0; i < headers.Count; i++)
        //            {
        //                var cell = new TableCell();
        //                object cellValue = rowData.Count > i ? rowData[i] : string.Empty;
        //                if (cellValue is string stringCellValue)
        //                {
        //                    cell.Append(new Paragraph(new Run(new Text(stringCellValue))));
        //                }
        //                else
        //                {
        //                    throw new NotImplementedException();
        //                }
        //                dataRow.Append(cell);
        //            }
        //            table.Append(dataRow);
        //        }

        //        return table;
        //    }
        //    private static void AddRowsToTable(WordprocessingDocument doc, List<List<object>> Rows, string bookmarkName)
        //    {
        //        if (Rows == null || !Rows.Any())
        //            return;

        //        var bookmarkStart = doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>().FirstOrDefault(b => b.Name == bookmarkName);
        //        if (bookmarkStart == null)
        //            return;

        //        Table table = bookmarkStart.Ancestors<Table>().FirstOrDefault();
        //        if (table == null)
        //            return;

        //        foreach (var rowCells in Rows)
        //        {
        //            var newRow = new TableRow();
        //            foreach (var cellData in rowCells)
        //            {
        //                var newCell = new TableCell();
        //                var cellProperties = new TableCellProperties();
        //                var cellBorders = new TableCellBorders
        //                {
        //                    TopBorder = new TopBorder() { Val = BorderValues.Single },
        //                    BottomBorder = new BottomBorder() { Val = BorderValues.Single },
        //                    LeftBorder = new LeftBorder() { Val = BorderValues.Single },
        //                    RightBorder = new RightBorder() { Val = BorderValues.Single }
        //                };

        //                cellProperties.Append(cellBorders);
        //                newCell.Append(cellProperties);
        //                if (cellData is string stringCellData)
        //                {
        //                    newCell.Append(new Paragraph(new Run(new Text(stringCellData))));
        //                }
        //                else if (cellData == null)
        //                {
        //                    newCell.Append(new Paragraph(new Run(new Text(""))));
        //                }
        //                else
        //                {
        //                    throw new NotImplementedException();
        //                }

        //                newRow.Append(newCell);
        //            }
        //            table.Append(newRow);
        //        }
        //    }

        //    private static void ReplaceBookmarksInDoc(WordprocessingDocument doc, Dictionary<string, string> textBookmark)
        //    {
        //        var body = doc.MainDocumentPart.Document.Body;
        //        foreach (var bookmarkStart in body.Descendants<BookmarkStart>())
        //        {
        //            var bookmarkName = bookmarkStart.Name;
        //            var bookmarkEnd = body.Descendants<BookmarkEnd>().Where(e => e.Id.Value == bookmarkStart.Id.Value).FirstOrDefault();
        //            if (textBookmark.ContainsKey(bookmarkName))
        //            {
        //                OpenXmlElement currentElem = bookmarkStart.NextSibling();
        //                while (currentElem != null && currentElem != bookmarkEnd)
        //                {
        //                    if (currentElem is Run run)
        //                    {
        //                        var textElem = run.GetFirstChild<Text>();
        //                        textElem?.Remove();
        //                    }

        //                    currentElem = currentElem.NextSibling();
        //                }


        //                var bookmarkTextElem = bookmarkStart.NextSibling<Run>();
        //                if (bookmarkTextElem != null)
        //                {

        //                    var textElem = bookmarkTextElem.GetFirstChild<Text>();
        //                    if (textElem != null)
        //                    {
        //                        textElem.Text = textBookmark[bookmarkName];
        //                    }
        //                    else
        //                    {
        //                        bookmarkTextElem.AppendChild(new Text(textBookmark[bookmarkName]));
        //                    }
        //                }
        //                else
        //                {
        //                    var newRun = new Run(new Text(textBookmark[bookmarkName]));
        //                    bookmarkStart.Parent.InsertAfter(newRun, bookmarkStart);
        //                }
        //            }
        //        }
        //    }
        //    private static void ReplaceBookmarkWithTable(WordprocessingDocument doc, string bookmarkName, Table table)
        //    {
        //        var bookmarkStart = doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>().FirstOrDefault(b => b.Name == bookmarkName);
        //        if (bookmarkStart == null)
        //            return;
        //        var bookmarkContent = bookmarkStart.Parent.Elements().ToList();
        //        foreach (var element in bookmarkContent)
        //        {
        //            if (element != bookmarkStart)
        //                element.Remove();
        //        }
        //        bookmarkStart.Parent.InsertAfterSelf(table);
        //        var bookmarkEnd = doc.MainDocumentPart.RootElement.Descendants<BookmarkEnd>().FirstOrDefault(b => b.Id == bookmarkStart.Id);
        //        bookmarkEnd?.Remove();
        //        bookmarkStart.Remove();
        //    }

        //    private static void ReplaceFooterBookmarks(WordprocessingDocument doc, Dictionary<string, string> textBookmark)
        //    {
        //        foreach (FooterPart footerPart in doc.MainDocumentPart.FooterParts)
        //        {
        //            foreach (var bookmarkStart in footerPart.RootElement.Descendants<BookmarkStart>())
        //            {
        //                var bookmarkName = bookmarkStart.Name;
        //                if (textBookmark.ContainsKey(bookmarkName))
        //                {
        //                    var bookmarkTextElem = bookmarkStart.NextSibling<Run>();
        //                    if (bookmarkTextElem != null)
        //                    {
        //                        var textElem = bookmarkTextElem.GetFirstChild<Text>();
        //                        if (textElem != null)
        //                        {
        //                            textElem.Text = textBookmark[bookmarkName];
        //                        }
        //                        else
        //                        {
        //                            bookmarkTextElem.AppendChild(new Text(textBookmark[bookmarkName]));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var newRun = new Run(new Text(textBookmark[bookmarkName]));
        //                        bookmarkStart.Parent.InsertAfter(newRun, bookmarkStart);
        //                    }
        //                }
        //            }
        //            footerPart.Footer.Save();
        //        }
        //    }
        //    private static void ReplaceHeaderBookmarks(WordprocessingDocument doc, Dictionary<string, string> textBookmark)
        //    {
        //        foreach (HeaderPart headerPart in doc.MainDocumentPart.HeaderParts)
        //        {
        //            foreach (var bookmarkStart in headerPart.RootElement.Descendants<BookmarkStart>())
        //            {
        //                var bookmarkName = bookmarkStart.Name;
        //                if (textBookmark.ContainsKey(bookmarkName))
        //                {
        //                    var bookmarkTextElem = bookmarkStart.NextSibling<Run>();
        //                    if (bookmarkTextElem != null)
        //                    {
        //                        var textElem = bookmarkTextElem.GetFirstChild<Text>();
        //                        if (textElem != null)
        //                        {
        //                            textElem.Text = textBookmark[bookmarkName];
        //                        }
        //                        else
        //                        {
        //                            bookmarkTextElem.AppendChild(new Text(textBookmark[bookmarkName]));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var newRun = new Run(new Text(textBookmark[bookmarkName]));
        //                        bookmarkStart.Parent.InsertAfter(newRun, bookmarkStart);
        //                    }
        //                }
        //            }
        //            headerPart.Header.Save();
        //        }
        //    }


        //    public byte[] GetTemplate(string templateName)
        //    {
        //        throw new NotImplementedException();
        //    }
        public byte[] CreateDocument(CreateDocumentDTO dto)
        {
            throw new NotImplementedException();
        }

        public byte[] GetTemplate(string templateName)
        {
            throw new NotImplementedException();
        }
    }



}
