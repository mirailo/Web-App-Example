using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using MyFirstRazorWebPage.Models;
using MyFirstRazorWebPage.Pages.DatabaseConnection;
using MigraDocCore.DocumentObjectModel.Tables;

namespace MyFirstRazorWebPage.Pages.StaffPage
{
    public class CustomerDetails1Model : PageModel
    {
        private readonly HairSalonApptContext _context;

        public CustomerDetails1Model(HairSalonApptContext context)
        {
            _context = context;
        }

        public string UserName;
        public const string SessionKeyName = "username";

        public IList<Customer> Customer { get; set; }


        public async Task<IActionResult> OnGetAsync(string pdf)
        {
            Customer = await _context.Customer.ToListAsync();

            UserName = HttpContext.Session.GetString(SessionKeyName);
            UserName = HttpContext.Session.Id;
            Console.WriteLine("Current session: " + UserName);

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); 
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Customer";


            var reader = command.ExecuteReader();

            Customer = new List<Customer>();
            while (reader.Read())
            {
                Customer Row = new Customer(); //each record found from the table
                Row.ID = reader.GetInt32(0);
                Row.FirstName = reader.GetString(1);
                Row.EmailAdd = reader.GetString(2);
                Row.Password = reader.GetString(3); // We dont get the password. The role field is in the 5th position
                Customer.Add(Row);
            }

            if (pdf == "1")
            {
                //Create an object for pdf document
                Document doc = new Document();
                Section sec = doc.AddSection();
                Paragraph para = sec.AddParagraph();

                para.Format.Font.Name = "Arial";
                para.Format.Font.Size = 14;
                para.Format.Font.Color = Color.FromCmyk(0, 0, 0, 100);
                para.AddFormattedText("List of Users", TextFormat.Bold);
                para.Format.SpaceAfter = "1.0cm";

                para.AddFormattedText();


                //Table
                Table tab = new Table();
                tab.Borders.Width = 0.75;
                tab.TopPadding = 5;
                tab.BottomPadding = 5;

                //Column
                Column col = tab.AddColumn(Unit.FromCentimeter(1.5));
                col.Format.Alignment = ParagraphAlignment.Justify;
                tab.AddColumn(Unit.FromCentimeter(3));
                tab.AddColumn(Unit.FromCentimeter(4));
                tab.AddColumn(Unit.FromCentimeter(2));

                //Row
                Row row = tab.AddRow();
                row.Shading.Color = Colors.Coral;//select your preference colour!



                //Cell for header
                Cell cell = new Cell();
                cell = row.Cells[0];
                cell.AddParagraph("No.");
                cell = row.Cells[1];
                cell.AddParagraph("First Name");
                cell = row.Cells[2];
                cell.AddParagraph("Email Address");
                cell = row.Cells[3];
                cell.AddParagraph("Password");

                //Add data to table

                for (int i = 0; i < Customer.Count; i++)

                {
                    row = tab.AddRow();
                    cell = row.Cells[0];
                    cell.AddParagraph(Convert.ToString(i + 1));
                    cell = row.Cells[1];
                    cell.AddParagraph(Customer[i].FirstName);
                    cell = row.Cells[2];
                    cell.AddParagraph(Customer[i].EmailAdd);
                    cell = row.Cells[3];
                    cell.AddParagraph(Customer[i].Password);
                }

                tab.SetEdge(0, 0, 4,(Customer.Count + 1), Edge.Box, BorderStyle.Single, 1.5, Colors.Black);
                sec.Add(tab);

                //Rendering

                PdfDocumentRenderer pdfRen = new PdfDocumentRenderer();
                pdfRen.Document = doc;
                pdfRen.RenderDocument();

                //Create a memory stream
                MemoryStream stream = new MemoryStream();
                pdfRen.PdfDocument.Save(stream); //saving the file into the stream
                Response.Headers.Add("content-disposition", new[] { "inline; filename = ListofUser.pdf" }); // a new tab
                return File(stream, "application/pdf");

            }




            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Session ended");
                return RedirectToPage("/StaffPage/Index2");
            }
            else
            {
                 return Page();
            }

        }
    }
}
